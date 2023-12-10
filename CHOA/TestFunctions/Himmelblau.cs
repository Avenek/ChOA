using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHOA
{
    public class Himmelblau : IFitnessFunction
    {
        public double variation { get { return 5; } }
        public double CalculateFitnesse(double[] position)
        {
            return Math.Pow(position[0] * position[0] + position[1] - 11, 2) + Math.Pow(position[0] + position[1] * position[1] - 7, 2);
        }
    }
}
