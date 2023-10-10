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
        double f;
        QuadraticChaoticValue m = new QuadraticChaoticValue();
        double a;
        double c;
        double[] d;

        private static Random random = new Random();

        public Chimp(int dimension)
        {
            coordinates = new double[dimension];
            this.dimension = dimension;
        }

        public void CalculateF(int currentIteration, int maxIteration)
        {
            switch (strategy)
            {
                case 1:
                    f = 1.95 - 2 * (Math.Pow(currentIteration, 1 / 4)) / Math.Pow(maxIteration, 1 / 3);
                    break;
                case 2:
                    f = 1.95 - 2 * (Math.Pow(currentIteration, 1 / 3)) / Math.Pow(maxIteration, 1 / 4);
                    break;
                case 3:
                    f = (-3 * Math.Pow(currentIteration, 3) / Math.Pow(maxIteration, 3)) + 1.5;
                    break;
                default:
                    f = (-2 * Math.Pow(currentIteration, 3) / Math.Pow(maxIteration, 3)) + 1.5;
                    break;
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

        public double[] CalculateD(Chimp chimp)
        {
            double[] d = new double[dimension];
            for (int i = 0; i < dimension; i++)
            {
                d[i] = chimp.c * chimp.coordinates[i] - chimp.m.value * coordinates[i];
            }
            return d;
        }

        public double[] CalculateX(Chimp chimp, double[] d)
        {
            double[] x = new double[dimension];
            for (int i = 0; i < dimension; i++)
            {
                x[i] = chimp.coordinates[i] - chimp.a * d[i];
            }
            return x;
        }
        public void UpdatePositionExplore(Chimp xAttacker, Chimp xChaser, Chimp xBarrier, Chimp xDriver)
        {
            double[] newPosition = new double[dimension];

            double[] dAttacker = CalculateD(xAttacker);
            double[] dChaser = CalculateD(xChaser);
            double[] dBarrier = CalculateD(xBarrier);
            double[] dDriver = CalculateD(xDriver);

            double[] x1 = CalculateX(xAttacker, dAttacker);
            double[] x2 = CalculateX(xChaser, dChaser);
            double[] x3 = CalculateX(xBarrier, dBarrier);
            double[] x4 = CalculateX(xDriver, dDriver);

            for (int i = 0; i < dimension; i++)
            {
                newPosition[i] = (x1[i] + x2[i] + x3[i] + x4[i])/4;
            }

            coordinates = newPosition;
        }

        /*public double[] UpdatePositionChaoticValue()
        {
            var newPosition = new double[dimension];

            for (int i = 0; i < dimension; i++)
            {
                m.Calculate();
                newPosition[i] = m.value;
            }
            return newPosition;
        }*/

        public void setGroup(int group)
        {
            strategy = group;
        }
    }
}
