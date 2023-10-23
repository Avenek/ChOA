namespace CHOA
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Rastrigin (0,0,0...)");
            /*for (int i = 0; i < 10; i++)
            {
                IFitnessFunction fitnessFunction = new Rastrigin();
                ChimpOptimizationAlgorithm CHOA = new ChimpOptimizationAlgorithm(50, 20, 100, fitnessFunction);
                CHOA.Solve();
                Console.WriteLine($"Wektor: {String.Join("; ", CHOA.XBest)}\nWartość: {CHOA.FBest}");
            }*/
            /*Console.WriteLine("-------------------------------------------\nRosenbrock (1,1,1,1...)");
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
                                IFitnessFunction fitnessFunction = new Rosenbrock();
                                ChimpOptimizationAlgorithm CHOA = new ChimpOptimizationAlgorithm(i, 5, j, fitnessFunction, minM, maxM);
                                CHOA.Solve();
                                bestX.Add(Math.Abs(Math.Sqrt(CHOA.XBest.Sum(xi => xi * xi)) - 1));

                            }
                        }
                    }
                    bestX.Sort();
                    Console.WriteLine($"Długość do minimum: {bestX[0]}");
                }
            }*/
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
            Console.WriteLine("-------------------------------------------\nBeale (3, 0.5)");
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
                                bestX.Add(Math.Abs(Math.Sqrt(CHOA.XBest.Sum(xi => xi * xi)) - 3.04138));
                            }
                        }
                    }
                    bestX.Sort();
                    Console.WriteLine($"Długość do minimum: {bestX[0]}");
                }
            }
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