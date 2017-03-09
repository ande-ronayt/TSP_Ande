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
        private List<Gene> genes;

        public double Fit1 { get; set; }

        public double Fit2 { get; set; }
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

        public Chromosome Crossover(Chromosome second)
        {
            return new Chromosome(ChromosomeOperator.Crossover(this.genes, second.genes));
        }
    }
}
