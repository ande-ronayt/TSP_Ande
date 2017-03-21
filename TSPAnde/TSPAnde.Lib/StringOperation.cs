using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSPAnde.Lib.GA;

namespace TSPAnde.Lib
{
    public static class StringOperation
    {
        public static string PrintNumber(this double number, int digits = 6){
            if (number > 1) return Math.Round(number, 1).ToString();
            return Math.Round(number, digits).ToString();
        }

        public static string PrintChromosome(this Chromosome chromosome, GA.Environment environment)
        {
            string distStr = "";
            for (int i = 0; i < chromosome.Distances.Count-1; i++)
			{
                distStr += chromosome.Distances[i].PrintNumber() + ":";
			}
            distStr += chromosome.Distances.Last().PrintNumber();

            string str = string.Format(
                                        @"F: {3} | D: {0} | BD: {1} | B {2}:- " ,
                                        chromosome.Distance.PrintNumber(), 
                                        distStr,
                                        chromosome.BalanceProportion.PrintNumber(),
                                        chromosome.GetOneFit(environment.Alpha, environment.Beta).PrintNumber()
                                        );
            for (int i = 0; i < chromosome.genes.Count - 1; i++)
            {
                str += chromosome.genes[i].ToString() + "-";
            }

            str += chromosome.genes.Last().ToString();
            return str;
        }
    }
}
