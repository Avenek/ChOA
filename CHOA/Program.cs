﻿namespace CHOA
{
    internal class Program
    {
        static void Main()
        {
            //for(int i = 0; i< 10; i++)
            //{
            //    IFitnessFunction fitnessFunction = new Rastrigin();
            //    ChimpOptimizationAlgorithm CHOA = new ChimpOptimizationAlgorithm(50, 5, 1000, fitnessFunction);
            //    CHOA.Solve();
            //    Console.WriteLine($"Wektor: {String.Join("; ", CHOA.XBest)}\nWartość: {CHOA.FBest}");
            //}

            //for (int i = 0; i < 10; i++)
            //{
            //    IFitnessFunction fitnessFunction = new Rosenbrock();
            //    ChimpOptimizationAlgorithm CHOA = new ChimpOptimizationAlgorithm(50, 5, 500, fitnessFunction);
            //    CHOA.Solve();
            //    Console.WriteLine($"Wektor: {String.Join("; ", CHOA.XBest)}\nWartość: {CHOA.FBest}");
            //}

            double x = 0.7;
            for(int i = 0; i <100; i++)
            {
                Console.WriteLine(x);
                x = x * x - 1;
            }
        }
    }
}