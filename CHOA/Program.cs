
using CHOA.TestFunctions;

namespace CHOA
{
    internal class Program
    {
        static void Main()
        {
            Sphere f = new Sphere();

            for(int i = 0;i < 5; i++)
            {
                ChimpOptimizationAlgorithm choa = new ChimpOptimizationAlgorithm();
                double[] parameters = new double[] { 50, 2, 70, 0.2, 0.4, 0.2, 0.4 };
                choa.Solve(f, new double[,] { { -1000, 1000}, { -1000, 1000} }, parameters, false);
                Console.WriteLine(String.Join(" ; ", choa.XBest));
                Console.WriteLine(choa.FBest);
            }
        }
    }
}