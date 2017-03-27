using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using TSPAnde.Lib.GA;
using TspLibNet;

namespace WinFormApp
{
    public class MyReport
    {
        public static List<Timer> BestList { get; set; }

        public static TspLib95Item Problem { get; set; }

        public static string FileToSaveName
        {
            get { return Problem.Problem.Name + BestList.First().Time.ToString("yy-MM-dd-hh-mm-ss") + ".txt"; }
        }
        public static void StartTimer(Population population, TspLib95Item tspLibItem = null)
        {
            Problem = tspLibItem;
            BestList = new List<Timer>
            {
                new Timer(DateTime.Now, 1, population.BestOneFitChromosome)
            };
        }

        public static void CheckAndAddBest(Population population)
        {
            var alpha = population.Environment.Alpha;
            var beta = population.Environment.Beta;
            var newChromosome = population.BestOneFitChromosome;
            if (newChromosome.GetOneFit(alpha,beta) > BestList.Last().Chromosome.GetOneFit(alpha, beta))
            {
                BestList.Add(new Timer(DateTime.Now, population.CurrentGeneration, newChromosome));

                SaveToFile(FileToSaveName);
            }
        }

        private static void SaveToFile(string fileToSaveName)
        {
            using (var writer = new StreamWriter(fileToSaveName))
            {
                writer.WriteLine("Count: ");
                writer.WriteLine(BestList.Count);
                writer.WriteLine("Best Tour: ");
                writer.WriteLine(BestList.Last().Chromosome.Distance);
                writer.WriteLine("-------------------");
                for (int i = 1; i < BestList.Count; i++)
                {
                    writer.WriteLine(i + ": " + "Gen " + BestList[i].Generation + " :  Time  " + BestList[i].Time);
                    writer.WriteLine(BestList[i].Chromosome.ToString());
                }

            }
        }
    }

    public class Timer
    {
        public Timer(DateTime time, int generation, Chromosome chromosome)
        {
            Time = time;
            Generation = generation;
            Chromosome = chromosome;
        }

        public  DateTime Time { get; set; }

        public int Generation { get; set; }

        public Chromosome Chromosome { get; set; }
    }
}
