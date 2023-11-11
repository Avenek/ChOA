using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHOA.TestFunctions;

namespace CHOA
{
    public class ParamInfo
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public double UpperBoundary { get; set; }
        public double LowerBoundary { get; set; }
    }
}
