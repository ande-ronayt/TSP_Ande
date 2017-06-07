using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSPAnde.Lib.GA;
using TSPAnde.Lib.GA.Crossover;
using TSPAnde.Lib.GA.Mutation;
using TspLibNet;

namespace ProgramManager
{
    public class ReportManager
    {
        #region old
        public static List<rowData> BestList { get; set; }

        public static TspLib95Item Problem { get; set; }

        public static string FileToSaveName
        {
            get { return Problem.Problem.Name + BestList.First().Time.ToString("yy-MM-dd-hh-mm-ss") + ".txt"; }
        }
        public static void StartTimer(Population population, TspLib95Item tspLibItem = null)
        {
            Problem = tspLibItem;
            BestList = new List<rowData>
            {
                new rowData(DateTime.Now, 1, population.BestOneFitChromosome)
            };
        }

        public static void CheckAndAddBest(Population population)
        {
            var alpha = population.Environment.Alpha;
            var beta = population.Environment.Beta;
            var newChromosome = population.TheBest;
            if (newChromosome != null && newChromosome.GetOneFit(alpha, beta) > BestList.Last().Chromosome.GetOneFit(alpha, beta))
            {
                BestList.Add(new rowData(DateTime.Now, population.CurrentGeneration, newChromosome));

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

        public static int SkipElements = 1;

        public static int TakeElements
        {
            get { return BestList.Count > 20 + SkipElements ? 20 : BestList.Count - SkipElements; }
        }
        #endregion

        public ReportManager(int number, int cityCode, string tspProblem)
        {
            NumberOfTest = number;
            CityCode = cityCode;
            TspProblem = tspProblem;

            data.Add(new rowData(DateTime.Now, 0, null));
        }

        public string TspProblem { get; set; }

        public int NumberOfTest { get; set; }

        public int CityCode { get; set; }

        private List<rowData> data = new List<rowData>();

        public void NextGeneration(Population population)
        {
            if (TakeData(population))
            {
                var row = GetRowData(population, data[0].Time, DateTime.Now);
                data.Add(row);
            }
        }

        public void EndOfAlgorithm(Population population)
        {
            var fileName = GetFileName(population);
            SaveToFile(fileName, GetResultContent(population));
        }

        private void SaveToFile(string fileName, string content)
        {
            new FileInfo(fileName).Directory.Create();
            using (var writer = new StreamWriter(fileName))
            {
                writer.WriteLine(content);
            }
        } 

        private string GetResultContent(Population population)
        {
            var content = string.Empty;

            content += "Map: " + TspProblem + "\n";
            content += "Crossover: " + ChromosomeOperator.GetCrossoverType.Name + "\n";
            content += "Mutation: " + ChromosomeOperator.GetMutationOperator.Name + "\n";
            content += "Best chromosome: " + population.TheBest + "\n\n";
            content += "Tick\tWorkingTime\tGeneration\tDistance\tRealtime\tStartTime\tCrossover\tMutation\tTestNumber\n";

            var cType = ChromosomeOperator.GetCrossoverType;
            var cCode = (CrossoverType)Enum.Parse(typeof(CrossoverType), cType.Name);

            var mType = ChromosomeOperator.GetMutationOperator;
            var mCode = (MutationType)Enum.Parse(typeof(MutationType), mType.Name);


            //var indx = data.FindIndex(x => x.Chromosome != null && Math.Abs(x.Chromosome.Distance - data.Last().Chromosome.Distance) < 1e-6);
            //var dataToWrite = data.Take(indx + 100).ToList(); // take last 100 data
            var dataToWrite = data.Skip(1).ToList(); // because first one doesn't have any data
            foreach (var rd in dataToWrite)
            {
                rd.Timer = rd.Time.Subtract(data[0].Time);
            }

            dataToWrite = dataToWrite.OrderBy(x => x.Time).ToList();
            for (var i = 0; i < dataToWrite.Count; i++)
            {
                dataToWrite[i].Tick = i + 1;
            }

            for (var i = 0; i < dataToWrite.Count; i++)
            {
                content += (dataToWrite[i] + string.Format("\t{0}\t{1}\t{2}\t{3}\n", 
                    data[0].GetTime,
                    (int)cCode,
                    (int)mCode,
                    NumberOfTest
                    ));
            }

            return content;
        }

        private string GetFileName(Population population)
        {
            var cType = ChromosomeOperator.GetCrossoverType;
            var cCode = (CrossoverType)Enum.Parse(typeof(CrossoverType), cType.Name);

            var mType = ChromosomeOperator.GetMutationOperator;
            var mCode = (MutationType)Enum.Parse(typeof(MutationType), mType.Name);

            //CityCode _ CrossCode _ MutCode _ Number
            return string.Format("ReportManager\\{0}\\{0}_{1}_{2}_{3}.txt",
                CityCode,
                cCode,
                mCode,
                NumberOfTest
                );
        }


        private rowData GetRowData(Population population, DateTime startTime, DateTime time)
        {
            var rd = new rowData(time, population.CurrentGeneration, population.TheBest);
            return rd;
        }

        private rowData GetRowData(double distance, int gener, DateTime startTime, DateTime time)
        {
            var rd = new rowData(time, gener, distance);
            return rd;
        }

        private bool TakeData(Population population)
        {
            var interval = 50;
            if (population.Environment.CityAmount < 30)
                interval = 15;
            else if (population.Environment.CityAmount < 50)
                interval = 25;
            else if (population.Environment.CityAmount < 75)
                interval = 30;
            
            if (population.CurrentGeneration % interval == 0)
                return true;
            return false;
        }

        public void NextTick(DateTime time, Population population)
        {
            var row = GetRowData(population, data[0].Time, time);
            data.Add(row);
        }

        public void NextTick(DateTime time, double distance, int gener)
        {
            var row = GetRowData(distance, gener, data[0].Time, time);
            data.Add(row);
        }
    }

    public class rowData
    {
        public rowData(DateTime time, int generation, Chromosome chromosome)
        {
            Time = time;
            Generation = generation;
            Chromosome = chromosome;
        }

        public rowData(DateTime time, int generation, double distance)
        {
            Time = time;
            Generation = generation;
            Distance = distance;
        }

        public long Tick { get; set; }

        public DateTime Time { get; set; }

        public TimeSpan Timer { get; set; }

        public string GetTime
        {
            get { return Time.ToString("hh-mm-ss.fff"); }
        }

        public string GetTimer
        {
            get
            {
                return String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    Timer.Hours, Timer.Minutes, Timer.Seconds,
                    Timer.Milliseconds/10);
            }
        }

        public int Generation { get; set; }

        public Chromosome Chromosome { get; set; }

        public double Distance;

        public override string ToString()
        {
            double dis = Distance;
            if (Chromosome != null)
                dis = this.Chromosome.Distance;

            return string.Format("{4}\t{0}\t{1}\t{2}\t{3}",
                GetTimer,
                Generation,
                dis,
                GetTime,
                Tick);
        }
    }
}
