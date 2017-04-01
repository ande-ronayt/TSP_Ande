using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPAnde.Lib.GA
{
    public static class SelectionOperator
    {
        private static ISelectionOperator selectionOperator;

        public static Type GetSelectionType
        {
            get { return selectionOperator.GetType(); }
        }

        static SelectionOperator()
        {
            //selectionOperator = new SelectionOperatorTheBestK();
            //selectionOperator = new SelectionOperatorTournamentWithRandomAlphaForOneFit();
            //selectionOperator = new SelectionOperatorWheelOneFit();
            selectionOperator = new SelectionOperatorBestPlusRouletteWheel();
            //selectionOperator = new SelectionOperatorTournament();
            //selectionOperator = new SelectionOperatorMySelection();
            Coefficient = 5;
        }

        public static int Coefficient { get; set; }

        public static List<Chromosome> Selection(Population population)
        {
            return selectionOperator.Selection(population);
        }
    }

    public class SelectionOperatorMySelection : ISelectionOperator
    {
        public List<Chromosome> Selection(Population population)
        {
            var alpha = population.Environment.Alpha;
            var beta = population.Environment.Alpha;
            var newPopulation = new List<Chromosome>();
            var toMutatePopulation = new List<Chromosome>();

            // add the best solution
            //newPopulation.Add(population.TheBest.Copy());

            //add the best current solution
            int index = population.TheBestAtByOneFit();
            var bestCurrentChromosome = population.population[index]; 
            newPopulation.Add(bestCurrentChromosome);
            population.population.RemoveAt(index);

            //RSM and PSM
            toMutatePopulation.Add(population.TheBest.Copy());
            toMutatePopulation.Add(population.TheBest.Copy());
            var mutR = population.Environment.MutationProbability;
            population.Environment.MutationProbability = 1;
            new MutationOperatorHalfRSMHalfPSM().Mutation(toMutatePopulation, population.Environment);
            foreach (var item in toMutatePopulation)
            {
                item.CalculateDistance();
                newPopulation.Add(item);
            }

            population.Environment.MutationProbability = mutR;
            while (newPopulation.Count < population.Environment.PopulationSize)
            {
                while (true)
                {
                    index = Randomizer.Random.Next(population.population.Count);
                    if (Randomizer.Random.NextDouble() <
                        population.population[index].GetOneFit(alpha, beta) / (bestCurrentChromosome.GetOneFit(alpha, beta)))
                    {
                        newPopulation.Add(population.population[index].Copy());
                        break;
                    }
                }
            }

            return newPopulation;
        }
    }


    public class SelectionOperatorTournament : ISelectionOperator
    {
        public List<Chromosome> Selection(Population population)
        {
            var alpha = population.Environment.Alpha;
            var beta = population.Environment.Alpha;
            int index = population.TheBestAtByOneFit();
            var bestChromosome = population.population[index];
            var newPopulation = new List<Chromosome>();
            var k = population.Environment.SelectionCoefficient;
            while ((k--) > 0)
            {
                while (true)
                {
                    index = Randomizer.Random.Next(population.population.Count);
                    if (Randomizer.Random.NextDouble() <
                        population.population[index].GetOneFit(alpha, beta) / (5 * bestChromosome.GetOneFit(alpha, beta)))
                    {
                        newPopulation.Add(population.population[index]);
                        population.population.RemoveAt(index);
                        break;
                    }
                }
            }

            population.population = newPopulation;
            newPopulation = new List<Chromosome>();
            var e = population.Environment.Elitism;
            while (e-- > 0)
            {
                index = population.TheBestAtByOneFit();
                newPopulation.Add(population.population[index]);
                population.population.RemoveAt(index);
            }

            return newPopulation;
        }
    }

    public class SelectionOperatorBestPlusRouletteWheel : ISelectionOperator
    {
        public List<Chromosome> Selection(Population population)
        {
            var alpha = population.Environment.Alpha;
            var beta = population.Environment.Alpha;
            var k = population.Environment.SelectionCoefficient;
            int index = population.TheBestAtByOneFit();
            var bestChromosome = population.population[index];

            var newPopulation = new List<Chromosome>();
            var e = population.Environment.Elitism;
            while (e-- > 0)
            {
                index = population.TheBestAtByOneFit();
                newPopulation.Add(population.population[index]);
                population.population.RemoveAt(index);
            }

            while (population.population.Count > 0 && (k-- ) > population.Environment.Elitism)
            {
                while (true)
                {
                    index = Randomizer.Random.Next(population.population.Count);
                    if (Randomizer.Random.NextDouble() <
                        population.population[index].GetOneFit(alpha, beta) / (bestChromosome.GetOneFit(alpha, beta)))
                    {
                        newPopulation.Add(population.population[index]);
                        population.population.RemoveAt(index);
                        break;
                    }
                }
            }

            return newPopulation;
        }
    }

    public class SelectionOperatorWheelOneFit : ISelectionOperator
    {
        public List<Chromosome> Selection(Population population)
        {
            var alpha = population.Environment.Alpha;
            var beta = population.Environment.Alpha;
            var k = population.Environment.SelectionCoefficient;
            int index = population.TheBestAtByOneFit();
            var bestChromosome = population.population[index];

            var newPopulation = new List<Chromosome>();
            newPopulation.Add(population.population[index]);
            population.population.RemoveAt(index);
            while (k-- > 1)
            {
                while (true)
                {
                    index = Randomizer.Random.Next(population.population.Count);
                    if (Randomizer.Random.NextDouble() <
                        population.population[index].GetOneFit(alpha, beta)/(3*bestChromosome.GetOneFit(alpha, beta)))
                    {
                        newPopulation.Add(population.population[index]);
                        population.population.RemoveAt(index);
                        break;
                    }
                }                
            }

            return newPopulation;
        }
    }

    public class SelectionOperatorTournamentWithRandomAlphaForOneFit : ISelectionOperator
    {
        public List<Chromosome> Selection(Population population)
        {
            var originalAlpha = population.Environment.Alpha;
            var newPopulation = new List<Chromosome>();

            var index = population.TheBestAtByOneFit();
            var chromosome = population.population[index];
            var ratio = chromosome.Fit2 / chromosome.Fit1;

            var k = population.Environment.SelectionCoefficient;
            while (k-- > 0)
            {
                //randomize alpha 
                if (population.Environment.TravelersAmount > 1 && population.Environment.Beta != 0 && population.Environment.Alpha == 0)
                    population.Environment.Alpha *= Randomizer.Random.Next(SelectionOperator.Coefficient)*ratio;
                
                index = population.TheBestAtByOneFit();
                newPopulation.Add(population.population[index]);
                population.population.RemoveAt(index);
                population.Environment.Alpha = originalAlpha;
            }

            population.Environment.Alpha = originalAlpha;
            return newPopulation;
        }
    }
   
    public class SelectionOperatorTheBestK : ISelectionOperator
    {
        public List<Chromosome> Selection(Population population)
        {
            var newPopulation = new List<Chromosome>();
            int k;
            if (population.Environment.IsuseOneFit)
            {
                k = population.Environment.SelectionCoefficient;
                while (k-- > 0)
                {
                    var index = population.TheBestAtByOneFit();
                    newPopulation.Add(population.population[index]);
                    population.population.RemoveAt(index);
                }
            }
            else
            {
                k = population.Environment.SelectionCoefficient / 2;
                while (k-- > 0)
                {
                    var index = population.TheBestAtByFit1();
                    newPopulation.Add(population.population[index]);
                    population.population.RemoveAt(index);
                }

                k = population.Environment.SelectionCoefficient - population.Environment.SelectionCoefficient / 2;
                while (k-- > 0)
                {
                    var index = population.TheBestAtByFit2();
                    newPopulation.Add(population.population[index]);
                    population.population.RemoveAt(index);
                }
            }

            return newPopulation;
        }
    }

    public interface ISelectionOperator
    {
        List<Chromosome> Selection(Population population);
    }
}
