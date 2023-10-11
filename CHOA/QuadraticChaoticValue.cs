using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHOA
{
    internal class QuadraticChaoticValue
    {
        public double c;
        public double value;

        public QuadraticChaoticValue()
        {
            this.c = 1;
            this.value = 0.7;
        }

        public void Calculate()
        {
            value = value * value - c;
            if (!(value > 0)) { value = 0.7; }
        }
    }
}
