namespace CHOA
{
    internal class Program
    {
        static void Main()
        {
            for (int i = 0; i < 10; i++)
            {
                IFitnessFunction fitnessFunction = new Rastrigin();
                ChimpOptimizationAlgorithm CHOA = new ChimpOptimizationAlgorithm(50, 5, 100, fitnessFunction);
                CHOA.Solve();
                Console.WriteLine($"Wektor: {String.Join("; ", CHOA.XBest)}\nWartość: {CHOA.FBest}");
            }
            Console.WriteLine("-------------------------------------------");
            for (int i = 0; i < 10; i++)
            {
                IFitnessFunction fitnessFunction = new Rosenbrock();
                ChimpOptimizationAlgorithm CHOA = new ChimpOptimizationAlgorithm(60, 5, 500, fitnessFunction);
                CHOA.Solve();
                Console.WriteLine($"Wektor: {String.Join("; ", CHOA.XBest)}\nWartość: {CHOA.FBest}");
            }
            Console.WriteLine("-------------------------------------------");
            for (int i = 0; i < 10; i++)
            {
                IFitnessFunction fitnessFunction = new Sphere();
                ChimpOptimizationAlgorithm CHOA = new ChimpOptimizationAlgorithm(50, 8, 1000, fitnessFunction);
                CHOA.Solve();
                Console.WriteLine($"Wektor: {String.Join("; ", CHOA.XBest)}\nWartość: {CHOA.FBest}");
            }
            Console.WriteLine("-------------------------------------------");
            for (int i = 0; i < 10; i++)
            {
                IFitnessFunction fitnessFunction = new Beale();
                ChimpOptimizationAlgorithm CHOA = new ChimpOptimizationAlgorithm(50, 2, 100, fitnessFunction);
                CHOA.Solve();
                Console.WriteLine($"Wektor: {String.Join("; ", CHOA.XBest)}\nWartość: {CHOA.FBest}");
            }
        }
    }
}