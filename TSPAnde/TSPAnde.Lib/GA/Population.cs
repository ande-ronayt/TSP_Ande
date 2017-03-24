using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPAnde.Lib.GA
{
    public class Population
    {
        public Environment Environment{ get; set; }

        public List<Chromosome> population;

        public Population(Environment environment)
        {
            this.Environment = environment;
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

        public double BestOneFit { get; set; }

        public Chromosome BestOneFitChromosome { get; set; }

        public virtual void MakeRandomPopulation(int count)
        {
            var tempGeneList = new List<Gene>();
            for (int i = 1; i <= Environment.numCities; i++)
            {
                tempGeneList.Add(new Gene() { Id = i });
            }

            tempGeneList.First(x => x.Id == Environment.depoId).IsDepo = true;
            if (Environment.travelers > 1)
            {
                for (int i = 1; i < Environment.travelers; i++)
                {
                    tempGeneList.Add(new Gene { Id = Environment.depoId, IsDepo = true });
                }
            }

            for (int i = 0; i < count; i++)
            {
                var newGeneList = new List<Gene>();
                for (int j = 0; j < tempGeneList.Count - 1; j++)
                {
                    newGeneList.Add(tempGeneList.Except(newGeneList).ElementAt(Randomizer.Random.Next(tempGeneList.Except(newGeneList).Count())));
                }

                newGeneList.Add(tempGeneList.Except(newGeneList).First());
                population.Add(new Chromosome(newGeneList, Environment));
            }
        }

        public void Selection()
        {
            this.population = SelectionOperator.Selection(this); new List<Chromosome>();
        }
      
        public void GenerateNewChildren(int k)
        {
            double bestFit1, bestFit2;
            int type1 = 1, type2 = 2;
            if (Environment.IsuseOneFit)
            {
                bestFit1 = bestFit2 = BestOneFit;
                type1 = type2 = 3;
            }
            else
            {
                bestFit1 = BestFit1;
                bestFit2 = BestFit2;
            }
            var parent1Index = ChooseParent(-1, bestFit1, Environment.k, type1, Environment.Alpha, Environment.Beta);
            var parent2Index = ChooseParent(parent1Index, bestFit2, Environment.k, type2, Environment.Alpha, Environment.Beta);
            var children = new List<Chromosome>();
            while (children.Count <= k)
            {
                children.AddRange(this.population[parent1Index].Crossover(this.population[parent2Index]));
            }

            Mutation(children);
            this.population = this.population.Concat(children).ToList();
        }

        private int ChooseParent(int another, double maxFit, int k, int type, double alpha = 1, double beta = 1)
        {
            return ChooseParentOperator.ChooseParent(this.population, another, maxFit, k, type, alpha, beta);
        }

        public void Mutation(List<Chromosome> chromosomes)
        {
            ChromosomeOperator.Mutation(chromosomes, this.Environment);
            foreach (var item in chromosomes)
            {
                item.CalculateDistance();
            }
        }

        #region BestFitFunctions
        public int TheBestAtByFit1()
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

        public int TheBestAtByFit2()
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

        public int TheBestAtByOneFit()
        {
            var max = population[0].GetOneFit(Environment.Alpha, this.Environment.Beta);
            var iMax = 0;
            for (int i = 1; i < population.Count; i++)
            {
                if (population[i].GetOneFit(Environment.Alpha, this.Environment.Beta) > max)
                {
                    max = population[i].GetOneFit(Environment.Alpha, this.Environment.Beta);
                    iMax = i;
                }
            }

            return iMax;
        }
        public void CalculateBestFit()
        {
            if (Environment.IsuseOneFit)
            {
                GetBestFit();
            }
            else
            {
                GetBestFit1();
                GetBestFit2();
            }
        }

        public void GetBestFit()
        {
            var index = TheBestAtByOneFit();
            this.BestOneFit = this.population[index].GetOneFit(Environment.Alpha, this.Environment.Beta);
            this.BestOneFitChromosome = this.population[index];
        }

        public void GetBestFit1()
        {
            var index = TheBestAtByFit1();
            this.BestFit1 = this.population[index].Fit1;
            this.BestFit1Chromosome = this.population[index];
        }

        public void GetBestFit2()
        {
            var index = TheBestAtByFit2();
            this.BestFit2 = this.population[index].Fit2;
            this.BestFit2Chromosome = this.population[index];
        }

        #endregion

        public void NextGeneration()
        {
            Selection();
            GenerateNewChildren(Environment.popSize - Environment.k);
            CalculateBestFit();
            this.CurrentGeneration++;
        }

        public void Init(int travelers, int depo)
        {
            MakeRandomPopulation(1);            
        }
    }
}
