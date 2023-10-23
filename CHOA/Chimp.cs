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
        public int strategy;
        public double f;
        public double m = 0.7;
        public double a;
        public double c;
        public double minM;
        public double maxM;

        private static Random random = new Random();

        public Chimp(int dimension, double minM, double maxM)
        {
            coordinates = new double[dimension];
            this.dimension = dimension;
            this.minM = minM;
            this.maxM = maxM;
        }
        public void CalculateF(int currentIteration, int maxIteration)
        {
            switch (strategy)
            {
                case 0:
                    f = 2.5 - (2 * Math.Log(currentIteration) / Math.Log(maxIteration));
                    break;
                case 1:
                    f = (-2 * Math.Pow(currentIteration, 3) / Math.Pow(maxIteration, 3)) + 2.5;
                    break;
                case 2:
                    f = 0.5 + 2 * Math.Exp(-1 * Math.Pow((4 * currentIteration / maxIteration), 2));
                    break;
                default:
                    f = 2.5 + 2 * Math.Pow((currentIteration / maxIteration), 2) - 2 * (2 * currentIteration / maxIteration);
                    break;
            }
        }
        public void CalculateM()
        {
            m = 1.07 * (7.86 * m - 23.31 * m * m + 28.75 * m * m * m - 13.302875 * m * m * m * m);
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
                d[i] = chimp.c * chimp.coordinates[i] - chimp.m * coordinates[i];
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
                coordinates[i] *= random.NextDouble()* (maxM - minM) + minM;
            }
        }

        public void setGroup(int group)
        {
            strategy = group;
        }
    }
}
