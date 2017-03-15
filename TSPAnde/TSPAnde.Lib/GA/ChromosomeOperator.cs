using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPAnde.Lib.GA
{
    public static class ChromosomeOperator
    {
        private static ICrossoverOperator crossover;
        private static IMutationOperator mutationOperator;

        static ChromosomeOperator()
        {
            crossover = new CrossoverOperatorMiddle();
            mutationOperator = new MutationOperatorRandomOne();
        }

        public static List<Gene> Crossover(List<Gene> firstParent, List<Gene> secondParent)
        {
            return crossover.Crossover(firstParent, secondParent);
        }

        public static void ChangeOperator(ICrossoverOperator cOperator)
        {
            crossover = cOperator;
        }

        public static void ChangeOperator(IMutationOperator mOperator)
        {
            mutationOperator = mOperator;
        }

        public static void Mutation(List<Chromosome> chromosomes)
        {
            mutationOperator.Mutation(chromosomes);
        }
    }

    public class CrossoverOperatorMiddle : ICrossoverOperator
    {
        public List<Gene> Crossover(List<Gene> first, List<Gene> second)
        {
            var i = Randomizer.Random.Next(1, second.Count);
            var j = Randomizer.Random.Next(i, second.Count);
            List<Gene> s = first.GetRange(i, j - i + 1);
            List<Gene> ms = second.Except(s).ToList();

            return ms.Take(i)
                .Concat(s)
                .Concat(ms.Skip(i))
                .ToList();           
        }
    }

    public interface ICrossoverOperator
    {
        List<Gene> Crossover(List<Gene> first, List<Gene> second);
    }

    public class MutationOperatorRandomOne : IMutationOperator
    {
        public void Mutation(List<Chromosome> chromosomes)
        {
            foreach (var item in chromosomes)
            {
                if (Randomizer.Random.NextDouble() < Environment.mutRate)
                {
                    //random swap
                    int i = Randomizer.Random.Next(0, item.genes.Count);
                    int j = Randomizer.Random.Next(0, item.genes.Count);
                    var tmpGene = item.genes[i];
                    item.genes[i] = item.genes[j];
                    item.genes[j] = tmpGene;
                }
            }
        }
    }
    public interface IMutationOperator
    {
        void Mutation(List<Chromosome> chromosomes);
    }
}
