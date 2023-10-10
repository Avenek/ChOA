namespace CHOA
{
    internal class Program
    {
        static void Main()
        {
            /*for(int i = 0; i< 10; i++)
            {
                IFitnessFunction fitnessFunction = new Rastrigin();
                ChimpOptimizationAlgorithm CHOA = new ChimpOptimizationAlgorithm(50, 5, 100, fitnessFunction);
                CHOA.Solve();
                Console.WriteLine($"Wektor: {String.Join("; ", CHOA.XBest)}\nWartość: {CHOA.FBest}");
            }*/

            /*for (int i = 0; i < 10; i++)
            {
                IFitnessFunction fitnessFunction = new Rosenbrock();
                ChimpOptimizationAlgorithm CHOA = new ChimpOptimizationAlgorithm(50, 2, 100, fitnessFunction);
                CHOA.Solve();
                Console.WriteLine($"Wektor: {String.Join("; ", CHOA.XBest)}\nWartość: {CHOA.FBest}");
            }
            */

            for (int i = 0; i < 10; i++)
            {
                IFitnessFunction fitnessFunction = new circle();
                ChimpOptimizationAlgorithm CHOA = new ChimpOptimizationAlgorithm(50, 5, 100, fitnessFunction);
                CHOA.Solve();
                Console.WriteLine($"Wektor: {String.Join("; ", CHOA.XBest)}\nWartość: {CHOA.FBest}");
            }
        }
    }
}