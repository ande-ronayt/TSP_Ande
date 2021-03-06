﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace TSPAnde.Lib.GA
{

    public class Environment
    {
        private  object lockObj = new object();
        private  double _alpha = 1;
        public  double Alpha
        {
            get
            {
                lock (lockObj)
                {
                    return _alpha;
                }
            }
            set
            {
                lock (lockObj)
                {
                    _alpha = value;
                }
            }
        }

        private  double _beta = 1;
        public  double Beta
        {
            get { lock (lockObj) { return _beta; } }
            set { lock (lockObj) { _beta = value; } }
        }

        public double CrossoverProbability { get; set; }

        public double MutationProbability { get; set; }

        public int PopulationSize { get; set; }

        public int SelectionCoefficient { get; set; }

        private int _travelersAmount;

        public int TravelersAmount
        {
            get { return _travelersAmount; }   
            set { _travelersAmount = value;  BigDist = int.MaxValue / value; }
        }

        public int CityAmount { get; set; }

        public int Elitism { get; set; }
       
        public int DepoId { get; set; }

        private int _maxStuckIter;

        public int MaximumStuckIteration
        {
            get
            {
                if (_maxStuckIter == 0)
                {
                    return CityAmount*500;
                }

                return _maxStuckIter;
            }
            set { _maxStuckIter = value; }
        }

        public int RandomTwoPointsMutationAmount
        {
            get { return (int)(CityAmount * 0.1)+2; }
        }

        public double IncertionReverseProbability { get; set; }

        public bool IsuseOneFit = true;

        public static double BalanceCoefficient = 0.7;

        public Environment()
        {
            CrossoverProbability = 0.8;
            MutationProbability = 0.5;
            PopulationSize = 60;
            SelectionCoefficient = 8;

            IncertionReverseProbability = 0.5;
            TravelersAmount = 3;
            CityAmount = 20;
            Elitism = 3;
        }

        public static int BigDist { get; set; }

        public Environment(double mutP, double crosP, int popSize )
        {
            MutationProbability = mutP;
            CrossoverProbability = crosP;
            PopulationSize = popSize;
        }
    }

}
