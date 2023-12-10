using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHOA.TestFunctions
{
    public class Beale : IFitnessFunction
    {
        public double variation { get { return 4.5; } }
        public double CalculateFitnesse(double[] position)
        {
            return Math.Pow(1.5 - position[0] + position[0] * position[1], 2) + Math.Pow(2.25 - position[0] + position[0] * position[1] * position[1], 2) + Math.Pow(2.625 - position[0] + position[0] * position[1] * position[1] * position[1], 2);
        }
    }
}
