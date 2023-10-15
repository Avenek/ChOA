namespace CHOA
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Rastrigin (0,0,0...)");
            for (int i = 0; i < 10; i++)
            {
                IFitnessFunction fitnessFunction = new Rastrigin();
                ChimpOptimizationAlgorithm CHOA = new ChimpOptimizationAlgorithm(50, 5, 100, fitnessFunction);
                CHOA.Solve();
                Console.WriteLine($"Wektor: {String.Join("; ", CHOA.XBest)}\nWartość: {CHOA.FBest}");
            }
            Console.WriteLine("-------------------------------------------\nRosenbrock (1,1,1,1...)");
            for (int i = 0; i < 10; i++)
            {
                IFitnessFunction fitnessFunction = new Rosenbrock();
                ChimpOptimizationAlgorithm CHOA = new ChimpOptimizationAlgorithm(60, 5, 500, fitnessFunction);
                CHOA.Solve();
                Console.WriteLine($"Wektor: {String.Join("; ", CHOA.XBest)}\nWartość: {CHOA.FBest}");
            }
            Console.WriteLine("-------------------------------------------\nSpheare (0,0,0...)");
            for (int i = 0; i < 10; i++)
            {
                IFitnessFunction fitnessFunction = new Sphere();
                ChimpOptimizationAlgorithm CHOA = new ChimpOptimizationAlgorithm(50, 8, 1000, fitnessFunction);
                CHOA.Solve();
                Console.WriteLine($"Wektor: {String.Join("; ", CHOA.XBest)}\nWartość: {CHOA.FBest}");
            }
            Console.WriteLine("-------------------------------------------\nBeale (3, 0.5)");
            for (int i = 0; i < 10; i++)
            {
                IFitnessFunction fitnessFunction = new Beale();
                ChimpOptimizationAlgorithm CHOA = new ChimpOptimizationAlgorithm(50, 2, 100, fitnessFunction);
                CHOA.Solve();
                Console.WriteLine($"Wektor: {String.Join("; ", CHOA.XBest)}\nWartość: {CHOA.FBest}");
            }
            Console.WriteLine("-------------------------------------------\nHimmeblau (3, 2) lub (-2.805118, 3.131312) lub (-3.779310, -3.283186) lub (3.584428, -1.848126)");
            for (int i = 0; i < 10; i++)
            {
                IFitnessFunction fitnessFunction = new Himmelblau();
                ChimpOptimizationAlgorithm CHOA = new ChimpOptimizationAlgorithm(80, 2, 100, fitnessFunction);
                CHOA.Solve();
                Console.WriteLine($"Wektor: {String.Join("; ", CHOA.XBest)}\nWartość: {CHOA.FBest}");
            }
        }
    }
}