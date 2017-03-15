using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPAnde.Lib.GA
{
    public class Chromosome
    {
        public List<Gene> genes; //public for mutation!

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

                Fit1 = 1 / value;
            }
        }

        public double Fit1 { get; set; }

        public double Fit2 { get { return Fit1; }  } //TODO: change for 2 fit
        public Chromosome(List<Gene> genes)
        {
            this.genes = genes;
        }

        public Chromosome(Chromosome chromosome)
        {
            genes = new List<Gene>();
            foreach (var gene in chromosome.genes)
            {
                this.genes.Add(gene);
            }
        }

        public virtual Chromosome Crossover(Chromosome second)
        {
            return new Chromosome(ChromosomeOperator.Crossover(this.genes, second.genes));
        }

        public double CalculateDistance()
        {
            _distance = 0;
            for (int i = 0; i < genes.Count - 1; i++)
            {
                _distance += DistanceOperator.dOperator.Matrix[genes[i].Id, genes[i + 1].Id];
            }

            Distance = _distance + DistanceOperator.dOperator.Matrix[genes[genes.Count - 1].Id, genes[0].Id];
            return Distance;
        }
    }
}
