using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHOA
{
    public class Bukin : IFitnessFunction
    {
        public double variation { get { return 0; } }
        public double CalculateFitnesse(double[] position)
        {
            return 100 * Math.Sqrt(Math.Abs(position[1] - 0.01 * position[0] * position[0])) + 0.01 * Math.Abs(position[0] + 10);
        }
    }
}
