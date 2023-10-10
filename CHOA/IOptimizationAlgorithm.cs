﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IOptimizationAlgorithm
{
    string Name { get; set; }
    double[] XBest { get; set; }
    double FBest { get; set; }
    int NumberOfEvaluationFitnessFunction { get; set; }

    double Solve();
}