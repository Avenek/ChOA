

namespace CHOA
{
    internal class Program
    {
        static void Main()
        {
            Rastrigin f = new Rastrigin();
            double[,] domain = new double[5, 2];
            for (int i = 0; i < 5; i++)
            { 
                domain[i, 0] = -1000000;
                domain[i, 1] = 1000000;
            }
            for(int i = 0;i < 5; i++)
            {
                ChimpOptimizationAlgorithm choa = new ChimpOptimizationAlgorithm();
                double[] parameters = new double[] { 50, 5, 70, 0, 1, 0, 1 };
                choa.Solve(f, domain, parameters, false);
                Console.WriteLine(String.Join(" ; ", choa.XBest));
                Console.WriteLine(choa.FBest);
            }
            

        }
    }
}