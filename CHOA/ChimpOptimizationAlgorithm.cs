using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CHOA
{
    internal class ChimpOptimizationAlgorithm : IOptimizationAlgorithm
    {
        int POPULATION_SIZE = 50;
        int DIMENSION = 5;
        int MAX_ITERATION = 100;

        int currentIteration = 1;

        public string Name { get; set; }
        public double[] XBest { get; set; }
        public double FBest { get; set; }
        public int NumberOfEvaluationFitnessFunction { get; set; }

        private static Random random = new Random();

        List<Chimp> population = new List<Chimp>();

        Chimp xAttacker;
        Chimp xChaser;
        Chimp xBarrier;
        Chimp xDriver;

        IFitnessFunction fitnessFunction;
        double maxM;
        double minM;

        public ChimpOptimizationAlgorithm(int population, int dimension, int iteration, IFitnessFunction function, double minM, double maxM=1)
        {
            POPULATION_SIZE = population;
            DIMENSION = dimension;
            MAX_ITERATION = iteration;
            fitnessFunction = function;
            this.maxM = maxM;
            this.minM = minM;
        }

        public double Solve()
        {

            InitializePopulation();
            CalculateFittnesForEachChimp();
            ChooseBestAgents();
            DivideChimpsIntoGroups();

            while(currentIteration <= MAX_ITERATION)
            {
                foreach (Chimp chimp in population)
                {
                    CalculateParameters(chimp);
                    UpdateChimpPositionByRules(chimp);
                    chimp.fitness = fitnessFunction.CalculateFitnesse(chimp.coordinates);
                }
                ChooseBestAgents();
                UpdateBestChimpsPosition();
                currentIteration++;
            }
            FBest = fitnessFunction.CalculateFitnesse(xAttacker.coordinates);
            XBest = xAttacker.coordinates;
            return FBest;
        }

        private void InitializePopulation()
        {
            for (int i = 0; i < POPULATION_SIZE; i++)
            {
                Chimp chimp = new Chimp(DIMENSION, minM, maxM);
                for (int j = 0; j < DIMENSION; j++)
                {
                    chimp.coordinates[j] = random.NextDouble() * (fitnessFunction.variation + fitnessFunction.variation) - fitnessFunction.variation;
                }
                population.Add(chimp);
            }
        }

        private void CalculateFittnesForEachChimp()
        {
            foreach (Chimp chimp in population)
            {
                chimp.fitness = fitnessFunction.CalculateFitnesse(chimp.coordinates);
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
                    chimp.setGroup(i);
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

        public void CalculateParameters(Chimp chimp)
        {
            chimp.CalculateF(currentIteration, MAX_ITERATION);
            chimp.CalculateM();
            chimp.CalculateA();
            chimp.CalculateC();
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

        public void UpdateChimpPositionByRules(Chimp chimp)
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
