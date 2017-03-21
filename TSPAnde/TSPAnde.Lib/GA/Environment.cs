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

        //public const int elitism = 6;
        public int depoId = 1;
        public int bigDist = 9999;
        public  int numCities = 20;
        public double mutRate = 0.7;
        public int popSize = 60;
        public int k = 6;
        public int travelers = 3;
        public int countMut { get { return (int)(numCities * 0.1)+1; } }

        public bool IsuseOneFit = true;
    }
}
