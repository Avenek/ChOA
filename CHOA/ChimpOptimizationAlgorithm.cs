namespace CHOA
{
    public class ChimpOptimizationAlgorithm : IOptimizationAlgorithm
    {
        int populationSize;
        int dimension;
        int maxIteration;

        int currentIteration = 1;

        public string Name { get; set; }
        public double[] XBest { get; set; }
        public double FBest { get; set; }
        public int NumberOfEvaluationFitnessFunction { get; set; }
        public ParamInfo[] ParamsInfo { get; set; }
        public IStateWriter Writer { get; set; }
        public IStateReader Reader { get; set; }
        public IGenerateTextReport StringReportGenerator { get; set; }
        public IGeneratePDFReport PdfReportGenerator { get; set; }

        private static Random random = new Random();

        List<Chimp> population = new List<Chimp>();

        Chimp xAttacker;
        Chimp xChaser;
        Chimp xBarrier;
        Chimp xDriver;

        public ChimpOptimizationAlgorithm()
        {
            Reader = new StateReader();
            ParamInfo populationSize = new ParamInfo("PopulationSize", "Liczba populacji", 10, 100);
            ParamInfo dimension = new ParamInfo("Dimension", "Wymiar szukanych rozwiązań", 1, 30);
            ParamInfo maxIteration = new ParamInfo("MaxIteration", "Całkowita liczba iteracji, po której algorytm zakończy działanie", 10, 100);
            ParamInfo minM = new ParamInfo("minM", "Parametr służący do aktualizacji pozycji za pomocą chaotycznej wartości", 0, 1.9);
            ParamInfo maxM = new ParamInfo("maX", "Parametr służący do aktualizacji pozycji za pomocą chaotycznej wartości", 0.1, 2);
            ParamInfo minC = new ParamInfo("minC", "Parametr służący do wyznaczania odległości", 0, 1.9);
            ParamInfo maxC = new ParamInfo("maxC", "Parametr służący do wyznaczania odległości", 0.1, 2);

            ParamsInfo = new ParamInfo[] { populationSize, dimension, maxIteration, minM, maxM, minC, maxC};
        }

        public void Solve(dynamic f, double[,] domain, params double[] parameters)
        {
            populationSize = (int)parameters[0];
            dimension = (int)parameters[1];
            maxIteration = (int)parameters[2];
            double minM = parameters[3];
            double maxM = parameters[4];
            double minC = parameters[5];
            double maxC = parameters[6];
            InitializePopulation(domain);
            CalculateFitnessForEachChimp(f);
            ChooseBestAgents();
            DivideChimpsIntoGroups();
            while (currentIteration <= maxIteration)
            {
                foreach (Chimp chimp in population)
                {
                    CalculateParameters(chimp, minM, maxM, minC, maxC);
                    UpdateChimpPositionByRules(chimp, domain);
                    chimp.fitness = f.CalculateFitnesse(chimp.coordinates);
                    NumberOfEvaluationFitnessFunction++;
                }
                ChooseBestAgents();
                UpdateBestChimpsPosition();
                currentIteration++;
                FBest = f.CalculateFitnesse(xAttacker.coordinates);
                Writer = new StateWriter(currentIteration, population, FBest, NumberOfEvaluationFitnessFunction);
                //Writer.SaveToFileStateOfAlghoritm("path");
            }
            FBest = f.CalculateFitnesse(xAttacker.coordinates);
            XBest = xAttacker.coordinates;
        }

        private void InitializePopulation(double[,] domain)
        {
            for (int i = 0; i < populationSize; i++)
            {
                Chimp chimp = new Chimp(dimension);
                for (int j = 0; j < dimension; j++)
                {
                    chimp.coordinates[j] = random.NextDouble() * (domain[j,1] - domain[j, 0]) + domain[j, 0];
                    
                }
                population.Add(chimp);
            }
        }

        private void CalculateFitnessForEachChimp(dynamic f)
        {
            foreach (Chimp chimp in population)
            {
                chimp.fitness = f.CalculateFitnesse(chimp.coordinates);
                NumberOfEvaluationFitnessFunction++;
            }
        }

        private void DivideChimpsIntoGroups()
        {
            int numberOfGroups = 4;
            var shuffledPopulation = population.OrderBy(x => random.Next()).ToList();
            int groupSize = population.Count / numberOfGroups;

            for (int i = 0; i < numberOfGroups; i++)
            {
                var group = shuffledPopulation.Skip(i * groupSize).Take(groupSize).ToList();
                foreach(Chimp chimp in group)
                {
                    chimp.SetGroup(i);
                }
            }
        }

        public void ChooseBestAgents()
        {
            List<Chimp> sortedChimps = population.OrderBy(chimp => chimp.fitness).ToList();
            InitializeBestAgents(sortedChimps);
        }

        private void InitializeBestAgents(List<Chimp> chimps)
        {
            xAttacker = chimps[0];
            xChaser = chimps[1];
            xBarrier = chimps[2];
            xDriver = chimps[3];
        }

        public void CalculateParameters(Chimp chimp, double minM, double maxM, double minC, double maxC)
        {
            chimp.CalculateF(currentIteration, maxIteration);
            chimp.CalculateM(minM, maxM);
            chimp.CalculateA();
            chimp.CalculateC(minC, maxC);
        }




        public void UpdateBestChimpsPosition()
        {
            double dAttacker = xAttacker.CalculateD(xAttacker);
            double dChaser = xChaser.CalculateD(xChaser);
            double dBarrier = xBarrier.CalculateD(xBarrier);
            double dDriver = xDriver.CalculateD(xDriver);
            xAttacker.coordinates = xAttacker.CalculateX(xAttacker, dAttacker);
            xChaser.coordinates = xChaser.CalculateX(xChaser, dChaser);
            xBarrier.coordinates = xBarrier.CalculateX(xBarrier, dBarrier);
            xDriver.coordinates = xDriver.CalculateX(xDriver, dDriver);
        }

        public void UpdateChimpPositionByRules(Chimp chimp, double[,] domain)
        {
            if (random.NextDouble() > 0.5)
            {
                if (Math.Abs(chimp.a) < 1)
                {
                    chimp.UpdatePositionExplore(xAttacker, xChaser, xBarrier, xDriver);
                }
                else
                {
                    chimp.coordinates = population[random.Next(population.Count)].coordinates;
                }
            }
            else
            {
                chimp.UpdatePositionChaoticValue();
            }
        }
    }
}
