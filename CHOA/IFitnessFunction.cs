using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHOA
{
    public interface IFitnessFunction
    {
        double variation { get; }
        double CalculateFitnesse(double[] position);
    }
}
