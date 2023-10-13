namespace CHOA
{
    internal class Program
    {
        static void Main()
        {
            for (int i = 0; i < 10; i++)
            {
                IFitnessFunction fitnessFunction = new Rastrigin();
                ChimpOptimizationAlgorithm CHOA = new ChimpOptimizationAlgorithm(50, 5, 1000, fitnessFunction);
                CHOA.Solve();
                Console.WriteLine($"Wektor: {String.Join("; ", CHOA.XBest)}\nWartość: {CHOA.FBest}");
            }
            Console.WriteLine("-------------------------------------------");
            for (int i = 0; i < 10; i++)
            {
                IFitnessFunction fitnessFunction = new Rosenbrock();
                ChimpOptimizationAlgorithm CHOA = new ChimpOptimizationAlgorithm(50, 5, 500, fitnessFunction);
                CHOA.Solve();
                Console.WriteLine($"Wektor: {String.Join("; ", CHOA.XBest)}\nWartość: {CHOA.FBest}");
            }
            Console.WriteLine("-------------------------------------------");
            for (int i = 0; i < 10; i++)
            {
                IFitnessFunction fitnessFunction = new Beale();
                ChimpOptimizationAlgorithm CHOA = new ChimpOptimizationAlgorithm(50, 2, 500, fitnessFunction);
                CHOA.Solve();
                Console.WriteLine($"Wektor: {String.Join("; ", CHOA.XBest)}\nWartość: {CHOA.FBest}");
            }
        }
    }
}