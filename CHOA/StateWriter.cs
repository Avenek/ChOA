using Newtonsoft.Json;
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
        public int Iteration;
        public List<Chimp> Population;
        public double FBest;
        public int NumberOfEvaluationFitnessFunction;
        public StateWriter(int iteration, List<Chimp> population, double fBest, int numberOfEvaluationFitnessFunction)
        {
            Iteration = iteration;
            Population = population;
            FBest = fBest;
            NumberOfEvaluationFitnessFunction = numberOfEvaluationFitnessFunction;
        }
        public void SaveToFileStateOfAlghoritm(string path)
        {
            var Data = new
            {
                iteration = Iteration,
                population = Population,
                fBest = FBest,
            };
            string jsonData = JsonConvert.SerializeObject(Data, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                TypeNameHandling = TypeNameHandling.None
            });
            File.WriteAllText(path, jsonData);
        }
    }
}
