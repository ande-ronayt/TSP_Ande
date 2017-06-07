using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ProgramManager;
using TSPAnde.Lib;
using TSPAnde.Lib.GA;
using TSPAnde.Lib.GA.Crossover;
using TSPAnde.Lib.GA.Mutation;
using TspLibNet;
using System.Timers;
namespace ConsoleApp
{
    class Program
    {
        static int currentCity { get; set; }
        static long tick { get; set; }
        static List<double> timerInterval = new List<double>
            {
                2,
                10,
                15,
                20,
                100,
            };
        static List<double> duration = new List<double>
            {
                5,
                8,
                10,
                10,
                300
            };

        public static string tspLibPath =
            "C:\\Users\\greedy\\Desktop\\Lunwen\\Programming\\4.5\\TSP_Ande\\TSPAnde\\packages\\TSPLib.Net.1.1.5\\TSPLIB95";
        static void Main(string[] args)
        {
            //timerInterval = new List<double>
            //{
            //    100
            //};

            //duration = new List<double>
            //{
            //    600
            //};

            TspLib95 lib = new TspLib95(tspLibPath);
            var tspList = lib.LoadAllTSP().ToList();

            var tspItemNumbers = new List<int> {
                39 // - 21 city
                , 
                2 //- 49
                , 
                27 //- 70+
                , 
                25 //- 101
                , 
                44 //- 666
            };

            var tspItems = new Dictionary<int, TspLib95Item>();
            foreach (var id in tspItemNumbers)
            {
                tspItems.Add(id, tspList[id]);
            }

            List<ICrossoverOperator> crossovers = new List<ICrossoverOperator>()
            {
                new CrossoverOperatorOX(),
                new CrossoverOperatorPMX(),
                new CrossoverOperatorAEX(),
                //new CrossoverOperatorAEXWithShortestDistance()
            };

            List<IMutationOperator> mutations = new List<IMutationOperator>()
            {
                new MutationOperatorRSM(),
                new MutationOperatorInsertions(),
                new MutationOperatorPSM(),
                //new MutationOperatorHalfRSMHalfPSM(),
            };

            var number = 100;
            

            //int city = 0, cI = 0, mI = 0, nI = 0;
            //var dOp = new DistanceOperator(tspItems[tspItemNumbers[city]].Problem.NodeProvider.CountNodes());
            //dOp.CalculateDistance(tspItems[tspItemNumbers[city]].Problem);
            //ChromosomeOperator.ChangeOperator(crossovers[cI]);
            //ChromosomeOperator.ChangeOperator(mutations[mI]);
            //var reportManager = new ReportManager(number[nI], tspItemNumbers[city],
            //                    tspItems[tspItemNumbers[city]].Problem.Name);
            //var tspManager = new TSPManager(dOp, 1, 1, reportManager.NextGeneration);
            //tspManager.Start();
            //reportManager.EndOfAlgorithm(tspManager.Population);

            var indx = 0;
            

            var total = tspItemNumbers.Count*crossovers.Count*mutations.Count*number;
            for (int city = 0; city < tspItemNumbers.Count; city++)
            {
                var dOp = new DistanceOperator(tspItems[tspItemNumbers[city]].Problem.NodeProvider.CountNodes());
                dOp.CalculateDistance(tspItems[tspItemNumbers[city]].Problem);
                for (int cI = 0; cI < crossovers.Count; cI++)
                {
                    ChromosomeOperator.ChangeOperator(crossovers[cI]);
                    for (int mI = 0; mI < mutations.Count; mI++)
                    {
                        ChromosomeOperator.ChangeOperator(mutations[mI]);
                        for (int nI = 2; nI <= number; nI++)
                        {
                            Console.WriteLine("{0}%", (double)indx / total*100);
                            Console.WriteLine("start {0} :  {1} {2} {3} {4}", indx++,
                                tspItemNumbers[city],
                                crossovers[cI].GetType().Name,
                                mutations[mI].GetType().Name,
                                nI
                                );

                            var reportManager = new ReportManager(nI, tspItemNumbers[city],
                                tspItems[tspItemNumbers[city]].Problem.Name);
                            //var tspManager = new TSPManager(dOp, 1, 1, reportManager.NextGeneration);
                            var tspManager = new TSPManager(dOp, 1, 1, reportManager.NextGeneration);

                            var timer = new Timer(timerInterval[city]);
                            currentCity = city;
                            timer.Elapsed += NextTick;

                            SetUpNewTest(tspManager.Population, reportManager);
                            timer.Start();
                            tspManager.Start(StopFunc);
                            timer.Stop();
                            reportManager.EndOfAlgorithm(tspManager.Population);
                        }
                    }
                }
            }
        }

        public static Population Population { get; set; }
        public static ReportManager RManager { get; set; }
        public static DateTime StartedTime { get; set; }

        private static void SetUpNewTest(Population population, ReportManager rManager)
        {
            tick = 0;
            Population = population;
            RManager = rManager;
            StartedTime = DateTime.Now;
        }



        private static void NextTick(object sender, ElapsedEventArgs e)
        {
            RManager.NextTick(e.SignalTime, Population.TheBest.Distance, Population.CurrentGeneration);
            //RManager.NextTick(e.SignalTime, Population);
        }

        private static bool StopFunc(Population arg)
        {
            var t = DateTime.Now.Subtract(StartedTime);

            if ((t.Minutes*60+t.Seconds) >= duration[currentCity])
            {
                return true;
            }

            return false;
        }

    }
}
