using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHOA
{
    internal class Sphere : IFitnessFunction
    {
        public double variation { get { return 100000000; } }
        public double CalculateFitnesse(double[] position)
        {
            return position.Sum(xi => Math.Pow(xi,2));
        }
    }
}
