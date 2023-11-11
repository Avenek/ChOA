public delegate double fitnessFunction(params double[] arg);

namespace CHOA
{
    internal class Program
    {
        static void Main()
        {
            Func<double[], double> f = Sphere;
            double[,] domain = new double[5, 2];
            for (int i = 0; i < 5; i++)
            { 
                domain[i, 0] = -1000000;
                domain[i, 1] = 1000000;
            }
            for(int i = 0;i < 5; i++)
            {
                ChimpOptimizationAlgorithm choa = new ChimpOptimizationAlgorithm();
                choa.Solve(f, domain, 50, 5, 70, 0, 1, 0, 1);
                Console.WriteLine(String.Join(" ; ", choa.XBest));
                Console.WriteLine(choa.FBest);
            }
            

        }

        static double Sphere(double[] x)
        {
            return x.Sum(xi => Math.Pow(xi, 2));
        }
    }
}