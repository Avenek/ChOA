using System;
using System.Diagnostics;

namespace CHOA
{
    internal class Program
    {
        public static void TestAlgorithm(IFitnessFunction function)
        {
            Stopwatch stoper = new Stopwatch();
            stoper.Start();
            List<BestParameters> bestChimps = new List<BestParameters>();
            for (int iterations = 10; iterations < 100; iterations += 10)
            {
                for (int population = 10; population < 100; population += 10)
                {
                    for (double minM = 0; minM < 1.9; minM += 0.1)
                    {
                        for (double maxM = minM + 0.1; maxM < 2; maxM += 0.1)
                        {
                            List<BestParameters> bestX = new List<BestParameters>();
                            for (int iter = 0; iter < 10; iter++)
                            {
                                ChimpOptimizationAlgorithm CHOA = new ChimpOptimizationAlgorithm(population, 5, iterations, function, minM, maxM);
                                CHOA.Solve();
                                double distance = Math.Sqrt(CHOA.XBest.Sum(xi => Math.Pow(xi - 1, 2)));
                                BestParameters bestParam = new BestParameters(CHOA.xAttacker, minM, maxM, population, iterations, distance);
                                bestX.Add(bestParam);
                            }
                            bestX = bestX.OrderBy(chimp => chimp.distance).ToList();
                            bestChimps.Add(bestX[0]);
                        }
                    }
                }
            }
            bestChimps = bestChimps.OrderBy(param => param.distance).ToList();
            stoper.Stop();

            Console.WriteLine($"Populacja: {bestChimps[0].population}\nIteracji: {bestChimps[0].iterations}\n MinM: {bestChimps[0].minM}\n MaxM: {bestChimps[0].maxM}\n Coordinates: {String.Join(" ; ", bestChimps[0].chimp.coordinates)}\n Wartość: {bestChimps[0].chimp.fitness}\n\"Czas wykonania algorytmu: {stoper.Elapsed.TotalSeconds} s");
        }
        static void Main()
        {
            //Console.WriteLine("Rastrigin (0,0,0...)");
            /*for (int i = 0; i < 10; i++)
            {
                IFitnessFunction fitnessFunction = new Rastrigin();
                ChimpOptimizationAlgorithm CHOA = new ChimpOptimizationAlgorithm(50, 20, 100, fitnessFunction);
                CHOA.Solve();
                Console.WriteLine($"Wektor: {String.Join("; ", CHOA.XBest)}\nWartość: {CHOA.FBest}");
            }*/
            Console.WriteLine("-------------------------------------------\nRosenbrock (1,1,1,1...)");


            for (int i = 0; i < 20; i++)
            {
                IFitnessFunction fitnessFunction = new Rosenbrock();
                ChimpOptimizationAlgorithm CHOA = new ChimpOptimizationAlgorithm(70, 5, 20, fitnessFunction, 0.2, 0.4);
                CHOA.Solve();
                Console.WriteLine($"Wektor: {String.Join(" ; ", CHOA.xAttacker.coordinates)}\nWartość: {CHOA.xAttacker.fitness}");
            }

            //IFitnessFunction fitnessFunction = new Rosenbrock();
            //TestAlgorithm(fitnessFunction);




            /*Console.WriteLine("-------------------------------------------\nSpheare (0,0,0...)");
            for (int i = 10; i < 200; i+=10)
            {
                for(int j = 10; j < 100; j*=2)
                {
                    List<double> bestX = new List<double>();
                    for (double minM = 0.1; minM <1.9; minM += 0.1)
                    {
                        for(double maxM = minM +=0.1; maxM <2; maxM += 0.1)
                        { 
                            for (int iter = 0; iter < 10; iter++)
                            {
                                IFitnessFunction fitnessFunction = new Sphere();
                                ChimpOptimizationAlgorithm CHOA = new ChimpOptimizationAlgorithm(i, 5, j, fitnessFunction, minM, maxM);
                                CHOA.Solve();
                                bestX.Add(Math.Abs(Math.Sqrt(CHOA.XBest.Sum(xi => xi * xi))-1));

                            }
                        }
                    }
                    bestX.Sort();
                    Console.WriteLine($"Długość do minimum: {bestX[0]}");
                }
            }*/
            /*Console.WriteLine("-------------------------------------------\nBeale (3, 0.5)");
            for (int i = 10; i < 200; i += 10)
            {
                for (int j = 10; j < 100; j *= 2)
                {
                    List<double> bestX = new List<double>();
                    for (double minM = 0.1; minM < 1.9; minM += 0.1)
                    {
                        for (double maxM = minM += 0.1; maxM < 2; maxM += 0.1)
                        {
                            for (int iter = 0; iter < 10; iter++)
                            {
                                IFitnessFunction fitnessFunction = new Beale();
                                ChimpOptimizationAlgorithm CHOA = new ChimpOptimizationAlgorithm(i, 2, j, fitnessFunction, minM, maxM);
                                CHOA.Solve();
                                bestX.Add(Math.Sqrt(CHOA.XBest.Sum(xi => xi * xi)) - 3.04138);
                            }
                        }
                    }
                    bestX.Sort();
                    Console.WriteLine($"Długość do minimum: {bestX[0]}");
                }
            }*/
            /*Console.WriteLine("-------------------------------------------\nHimmeblau (3, 2) lub (-2.805118, 3.131312) lub (-3.779310, -3.283186) lub (3.584428, -1.848126)");
            for (int i = 0; i < 10; i++)
            {
                IFitnessFunction fitnessFunction = new Himmelblau();
                ChimpOptimizationAlgorithm CHOA = new ChimpOptimizationAlgorithm(80, 2, 100, fitnessFunction, 1.5);
                CHOA.Solve();
                Console.WriteLine($"Wektor: {String.Join("; ", CHOA.XBest)}\nWartość: {CHOA.FBest}");
            }*/
        }
    }
}