using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CHOA
{
    internal class Raport
    {
        string filePath = "C:\\Users\\Jakub\\Desktop\\Ai.txt";
        string functionName;
        int dimension;
        int population;
        int iteration;
        IFitnessFunction function;

        public Raport(string functionName, int dimension, int population, int iteration, IFitnessFunction function)
        {
            this.functionName = functionName;
            this.dimension = dimension;
            this.population = population;
            this.iteration = iteration;
            this.function = function;
        }
        public void TestAlgorithm()
        {
            List<BestParameters> bestChimps = new List<BestParameters>();
            for (double minC = 0; minC <= 1.9; minC += 0.1)
            {
                for (double maxC = minC+0.1; maxC <= 2; maxC += 0.1)
                {
                    for (double minM = 0; minM <=1.9; minM += 0.1)
                    {
                        for (double maxM = minM+0.1; maxM <= 2; maxM += 0.1)
                        {
                            List<Chimp> bestX = new List<Chimp>();
                            for (int iter = 0; iter < 10; iter++)
                            {
                                ChimpOptimizationAlgorithm CHOA = new ChimpOptimizationAlgorithm(population, dimension, iteration, function, minM, maxM, minC, maxC);
                                CHOA.Solve();
                                bestX.Add(CHOA.xAttacker);
                            }
                            bestX = bestX.OrderBy(chimp => chimp.fitness).ToList();
                            int index = findNotNaNFirstIndex(bestX);
                            if(index != -1)
                            {
                                double stdY = calculateStdY(bestX, index);
                                double coefficientY = calculateCoefficientY(bestX, index, stdY);
                                double[] stdX = calculateStdX(bestX, index);
                                double[] coefficientX = calculateCoefficientX(bestX, index, stdX);
                                BestParameters best = new BestParameters(bestX[index], minM, maxM, minC, maxC, stdX, stdY, coefficientX, coefficientY);
                                bestChimps.Add(best);
                            }
                        }
                    }
                }
            }
            bestChimps = bestChimps.OrderBy(param => param.chimp.fitness).ToList();
            MakeRaport(bestChimps);

        }

        public void MakeRaport(List<BestParameters> bestChimps)
        {
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                for (int i = 0; i < 5; i++)
                {
                    writer.WriteLine($"CHOA;{functionName};{dimension};{bestChimps[i].minM};{bestChimps[i].maxM};{bestChimps[i].minC};{bestChimps[i].maxC};{iteration};{population};({String.Join(" , ", bestChimps[i].chimp.coordinates)});({String.Join(" , ", bestChimps[i].stdX)});({String.Join(" , ", bestChimps[i].coeffX)});{bestChimps[i].chimp.fitness};{bestChimps[i].stdY};{bestChimps[i].coeffY}");
                }
            }
        }

        public int findNotNaNFirstIndex(List<Chimp> bestX)
        {
            for (int i = 0; i < bestX.Count; i++)
            {
                if (!double.IsNaN(bestX[i].fitness))
                {
                    return i;
                }
            }
            return -1;
        }

        public double calculateStdY(List<Chimp> bestX, int startIndex)
        {
            double mean = calculateMeanY(bestX, startIndex);
            double stdev = 0;
            for (int i = startIndex; i < bestX.Count; i++)
            {
                stdev += Math.Pow(bestX[i].fitness - mean, 2);
            }
            stdev /= (bestX.Count - startIndex);
            stdev = Math.Sqrt(stdev);
            return stdev;
        }

        public double[] calculateStdX(List<Chimp> bestX, int startIndex)
        {
            double[] stdX = new double[dimension];
            for(int dim = 0; dim< dimension; dim++)
            {
                double mean = calculateMeanX(bestX, startIndex, dim);
                double stdev = 0;
                for (int i = startIndex; i < bestX.Count; i++)
                {
                    stdev += Math.Pow(bestX[i].coordinates[dim] - mean, 2);
                }
                stdev /= (bestX.Count - startIndex);
                stdev = Math.Sqrt(stdev);
                stdX[dim] = stdev;
            }
            return stdX;
        }

        public double calculateMeanY(List<Chimp> bestX, int startIndex)
        {
            double mean = 0;
            for(int i = startIndex; i < bestX.Count; i++)
            {
                mean += bestX[i].fitness;
            }
            mean /= (bestX.Count - startIndex);
            if (mean == 0)
            {
                mean = 0.0000000000000000001;
            }
            return mean;
        }

        public double calculateMeanX(List<Chimp> bestX, int startIndex, int cord)
        {
            double mean = 0;
            for (int i = startIndex; i < bestX.Count; i++)
            {
                mean += bestX[i].coordinates[cord];
            }
            mean /= (bestX.Count - startIndex);
            if (mean == 0)
            {
                mean = 0.0000000000000000001;
            }
            return mean;
        }

        public double calculateCoefficientY(List<Chimp> bestX, int startIndex, double stdY)
        {
            double mean = calculateMeanY(bestX, startIndex);

            return stdY*100/mean;
        }

        public double[] calculateCoefficientX(List<Chimp> bestX, int startIndex, double[] stdX)
        {
            double[] coeffX = new double[dimension];
            for (int dim = 0; dim < dimension; dim++)
            {
                double mean = calculateMeanX(bestX, startIndex, dim);
                coeffX[dim] = stdX[dim] * 100 / mean;
            }
            return coeffX;
        }
    }
}
