
using CHOA.TestFunctions;

namespace CHOA
{
    internal class Program
    {
        static void Main()
        {
            TSFDE f = new TSFDE();
            double[,] domain = new double[7, 2] { {0.1, 0.9 }, {1.1, 1.9 }, { 1, 5 }, {-70, -20 }, {250,450 }, {-30,-10 }, {50,250 } };
            for(int i = 0;i < 5; i++)
            {
                ChimpOptimizationAlgorithm choa = new ChimpOptimizationAlgorithm();
                double[] parameters = new double[] { 50, 7, 70, 1, 1, 1, 1 };
                choa.Solve(f, domain, parameters, false);
                Console.WriteLine(String.Join(" ; ", choa.XBest));
                Console.WriteLine(choa.FBest);
            }
        }
    }
}