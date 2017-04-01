﻿using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPAnde.Lib.GA
{
    public static class ChromosomeOperator
    {
        private static ICrossoverOperator crossover;
        private static IMutationOperator mutationOperator;

        public static Type GetCrossoverType { get { return crossover.GetType(); } }
        public static Type GetMutationOperator { get { return mutationOperator.GetType(); } }

        static ChromosomeOperator()
        {
            //crossover = new CrossoverOperatorTwoPoints();
            crossover = new CrosooverOperatorPMX();
            //mutationOperator = new MutationOperatorRandomTwoPoints();
            //mutationOperator = new MutationOperatorRSM();
            //mutationOperator = new MutationOperatorRandomAndThenRSM();
            //mutationOperator = new MutationOperatorPSM();
            mutationOperator = new MutationOperatorHalfRSMHalfPSM();
            mutationOperator = new MutationOperatorInsertions();
            mutationOperator = new MutationOperatorHalfRSMHalfInsertins();
        }

        public static List<Chromosome> Crossover(List<Gene> firstParent, List<Gene> secondParent, Environment environment)
        {
            return crossover.Crossover(firstParent, secondParent, environment);
        }

        public static void ChangeOperator(ICrossoverOperator cOperator)
        {
            crossover = cOperator;
        }

        public static void ChangeOperator(IMutationOperator mOperator)
        {
            mutationOperator = mOperator;
        }

        public static void Mutation(List<Chromosome> chromosomes, Environment environment)
        {
            mutationOperator.Mutation(chromosomes, environment);
        }
    }

    #region crossover
    public class CrossoverOperatorTwoPoints : ICrossoverOperator
    {
        public List<Chromosome> Crossover(List<Gene> first, List<Gene> second, Environment environment)
        {
            var i = Randomizer.Random.Next(1, second.Count);
            var j = Randomizer.Random.Next(i, second.Count);
            List<Gene> s = first.GetRange(i, j - i + 1);
            List<Gene> ms = second.Except(s).ToList();

            List<Chromosome> newChildren = new List<Chromosome>();
            newChildren.Add(new Chromosome(
                                     ms.Take(i)
                                        .Concat(s)
                                        .Concat(ms.Skip(i))
                                        .ToList(), environment));
            return newChildren;
        }
    }

    public class CrosooverOperatorPMX : ICrossoverOperator
    {
        public List<Chromosome> Crossover(List<Gene> first, List<Gene> second, Environment environment)
        {
            var i = Randomizer.Random.Next(1, second.Count);
            var j = Randomizer.Random.Next(i, second.Count);
            
            List<Gene> mFirst = first.GetRange(i, j - i + 1);
            List<Gene> mSecond = second.GetRange(i, j - i + 1);

            //TODO: check skip(j) or j+1
            List<Gene> fFull = first.Take(i)
                .Concat(mSecond)
                .Concat(first.Skip(j+1)).ToList();
            List<Gene> sFull = second.Take(i)
                .Concat(mFirst)
                .Concat(second.Skip(j+1)).ToList();

            var mapper1 = new Dictionary<Gene, Gene>();
            var mapper2 = new Dictionary<Gene, Gene>();
            for (int k = 0; k < mFirst.Count; k++)
            {
                mapper1.Add(mFirst[k], mSecond[k]);
                mapper2.Add(mSecond[k], mFirst[k]);
            }

            for (int k = 0; k < i; k++)
            {
                while (mSecond.Contains(fFull[k]))
                {
                    fFull[k] = mapper2[fFull[k]];
                }
                while (mFirst.Contains(sFull[k]))
                {
                    sFull[k] = mapper1[sFull[k]];
                }
            }
            for (int k = j+1; k < fFull.Count; k++)
            {
                while (mSecond.Contains(fFull[k]))
                {
                    fFull[k] = mapper2[fFull[k]];
                }
                while (mFirst.Contains(sFull[k]))
                {
                    sFull[k] = mapper1[sFull[k]];
                }
            }

            var newChilder = new List<Chromosome>();
            newChilder.Add(new Chromosome(fFull, environment));
            newChilder.Add(new Chromosome(sFull, environment));
            return newChilder;
        }
    }

    public interface ICrossoverOperator
    {
        List<Chromosome> Crossover(List<Gene> first, List<Gene> second, Environment environment);
    }
    #endregion
    #region mutation

    public class MutationOperatorHalfRSMHalfInsertins : IMutationOperator
    {
        public void Mutation(List<Chromosome> chromosomes, Environment Environment)
        {
            var rsmMutationList = new List<Chromosome>();
            var insertionsMutationList = new List<Chromosome>();
            foreach (var item in chromosomes)
            {
                if (Randomizer.Random.NextDouble() < 0.5)
                    rsmMutationList.Add(item);
                else insertionsMutationList.Add(item);
            }

            var rsmMutation = new MutationOperatorRSM();
            var insertionMutation = new MutationOperatorInsertions();
            rsmMutation.Mutation(rsmMutationList, Environment);
            insertionMutation.Mutation(insertionsMutationList, Environment);
        }
    }

    public class MutationOperatorInsertions : IMutationOperator
    {
        public void Mutation(List<Chromosome> chromosomes, Environment Environment)
        {
            foreach (var item in chromosomes)
            {
                if (!(Randomizer.Random.NextDouble() < Environment.MutationProbability)) continue;

                int i, j, z;
                i = Randomizer.Random.Next(0, item.genes.Count);
                j = Randomizer.Random.Next(0, item.genes.Count);
                if (i > j)
                {
                    var tmp = i;
                    i = j;
                    j = tmp;
                }

                if (j - i + 1 == item.genes.Count) continue;               

                do
                {
                    z = Randomizer.Random.Next(0, item.genes.Count);
                } while (z >= i && z <= j);

                List<Gene> start, start2, end, end2;
                List<Gene> middle;
                if (z < i)
                {
                    start = item.genes.Take(z+1).ToList();
                    start2 = item.genes.Skip(z+1).Take(i-z-1).ToList();
                    middle = item.genes.Skip(i).Take(j - i + 1).ToList();
                    end = item.genes.Skip(j + 1).ToList();
                    item.genes = start.Concat(middle).Concat(start2).Concat(end).ToList();
                }
                else
                {
                    start = item.genes.Take(i).ToList();
                    middle = item.genes.Skip(i).Take(j - i + 1).ToList();
                    end = item.genes.Skip(j + 1).Take(z - j).ToList();
                    end2 = item.genes.Skip(z + 1).ToList();
                    item.genes = start.Concat(middle).Concat(end).Concat(end2).ToList();
                }
            }
        }
    }

    public class MutationOperatorHalfRSMHalfPSM : IMutationOperator
    {
        public void Mutation(List<Chromosome> chromosomes, Environment Environment)
        {
            var rsmMutationList = new List<Chromosome>();
            var psmMutationList = new List<Chromosome>();
            foreach (var item in chromosomes)
            {
                if (Randomizer.Random.NextDouble() < 0.5)
                    rsmMutationList.Add(item);
                else psmMutationList.Add(item);
            }

            var rsmMutation = new MutationOperatorRSM();
            var psmMutation = new MutationOperatorPSM();
            rsmMutation.Mutation(rsmMutationList, Environment);
            psmMutation.Mutation(psmMutationList, Environment);

            //var m = chromosomes.Count / 5;
            //var rsmMutation = new MutationOperatorRSM();
            //var psmMutation = new MutationOperatorPSM();

            //rsmMutation.Mutation(chromosomes.Take(m).ToList(), Environment);
            //psmMutation.Mutation(chromosomes.Skip(m).ToList(), Environment);
        }
    }

    public class MutationOperatorPSM : IMutationOperator // partial shuffle mutation
    {
        public void Mutation(List<Chromosome> chromosomes, Environment Environment)
        {
            IEnumerable<Gene> start, end;
            List<Gene> middle;
            foreach (var item in chromosomes)
            {
                if (Randomizer.Random.NextDouble() < Environment.MutationProbability)
                {
                    int i, j;
                    do
                    {
                        i = Randomizer.Random.Next(0, item.genes.Count);
                        j = Randomizer.Random.Next(0, item.genes.Count);
                    } while (i == j);
                    if (i > j)
                    {
                        var tmp = i;
                        i = j;
                        j = tmp;
                    }
                    
                    start = item.genes.Take(i);
                    middle = item.genes.Skip(i).Take(j - i+1).ToList();
                    end = item.genes.Skip(j+1);

                    middle.Shuffle();

                    item.genes = start.Concat(middle).Concat(end).ToList();
                    if (item.genes.Count == 47)
                    {
                        ;
                    }
                }
            }
        }
    }

    public class MutationOperatorRandomAndThenRSM : IMutationOperator
    {
        public void Mutation(List<Chromosome> chromosomes, Environment Environment)
        {
            var m = chromosomes.Count/2;
            var randomTwoPointsMutation = new MutationOperatorRandomTwoPoints();
            var rsmMutation = new MutationOperatorRSM();

            randomTwoPointsMutation.Mutation(chromosomes.Take(m).ToList(), Environment);
            rsmMutation.Mutation(chromosomes.Skip(m).ToList(), Environment);
        }
    }

    public class MutationOperatorRSM : IMutationOperator
    {
        public void Mutation(List<Chromosome> chromosomes, Environment Environment)
        {
            IEnumerable<Gene> start, middle, end;
            foreach (var item in chromosomes)
            {
                if (Randomizer.Random.NextDouble() < Environment.MutationProbability)
                {
                    int i, j;
                    do
                    {
                        i = Randomizer.Random.Next(0, item.genes.Count);
                        j = Randomizer.Random.Next(0, item.genes.Count);
                    } while (i == j);
                    if (i > j)
                    {
                        var tmp = i;
                        i = j;
                        j = tmp;
                    }

                    start = item.genes.Take(i);
                    middle = item.genes.Skip(i).Take(j - i +1);
                    end = item.genes.Skip(j+1);

                    item.genes = start.Concat(middle.Reverse()).Concat(end).ToList();
                }

            }
        }
    }

    public class MutationOperatorRandomTwoPoints : IMutationOperator
    {
        public void Mutation(List<Chromosome> chromosomes, Environment Environment)
        {
            foreach (var item in chromosomes)
            {
                int c = 0;
                do{
                    if (Randomizer.Random.NextDouble() < Environment.MutationProbability)
                    {
                        //random swap
                        int i = Randomizer.Random.Next(0, item.genes.Count);
                        int j = Randomizer.Random.Next(0, item.genes.Count);
                        var tmpGene = item.genes[i];
                        item.genes[i] = item.genes[j];
                        item.genes[j] = tmpGene;
                    }
                    
                }while(++c < Environment.RandomTwoPointsMutationAmount);
                
                
            }
                
        }
    }
    public interface IMutationOperator
    {
        void Mutation(List<Chromosome> chromosomes, Environment Environment);
    }
#endregion
}

