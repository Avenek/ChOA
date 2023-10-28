using System;
using System.Diagnostics;
using System.Text;
using static System.Formats.Asn1.AsnWriter;

namespace CHOA
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Rastrigin (0,0,0...)");
            IFitnessFunction fitnessFunction = new Rastrigin();
            Raport raport = new Raport("Rastrigin", 5, 40, 80, fitnessFunction);
            raport.TestAlgorithm();
            Raport raport2 = new Raport("Rastrigin", 20, 60, 100, fitnessFunction);
            raport2.TestAlgorithm();
            Console.WriteLine("-------------------------------------------\nRosenbrock (1,1,1,1...)");

            fitnessFunction = new Rosenbrock();
            raport = new Raport("Rosenbrock", 3, 40, 80, fitnessFunction);
            raport.TestAlgorithm();
            raport2 = new Raport("Rosenbrock", 15, 60, 100, fitnessFunction);
            raport2.TestAlgorithm();

            Console.WriteLine("-------------------------------------------\nSphere (0,0,0...)");
            fitnessFunction = new Sphere();
            raport = new Raport("Sphere", 10, 30, 60, fitnessFunction);
            raport.TestAlgorithm();
            raport2 = new Raport("Sphere", 20, 80, 100, fitnessFunction);
            raport2.TestAlgorithm();

            Console.WriteLine("-------------------------------------------\nBeale (3, 0.5)");
            fitnessFunction = new Beale();
            raport = new Raport("Beale", 2, 40, 60, fitnessFunction);
            raport.TestAlgorithm();
            raport2 = new Raport("Beale", 2, 80, 100, fitnessFunction);
            raport2.TestAlgorithm();

            Console.WriteLine("-------------------------------------------\nHimmeblau (3, 2) lub (-2.805118, 3.131312) lub (-3.779310, -3.283186) lub (3.584428, -1.848126)");
            fitnessFunction = new Himmelblau();
            raport = new Raport("HimmeBlau", 2, 30, 60, fitnessFunction);
            raport.TestAlgorithm();
            raport2 = new Raport("Himmeblau", 2, 80, 100, fitnessFunction);
            raport2.TestAlgorithm();

            Console.WriteLine("-------------------------------------------\nBukin (-10, 1)");
            fitnessFunction = new Bukin();
            raport = new Raport("Bukin", 2, 30, 50, fitnessFunction);
            raport.TestAlgorithm();
            raport2 = new Raport("Bukin", 2, 70, 90, fitnessFunction);
            raport2.TestAlgorithm();
        }
    }
}