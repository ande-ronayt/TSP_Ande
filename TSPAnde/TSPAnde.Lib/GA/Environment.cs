using System;
using System.Collections.Generic;
using System.Linq;
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
        public int TravelersAmount { get { return _travelersAmount; } set { _travelersAmount = value;  BigDist = int.MaxValue / value; } }

        public int CityAmount { get; set; }

        public int Elitism { get; set; }
       
        public int DepoId { get; set; }

        public int MaximumStuckIteration { get; set; }
        //public int elitism = 4;
        //public int depoId = 1;
        //public  int numCities = 20;
        //public double mutRate = 0.80;
        //public int popSize = 60;
        //public int k = 12;
        //public int travelers = 3;

        public int RandomTwoPointsMutationAmount { get { return (int)(CityAmount * 0.1)+2; } }

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

            MaximumStuckIteration = 500;

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
