using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace TSPAnde.Lib.GA
{
    public class Chromosome
    {
        public Chromosome Copy()
        {
            Gene[] newGenes = new Gene[this.genes.Count];
            this.genes.CopyTo(newGenes);
            return new Chromosome(newGenes.ToList(), Environment);
        }

        public override string ToString()
        {
            return this.PrintChromosome(this.Environment);
        }

        public Environment Environment;
        public string ToString(Environment environment)
        {
            return this.PrintChromosome(environment);
        }

        public double GetOneFit(double alpha, double beta)
        {
            return alpha * Fit1 + beta * Fit2; 
        }

        public List<Gene> genes; //public for mutation!

        private double _distance;

        public double BalanceProportion;

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

        public List<double> Distances = new List<double>();

        public double Fit1 { get; set; }

        //public double Fit2 { get { return 1/BalanceProportion; }  }
        public double Fit2 { get { return 1 / BalanceProportion; } } 
        public Chromosome(List<Gene> genes, Environment environment)
        {
            this.Environment = environment;
            this.genes = genes;
            CalculateDistance();
        }

        public Chromosome(Chromosome chromosome)
        {
            genes = new List<Gene>();
            foreach (var gene in chromosome.genes)
            {
                this.genes.Add(gene);
            }
            CalculateDistance();
        }

        public virtual List<Chromosome> Crossover(Chromosome second)
        {
            //TODO try new crossover:
            var result = ChromosomeOperator.Crossover(this.genes, second.genes, Environment);
            this.genes = result[0].genes;
            second.genes = result[1%result.Count].genes;

            return ChromosomeOperator.Crossover(this.genes, second.genes, Environment);
        }

        public double CalculateDistance()
        {
            Distances.Clear();
            Distances.Add(0);
            int trevelid = 0;
            //Distances.Last
            _distance = 0;
            double temp = 0;

            for (int i = 0; i < genes.Count; ++i)
            {
                if (genes[i].Id == genes[(i + 1) % genes.Count].Id)
                {
                    temp = Environment.BigDist;
                }else temp = DistanceOperator.dOperator.Matrix[genes[i].Id, genes[(i + 1) % genes.Count].Id];
                _distance += temp;

                if (genes[i].IsDepo)
                {
                    Distances.Add(0);
                    trevelid++;
                }

                Distances[trevelid] += temp;
                //temp = this.t[i].distanceTo(this.t[(i + 1) % this.t.Count]);
            }

            if (Distances.Count > Environment.travelers)
            {
                Distances[0] += Distances[trevelid];
                Distances.RemoveAt(trevelid);
            }

            BalanceProportion = DistanceOperator.GetBalanceProportion(Distances, _distance);


            Distance = _distance;
            return Distance;

            //_distance = 0;
            //for (int i = 0; i < genes.Count - 1; i++)
            //{
            //    _distance += DistanceOperator.dOperator.Matrix[genes[i].Id, genes[i + 1].Id];
            //}

            //Distance = _distance + DistanceOperator.dOperator.Matrix[genes[genes.Count - 1].Id, genes[0].Id];
            //return Distance;
        }
    }
}
