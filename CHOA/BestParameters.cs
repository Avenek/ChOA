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
        public Chimp chimp;
        public int population;
        public int iterations;
        public double distance;

        public BestParameters(Chimp chimp, double minM, double maxM, int population, int iterations, double distance)
        {
            this.minM = minM;
            this.maxM = maxM;
            this.chimp = chimp;
            this.population = population;
            this.iterations = iterations;
            this.distance = distance;
        }
    }
}
