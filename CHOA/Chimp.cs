using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CHOA
{
    internal class Chimp
    {
        public double[] coordinates;
        public int dimension;
        public double fitness;
        int strategy;
        public double f;
        QuadraticChaoticValue m = new QuadraticChaoticValue();
        public double a;
        double c;

        private static Random random = new Random();

        public Chimp(int dimension)
        {
            coordinates = new double[dimension];
            this.dimension = dimension;
        }
        public double CalculateFTwo(int currentIteration, int maxIteration)
        {
            switch (strategy)
            {
                case 1:
                    return 2.5 - (2 * Math.Log(currentIteration) / Math.Log(maxIteration));
                case 2:
                    return (-2 * Math.Pow(currentIteration, 3) / Math.Pow(maxIteration, 3)) + 2.5;
                case 3:
                    return 0.5 + 2 * Math.Exp(-1 * Math.Pow((4 * currentIteration / maxIteration), 2));
                default:
                    return 2.5 + 2 * Math.Pow((currentIteration / maxIteration), 2) - 2 * (2 * currentIteration / maxIteration);
            }
        }
        public void CalculateM()
        {
            m.Calculate();
        }
        public void CalculateA()
        {
           a = 2 * f * random.NextDouble() - f;
        }

       public void CalculateC()
        {
            c = 2 * random.NextDouble();
        }

        public double CalculateD(Chimp chimp)
        {
            double[] d = new double[dimension];
            for (int i = 0; i < dimension; i++)
            {
                d[i] = chimp.c * chimp.coordinates[i] - chimp.m.value * coordinates[i];
            }
            return Math.Sqrt(d.Sum(xi=>xi* xi));
        }

        public double[] CalculateX(Chimp chimp, double d)
        {
            double[] x = new double[dimension];
            for (int i = 0; i < dimension; i++)
            {
                x[i] = chimp.coordinates[i] - chimp.a * d;
            }
            return x;
        }
        public void UpdatePositionExplore(Chimp xAttacker, Chimp xChaser, Chimp xBarrier, Chimp xDriver)
        {

            double dAttacker = CalculateD(xAttacker);
            double dChaser = CalculateD(xChaser);
            double dBarrier = CalculateD(xBarrier);
            double dDriver = CalculateD(xDriver);

            double[] x1 = CalculateX(xAttacker, dAttacker);
            double[] x2 = CalculateX(xChaser, dChaser);
            double[] x3 = CalculateX(xBarrier, dBarrier);
            double[] x4 = CalculateX(xDriver, dDriver);

            for (int i = 0; i < dimension; i++)
            {
               coordinates[i] = (x1[i] + x2[i] + x3[i] + x4[i])/4;
            }
        }

        public void UpdatePositionChaoticValue()
        {
            for (int i = 0; i < dimension; i++)
            {
                coordinates[i] *= m.value;
            }
        }

        public void setGroup(int group)
        {
            strategy = group;
        }
    }
}
