using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHOA
{
    public interface IStateWriter
    {
        void SaveToFileStateOfAlghoritm(string path);

    }

    public class StateWriter : IStateWriter
    {
        public StateWriter() { }
        public void SaveToFileStateOfAlghoritm(string path)
        {

        }
    }
}
