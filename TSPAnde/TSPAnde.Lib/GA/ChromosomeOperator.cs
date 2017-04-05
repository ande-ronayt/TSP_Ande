using System;
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
            crossover = new CrossoverOperatorAEX();
            //crossover = new CrosooverOperatorPMX();
            //mutationOperator = new MutationOperatorRandomTwoPoints();
            //mutationOperator = new MutationOperatorRSM();
            //mutationOperator = new MutationOperatorRandomAndThenRSM();
            //mutationOperator = new MutationOperatorPSM();
            mutationOperator = new MutationOperatorHalfRSMHalfPSM();
            //mutationOperator = new MutationOperatorInsertionsWithReverse();
           // mutationOperator = new MutationOperatorHalfRSMHalfInsertins();
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

   
    
}

