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

        IFitnessFunction fitnessFunction = new Rastrigin();

        public ChimpOptimizationAlgorithm(int population, int dimension, int iteration, IFitnessFunction function)
        {
            POPULATION_SIZE = population;
            DIMENSION = dimension;
            MAX_ITERATION = iteration;
            fitnessFunction = function;
        }

        public double Solve()
        {

            InitializePopulation();
            foreach (Chimp chimp in population)
            {
                double fitness = fitnessFunction.CalculateFitnesse(chimp.coordinates);
                chimp.fitness = fitness;
            }
            List<Chimp> sortedChimps = population.OrderBy(chimp => chimp.fitness).ToList();
            InitializeBestAgents(sortedChimps);


            List<List<Chimp>> groups = DivideChimpsIntoGroups();

            while(currentIteration <= MAX_ITERATION)
            {
                int currentGroup = 1;
                foreach (var group in groups)
                {
                    foreach (Chimp chimp in group)
                    {
                        CalculateParameters(chimp);
                        chimp.setGroup(currentGroup);
                    }
                    currentGroup++;
                }

                foreach (Chimp chimp in population)
                {
                    chimp.UpdatePositionExplore(xAttacker, xChaser, xBarrier, xDriver);
                    CalculateParameters(chimp);
                }



                currentIteration++;
            }
            FBest = fitnessFunction.CalculateFitnesse(xAttacker.coordinates);
            XBest = xAttacker.coordinates;
            return FBest;
        }

        private void InitializePopulation()
        {
            Chimp chimp = new Chimp(DIMENSION);
            for (int i = 0; i < POPULATION_SIZE; i++)
            {
                for (int j = 0; j < DIMENSION; j++)
                {
                    chimp.coordinates[j] = random.NextDouble() * (fitnessFunction.variation + fitnessFunction.variation) - fitnessFunction.variation;
                }
                population.Add(chimp);
            }
        }

        private List<List<Chimp>> DivideChimpsIntoGroups()
        {
            int numberOfGroups = 4;
            var shuffledPopulation = population.OrderBy(x => random.Next()).ToList();
            var groups = new List<List<Chimp>>();
            int groupSize = population.Count / numberOfGroups;

            for (int i = 0; i < numberOfGroups; i++)
            {
                var group = shuffledPopulation.Skip(i * groupSize).Take(groupSize).ToList();
                groups.Add(group);
            }

            return groups;
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
            double[] dAttacker = xAttacker.CalculateD(xAttacker);
            double[] dChaser = xChaser.CalculateD(xChaser);
            double[] dBarrier = xBarrier.CalculateD(xBarrier);
            double[] dDriver = xDriver.CalculateD(xDriver);

            xAttacker.coordinates = xAttacker.CalculateX(xAttacker, dAttacker);
            xChaser.coordinates = xChaser.CalculateX(xChaser, dChaser);
            xBarrier.coordinates = xBarrier.CalculateX(xBarrier, dBarrier);
            xDriver.coordinates = xDriver.CalculateX(xDriver, dDriver);
        }
    }
}
