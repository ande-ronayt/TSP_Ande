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

        static ChromosomeOperator()
        {
            crossover = new CrossoverOperatorMiddle();
        }

        public static List<Gene> Crossover(List<Gene> firstParent, List<Gene> secondParent)
        {
            return crossover.Crossover(firstParent, secondParent);
        }

        public static void ChangeOperator(ICrossoverOperator cOperator)
        {
            crossover = cOperator;
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
}
