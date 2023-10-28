using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHOA
{
    internal class BestParameters
    {
        public double minM;
        public double maxM;
        public double minC;
        public double maxC;
        public double[] stdX;
        public double stdY;
        public double[] coeffX;
        public double coeffY;
        public Chimp chimp;

        public BestParameters(Chimp chimp, double minM, double maxM, double minC, double maxC, double[] stdX, double stdY, double[] coeffX, double coeffY)
        {
            this.minM = minM;
            this.maxM = maxM;
            this.chimp = chimp;
            this.minC = minC;
            this.maxC = maxC;
            this.stdX = stdX;
            this.stdY = stdY;
            this.coeffX = coeffX;
            this.coeffY = coeffY;
        }
    }
}
