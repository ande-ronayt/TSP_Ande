using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPAnde.Lib.GA
{
    public class Population
    {
        private List<Chromosome> population;

        public Population()
        {
            this.population = new List<Chromosome>();
            CurrentGeneration = 1;
            MakeRandomPopulation(Environment.popSize);
            CalculateBestFit();
        }

        public int CurrentGeneration { get; set; }

        public double BestFit1 { get; set; }
        public Chromosome BestFit1Chromosome { get; set; }

        public double BestFit2 { get; set; }
        public Chromosome BestFit2Chromosome { get; set; }

        public virtual void MakeRandomPopulation(int count)
        {
            throw new NotImplementedException();
        }

        public void SaveKBestChromosome()
        {
            var k = Environment.k/2;
            List<Chromosome> newPopulation = new List<Chromosome>();
            while (k-- > 0)
            {
                var index = TheBestAtByFit1();
                newPopulation.Add(population[index]);
                this.population.RemoveAt(index);
            }

            k = Environment.k - k;
            while (k-- > 0)
            {
                var index = TheBestAtByFit2();
                newPopulation.Add(population[index]);
                this.population.RemoveAt(index);
            }

            this.population = newPopulation;
        }

        public virtual int TheBestAtByFit1()
        {
            var max = population[0].Fit1;
            var iMax = 0;
            for (int i = 1; i < population.Count; i++)
            {
                if (population[i].Fit1 > max)
                {
                    max = population[i].Fit1;
                    iMax = i;
                }
            }

            return iMax;
        }

        public virtual int TheBestAtByFit2()
        {
            var max = population[0].Fit2;
            var iMax = 0;
            for (int i = 1; i < population.Count; i++)
            {
                if (population[i].Fit2 > max)
                {
                    max = population[i].Fit2;
                    iMax = i;
                }
            }

            return iMax;
        }

        public void GenerateNewChildren()
        {
            var parent1Index = ChooseParent(-1);
            var parent2Index = ChooseParent(parent1Index);
            var k = Environment.popSize - Environment.k;
            var children = new List<Chromosome>();
            while (k-- > 0)
            {
                children.Add(this.population[parent1Index].Crossover(this.population[parent2Index]));
            }

            Mutation(children);
            this.population = this.population.Concat(children).ToList();
        }

        private int ChooseParent(int another)
        {
            return ChooseParentOperator.ChooseParent(this.population, another);
        }

        public void Mutation(List<Chromosome> chromosomes)
        {
            ChromosomeOperator.Mutation(chromosomes);
        }

        public virtual void CalculateBestFit()
        {
            GetBestFit1();
            GetBestFit2();
        }

        public virtual void GetBestFit1()
        {
            var index = TheBestAtByFit1();
            this.BestFit1 = this.population[index].Fit1;
            this.BestFit1Chromosome = this.population[index];
        }

        public virtual void GetBestFit2()
        {
            var index = TheBestAtByFit2();
            this.BestFit2 = this.population[index].Fit2;
            this.BestFit2Chromosome = this.population[index];
        }

        public void NextGeneration()
        {
            SaveKBestChromosome();
            GenerateNewChildren();
            CalculateBestFit();
            this.CurrentGeneration++;
        }
    }
}
