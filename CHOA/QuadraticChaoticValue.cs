using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHOA
{
    internal class QuadraticChaoticValue
    {
        public double x0;
        public double c;
        public double value;

        public QuadraticChaoticValue()
        {
            this.x0 = 0.9;
            this.c = 1;
            this.value = 0;
        }

        public void Calculate()
        {
            double xi_plus_1 = x0 * x0 - c;
            value = Math.Abs(xi_plus_1 - x0); 
            x0 = xi_plus_1;
        }
    }
}
