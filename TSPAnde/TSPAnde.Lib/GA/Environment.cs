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

        public int elitism = 0;
        public int depoId = 1;
        public static int BigDist = 9999;
        public  int numCities = 20;
        public double mutRate = 0.05;
        public int popSize = 60;
        public int k = 12;
        public int travelers = 3;
        public int countMut { get { return (int)(numCities * 0.1)+2; } }

        public bool IsuseOneFit = true;

        public static double BalanceCoefficient = 0.7;
    }
}
