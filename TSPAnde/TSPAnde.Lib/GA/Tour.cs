using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSPAnde.Lib.GA;

namespace TSPAnde.Lib.GA
{
    public class Tour : Chromosome
    {
        public Tour(Chromosome chromosome) : base(new List<Gene>())
        {
            this.genes = chromosome.genes;
            CalculateDistance();
        }

        private double _distance;

        public double Distance
        {
            get
            {
                return _distance;
            }

            set
            {
                _distance = value;
                if (value == 0)
                {
                    Fit1 = 0;
                }

                Fit1 = 1/value;
            }
        }


        public double CalculateDistance()
        {
            _distance = 0;
            for (int i = 0; i < genes.Count-1; i++)
            {
                _distance += DistanceOperator.dOperator.Matrix[genes[i].Id, genes[i + 1].Id];
            }

            Distance = _distance + DistanceOperator.dOperator.Matrix[genes[genes.Count - 1].Id, genes[0].Id];
            return Distance;
        }
    }
}
