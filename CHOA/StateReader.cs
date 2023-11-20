using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

namespace CHOA
{
    public interface IStateReader
    {
        StateReader LoadFromFileStateOfAlghoritm(string path);
    }
    public class StateReader : IStateReader
    {
        public int Iteration { get; set; }
        public List<Chimp> Population;
        public double FBest;
        public int NumberOfEvaluationFitnessFunction;
        public StateReader() { }
        public StateReader LoadFromFileStateOfAlghoritm(string path)
        {
            string jsonData = File.ReadAllText(path);
            var data = JsonConvert.DeserializeObject<StateReader>(jsonData);
            Iteration = data.Iteration;
            Population = data.Population;
            FBest = data.FBest;
            NumberOfEvaluationFitnessFunction = data.NumberOfEvaluationFitnessFunction;
            return data;
        }
    }

}
