using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPAnde.Lib.GA
{
    #region crossover

    public enum CrossoverType
    {
        CrossoverOperatorOX = 1,
        CrossoverOperatorAEXWithShortestDistance = 2,
        CrossoverOperatorAEX = 3,
        CrosooverOperatorPMX = 4
    }

    public class CrossoverOperatorOX : ICrossoverOperator
    {
        public List<Chromosome> Crossover(List<Gene> first, List<Gene> second, Environment environment)
        {
            var i = Randomizer.Random.Next(1, second.Count);
            var j = Randomizer.Random.Next(i, second.Count);
            List<Gene> s = first.GetRange(i, j - i + 1);
            List<Gene> ms = second.Except(s).ToList();

            List<Gene> s2 = second.GetRange(i, j - i + 1);
            List<Gene> ms2 = first.Except(s2).ToList();

            List<Chromosome> newChildren = new List<Chromosome>();
            newChildren.Add(new Chromosome(
                                     ms.Take(i)
                                        .Concat(s)
                                        .Concat(ms.Skip(i))
                                        .ToList(), environment));
            newChildren.Add(new Chromosome(
                                     ms2.Take(i)
                                        .Concat(s2)
                                        .Concat(ms2.Skip(i))
                                        .ToList(), environment));
            return newChildren;
        }
    }

    public class CrossoverOperatorAEXWithShortestDistance : ICrossoverOperator //Alternating edges crossover
    {
        public List<Chromosome> Crossover(List<Gene> first, List<Gene> second, Environment environment)
        {
            List<Gene> child1, child2, parent1, parent2;
            Gene dx, dy;
            int indx1, indx2;
            //child 1 right derection
            parent1 = first.ToList();
            parent2 = second.ToList();

            var i = Randomizer.Random.Next(parent2.Count);
            var gene = parent1[i];
            child1 = new List<Gene>() { gene };
            while (parent1.Count > 1)
            {
                indx1 = (parent1.IndexOf(gene) + 1) % parent1.Count;
                indx2 = (parent2.IndexOf(gene) + 1) % parent2.Count;
                dx = parent1[indx1];
                dy = parent2[indx2];
                parent1.Remove(gene);
                parent2.Remove(gene);

                gene = DistanceOperator.dOperator.Matrix[gene.Id, dx.Id] < DistanceOperator.dOperator.Matrix[gene.Id, dx.Id] ?
                    dx : dy;
                child1.Add(gene);
            }

            //child 2 -- left derection
            parent1 = first.ToList();
            parent2 = second.ToList();
            i = Randomizer.Random.Next(parent2.Count);
            gene = parent1[i];
            child2 = new List<Gene>() { gene };
            while (parent1.Count > 1)
            {
                indx1 = parent1.IndexOf(gene) - 1;
                indx1 = indx1 == -1 ? parent1.Count - 1 : indx1;
                indx2 = parent2.IndexOf(gene) - 1;
                indx2 = indx2 == -1 ? parent2.Count - 1 : indx2;
                dx = parent1[indx1];
                dy = parent2[indx2];
                parent1.Remove(gene);
                parent2.Remove(gene);
                gene = DistanceOperator.dOperator.Matrix[gene.Id, dx.Id] < DistanceOperator.dOperator.Matrix[gene.Id, dx.Id] ?
                    dx : dy;
                child2.Add(gene);
            }

            return new List<Chromosome>(){
                new Chromosome(child1, environment),
                new Chromosome(child2, environment)
            };
        }
    }

    public class CrossoverOperatorAEX : ICrossoverOperator //Alternating edges crossover
    {
        public List<Chromosome> Crossover(List<Gene> first, List<Gene> second, Environment environment)
        {
            List<Gene> child1, child2, parent1, parent2;
            Gene dx, dy;
            int indx1, indx2;
            bool fs = false;
            //child 1 right derection
            parent1 = first.ToList();
            parent2 = second.ToList();

            var i = Randomizer.Random.Next(parent2.Count);
            var gene = parent1[i];
            child1 = new List<Gene>() { gene };
            while (parent1.Count > 1)
            {
                indx1 = (parent1.IndexOf(gene) + 1) % parent1.Count;
                indx2 = (parent2.IndexOf(gene) + 1) % parent2.Count;
                dx = parent1[indx1];
                dy = parent2[indx2];
                parent1.Remove(gene);
                parent2.Remove(gene);

                gene = fs ? dx : dy;
                fs = !fs;
                //gene = DistanceOperator.dOperator.Matrix[gene.Id, dx.Id] < DistanceOperator.dOperator.Matrix[gene.Id, dx.Id] ?
                //    dx : dy;
                child1.Add(gene);
            }

            //child 2 -- left derection
            parent1 = first.ToList();
            parent2 = second.ToList();
            i = Randomizer.Random.Next(parent2.Count);
            gene = parent1[i];
            child2 = new List<Gene>() { gene };
            fs = false;
            while (parent1.Count > 1)
            {
                indx1 = parent1.IndexOf(gene) - 1;
                indx1 = indx1 == -1 ? parent1.Count - 1 : indx1;
                indx2 = parent2.IndexOf(gene) - 1;
                indx2 = indx2 == -1 ? parent2.Count - 1 : indx2;
                dx = parent1[indx1];
                dy = parent2[indx2];
                parent1.Remove(gene);
                parent2.Remove(gene);
                gene = fs ? dx : dy;
                fs = !fs;
                //gene = DistanceOperator.dOperator.Matrix[gene.Id, dx.Id] < DistanceOperator.dOperator.Matrix[gene.Id, dx.Id] ?
                //    dx : dy;
                child2.Add(gene);
            }

            return new List<Chromosome>(){
                new Chromosome(child1, environment),
                new Chromosome(child2, environment)
            };
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
                .Concat(first.Skip(j + 1)).ToList();
            List<Gene> sFull = second.Take(i)
                .Concat(mFirst)
                .Concat(second.Skip(j + 1)).ToList();

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
            for (int k = j + 1; k < fFull.Count; k++)
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
}
