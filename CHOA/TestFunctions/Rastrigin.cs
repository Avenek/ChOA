using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHOA
{
    public class Rastrigin : IFitnessFunction
    {
        public double variation { get { return 5.12; } }
        public double CalculateFitnesse(double[] position)
        {
            return 10*position.Length + position.Sum(xi => (xi * xi) - 10*Math.Cos(2*Math.PI*xi));
        }
    }
}
