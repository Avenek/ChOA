using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHOA
{
    internal class Rosenbrock : IFitnessFunction
    {
        public double variation { get { return 10000000000; } }
        public double CalculateFitnesse(double[] position)
        {
            double value = 0;
            for(int i = 0; i < position.Length-1; i++) 
            {
                value += 100 * Math.Pow(position[i + 1] - position[i] * position[i], 2) + (Math.Pow(1 - position[i], 2));
            }
            return value;
        }
    }
}
