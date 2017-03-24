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
        public static Type GetSelectionType { get { return selectionOperator.GetType(); } }
        static SelectionOperator()
        {
            //selectionOperator = new SelectionOperatorTournament();
            selectionOperator = new SelectionOperatorTournamentWithRandomAlphaForOneFit();
            Coefficient = 5;
        }

        public static int Coefficient { get; set; }

        public static List<Chromosome> Selection(Population population)
        {
            return selectionOperator.Selection(population);
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

            var k = population.Environment.k;
            while (k-- > 0)
            {
                //randomize alpha 
                if (population.Environment.Beta != 0 && population.Environment.Alpha == 0)
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
   
    public class SelectionOperatorTournament : ISelectionOperator
    {
        public List<Chromosome> Selection(Population population)
        {
            var newPopulation = new List<Chromosome>();
            int k;
            if (population.Environment.IsuseOneFit)
            {
                k = population.Environment.k;
                while (k-- > 0)
                {
                    var index = population.TheBestAtByOneFit();
                    newPopulation.Add(population.population[index]);
                    population.population.RemoveAt(index);
                }
            }
            else
            {
                k = population.Environment.k / 2;
                while (k-- > 0)
                {
                    var index = population.TheBestAtByFit1();
                    newPopulation.Add(population.population[index]);
                    population.population.RemoveAt(index);
                }

                k = population.Environment.k - population.Environment.k / 2;
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
