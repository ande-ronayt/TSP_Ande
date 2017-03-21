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

        public static int ChooseParent(List<Chromosome> population, int another, double maxFit, int k, int type, double alpha, double beta)
        {
            return _chPOperator.ChooseParent(population, another, maxFit, k, type, alpha, beta);
        }

        public static void ChangeOperator(IChooseParentOperator chPOperator)
        {
            _chPOperator = chPOperator;
        }
    }

    public class RandomChooseParentOperator : IChooseParentOperator
    {
        public int ChooseParent(List<Chromosome> population, int another, double maxFit, int k, int type, double alpha, double beta)
        {
            while (true)
            {
                int start = 0;
                int end = population.Count;
                switch (type)
                {
                    case 1:
                        end = k / 2;
                        break;
                    case 2:
                        start = k / 2;
                        break;
                    case 3:
                        break;
                }
                    
                int index = Randomizer.Random.Next(start, population.Count);

                while (index == another)
                {
                    index = Randomizer.Random.Next(start, population.Count);
                }

                switch (type)
                {
                    case 1: if (Randomizer.Random.NextDouble() < population[index].Fit1 / maxFit)
                        {
                            return index;
                        } 
                        break;
                    case 2: 
                        if (Randomizer.Random.NextDouble() < population[index].Fit2 / maxFit)
                        {
                            return index;
                        }
                        break;
                    case 3:
                        if (Randomizer.Random.NextDouble() < population[index].GetOneFit(alpha, beta) / maxFit)
                        {
                            return index;
                        }
                        break;
                }
            }
        }
}

    public interface IChooseParentOperator
    {
        int ChooseParent(List<Chromosome> population, int another, double maxFit, int k, int type, double alpha, double beta);
    }
}
