using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHOA.TestFunctions
{
        public class TSFDE : IFitnessFunction
        {
            // Zmienne
            int N; //siatka w dziedzinie przestrzeni x
            int M; //siatka w dziedzinie czasu t
            double wart_funkcjonalu;
            double alfa; //rząd pochodnej względem czasu t
            double beta; //rząd pochodnej względem przestrzeni x
            double gamma; // współczynnik w warunku brzegowym (gamma=0 --> fractional Neumann, gamma > 0 - fractional Robin)

            // stałe pomocnicze
            double r;

            double deltaX; // krok siatki dla x
            double deltaT; //krok siatki dla czasu
            double[,] macierzA; // macierz wymiaru N x N - macierz współczynników układu równań
            double[] prawa; // kolumna wyrazów wolnych dla układu równań N x 1
            double[,] U; // dwuwymiarowa tablica w której będą przechowywane rozwiązania, wymiar (M+1)x(N+1) czas x przestrzeń
            double[] wektorRoz; // wektor N x 1 przechowujący rozwiązanie układu równań

            //zmienne do odwrotnych
            int liczba_termopar;
            double[,] dokladne; //[ liczba_termopar ][ liczba_pomiarow ]
            double[,] odczytane_temperatury; //[ liczba_termopar ][ liczba_pomiarow ]
            int ile_pomiarow;
            int[] punkty_pomiarowe_ind; //[liczba_termopar] w tablicy bedę przechowywane indeksy odpowiadające lokalizacji termopar

            // Warunki początkowo-brzegowe
            public delegate double Fractional_boundary_right_side(double t, params double[] w); //funkcja występująca w ułamkowym warunku brzegowym na prawym brzegu (x = Lx)
            Fractional_boundary_right_side war_brzeg_prawy;
            public delegate double diffusionCoefficient(double x, double t); //współczynnik przewodności cieplnej przy pochodnej po x i w warunku brzegowym
            diffusionCoefficient lambda;
            public delegate double SourceTerm(double x, double t); //funkcja występująca po prawej stronie równania, zależy od x, t
            SourceTerm f;

            // warunek brzegowy trzeciego rodzaju
            private double warunekBrzegowyRobin(double t, params double[] g)
            {
                return g[0] * t * t + g[1] * Math.Pow(t, 1.5) + g[2] * t + g[3] * Math.Sqrt(t) + g[4];
            }

            public double gammafunction(double x)
            {

                return gammafunction(x, null);
            }

        public class xparams
        {
            public ulong flags;
            public xparams(ulong v)
            {
                flags = v;
            }
        }

        public double gammafunction(double x, xparams _params)
            {

                return gammafunction(x, _params);
            }

            // pochodna po czasie
            public double Alfa
            {
                set { alfa = value; }
                get { return alfa; }
            }

            // pochodna po przestrzeni
            public double Beta
            {
                set { beta = value; }
                get { return beta; }
            }

            // Funkcje pomocnicze
            private double g(double alfa, int i)
            {
                if (i == 0)
                    return 1.0;
                return (1 - (alfa + 1.0) / i) * g(alfa, i - 1);
            }

            private double omega(double alfa, int i)
            {
                if (i == 0)
                    return (alfa / 2.0) * g(alfa, 0);
                return (alfa / 2.0) * g(alfa, i) + ((2.0 - alfa) / 2.0) * g(alfa, i - 1);
            }

            private double funkcjaB(double alfa, int j)
            {
                return Math.Pow(j + 1.0, 1.0 - alfa) - Math.Pow(j, 1.0 - alfa);
            }

            public double[] zwrocX(int indeks)
            {
                double[] wyn = new double[M + 1];
                for (int i = 0; i < M + 1; i++)
                {
                    wyn[i] = U[i, indeks];
                }

                return wyn;
            }

            // Rozwiązywanie układu równań
            public double[] GaussElimination(double[,] A, double[] b, int n)
            {
                double[] x = new double[n];

                double[,] tmpA = new double[n, n + 1];

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        tmpA[i, j] = A[i, j];
                    }
                    tmpA[i, n] = b[i];
                }

                double tmp = 0;

                for (int k = 0; k < n - 1; k++)
                {
                    for (int i = k + 1; i < n; i++)
                    {
                        tmp = tmpA[i, k] / tmpA[k, k];
                        for (int j = k; j < n + 1; j++)
                        {
                            tmpA[i, j] -= tmp * tmpA[k, j];
                        }
                    }
                }

                for (int k = n - 1; k >= 0; k--)
                {
                    tmp = 0;
                    for (int j = k + 1; j < n; j++)
                    {
                        tmp += tmpA[k, j] * x[j];
                    }
                    x[k] = (tmpA[k, n] - tmp) / tmpA[k, k];
                }

                return x;
            }

            public double CalculateFitnesse(double[] position)
            {
                wart_funkcjonalu = 0;

                #region Uwzględnienie odtwarzanych parametrów

                this.Alfa = position[0];
                this.Beta = position[1];

                this.war_brzeg_prawy = warunekBrzegowyRobin;

                #endregion

                // Główna pętla (przebiega po czasie)
                for (int m = 0; m < M; m++)
                {

                    // Komunikat na konsolę
                    if (m % 5 == 0)
                        Console.WriteLine((((double)m / (double)M) * 100).ToString() + "%");

                    #region Uzupełniamy macierz A i wektor wolny układu równań

                    for (int i = 1; i <= N; i++)
                    {
                        for (int j = 1; j <= N; j++)
                        {
                            if (i <= N - 1 && j <= i - 1)
                            {
                                macierzA[i - 1, j - 1] = -r * lambda(i * deltaX, (m + 1) * deltaT) * omega(beta, i - j + 1);
                            }

                            if (i <= N - 1 && j == i)
                            {
                                macierzA[i - 1, j - 1] = 1.0 - r * lambda(i * deltaX, (m + 1) * deltaT) * omega(beta, 1);
                            }

                            if (i <= N - 1 && j == i + 1)
                            {
                                macierzA[i - 1, j - 1] = -r * lambda(i * deltaX, (m + 1) * deltaT) * omega(beta, 0);
                            }

                            if (i <= N - 2 && j <= N && j >= i + 2)
                            {
                                macierzA[i - 1, j - 1] = 0;
                            }

                            if (i == N && j <= N - 3)
                            {
                                macierzA[i - 1, j - 1] = lambda(N * deltaX, (m + 1) * deltaT) * omega(beta - 1.0, N - j + 1);
                            }

                            if (i == N && j == N - 2)
                            {
                                macierzA[i - 1, j - 1] = lambda(N * deltaX, (m + 1) * deltaT) * omega(beta - 1.0, 3) + lambda(N * deltaX, (m + 1) * deltaT) * omega(beta - 1.0, 0);
                            }

                            if (i == N && j == N - 1)
                            {
                                macierzA[i - 1, j - 1] = lambda(N * deltaX, (m + 1) * deltaT) * omega(beta - 1.0, 2) - 3 * lambda(N * deltaX, (m + 1) * deltaT) * omega(beta - 1.0, 0);
                            }

                            if (i == N && j == N)
                            {
                                macierzA[i - 1, j - 1] = gamma * Math.Pow(deltaX, beta - 1.0) + lambda(N * deltaX, (m + 1) * deltaT) * omega(beta - 1.0, 1) + 3 * lambda(N * deltaX, (m + 1) * deltaT) * omega(beta - 1.0, 0);
                            }
                        }
                        if (m == 0) // przypadek dla pierwszej szukanej chwili czasu wygląda inaczej niż w pozostałych chwilach
                        {
                            if (i <= N - 1)
                            {
                                prawa[i - 1] = U[0, i] + Math.Pow(deltaT, alfa) * gammafunction(2.0 - alfa) * f(i * deltaX, deltaT);
                            }
                            else // tylko przypadek i = N
                            {
                                prawa[i - 1] = Math.Pow(deltaX, beta - 1.0) * war_brzeg_prawy(deltaT,
                                    position[2], position[3], position[4], position[5], position[6]);
                            }

                        }
                        else
                        {
                            double sumaTemp = 0;
                            for (int k = 1; k <= m - 1; k++)
                            {
                                sumaTemp += (funkcjaB(alfa, k) - funkcjaB(alfa, k + 1)) * U[m - k, i];
                            }

                            if (i <= N - 1)
                            {
                                prawa[i - 1] = (1.0 - funkcjaB(alfa, 1)) * U[m, i] + sumaTemp + funkcjaB(alfa, m) * U[0, i] + Math.Pow(deltaT, alfa) * gammafunction(2.0 - alfa) * f(i * deltaX, (m + 1) * deltaT);
                            }
                            else // tylko przypadek i = N
                            {
                                prawa[i - 1] = Math.Pow(deltaX, beta - 1.0) * war_brzeg_prawy((m + 1) * deltaT,
                                    position[2], position[3], position[4], position[5], position[6]);
                            }
                        }
                    }

                    #endregion


                    #region Rozwiązanie układu równań 


                    //for (int i = 0; i < N; i++)
                    //{
                    //    for (int j = 0; j < N; j++)
                    //    {
                    //        Console.Write(macierzA[i, j] + " ");
                    //    }
                    //    Console.WriteLine(" ");
                    //}

                    //Console.WriteLine("Prawa: ");



                    //for (int i = 0; i < N; i++)
                    //{
                    //    Console.Write(prawa[i] + " ");
                    //}

                    //Console.WriteLine(" ");

                    //int info;
                    //alglib.densesolverreport dsr;
                    //alglib.rmatrixsolve(macierzA, N, prawa, out info, out dsr, out wektorRoz);
                    //alglib.rmatrixsolvefast(macierzA, N, ref prawa, out info);
                    wektorRoz = GaussElimination(macierzA, prawa, N);
                    //BiCgSTAB(macierzA, prawa, N, x0, wektorRoz, 0.0000000001, 1000);

                    // Wpisanie rozwiązania do macierzy rozwiązań U
                    for (int i = 1; i <= N; i++)
                    {
                        U[m + 1, i] = wektorRoz[i - 1];
                    }

                    for (int i = 0; i < N; i++)
                    {
                        for (int j = 0; j < N; j++)
                        {
                            macierzA[i, j] = 0;
                        }
                        prawa[i] = 0;
                        wektorRoz[i] = 0;
                    }

                    #endregion
                }



                //-----------------------------//funkcjonał//------------------------------------

                //przygotowanie danych - odczyty temperatury z punktów pomiarowych
                for (int i = 0; i < liczba_termopar; i++)
                {
                    double[] x_temp = zwrocX(punkty_pomiarowe_ind[i]); //zwracamy wart temp w punkcie pomiarowym
                    for (int j = 0; j < ile_pomiarow; j++)
                    {
                        odczytane_temperatury[i, j] = x_temp[j];
                    }
                }

                wart_funkcjonalu = 0;
                //obliczenie wartości funkcjonału
                for (int i = 0; i < liczba_termopar; i++)
                {
                    for (int j = 0; j < ile_pomiarow; j++)
                    {
                        wart_funkcjonalu += (Math.Pow(Math.Abs(odczytane_temperatury[i, j] - dokladne[i, j]), 2));
                    }
                }

                //MessageBox.Show(wart_funkcjonalu.ToString());
                return wart_funkcjonalu;

                //---------------------------------------------------------------------------------------

                //ZapiszMacierzRozwiazanDoMathematiki("macierzU");
            }
        }
    }
