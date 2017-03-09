using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPAnde.Lib.GA
{
    public static class ChooseParentOperator
   {
        private static IChooseParentOperator _chPOperator;

        static ChooseParentOperator()
        {
            _chPOperator = new RandomChooseParentOperator();
        }

        public static int ChooseParent(List<Chromosome> population, int another)
        {
            return _chPOperator.ChooseParent(population, another);
        }

        public static void ChangeOperator(IChooseParentOperator chPOperator)
        {
            _chPOperator = chPOperator;
        }
    }

    public class RandomChooseParentOperator : IChooseParentOperator
    {
        public int ChooseParent(List<Chromosome> population, int another)
        {
            int index = Randomizer.Random.Next(population.Count);
            while (index == another)
            {
                index = Randomizer.Random.Next(population.Count);
            }

            return index;
        }
}

    public interface IChooseParentOperator
    {
        int ChooseParent(List<Chromosome> population, int another);
    }
}
