using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSPAnde.Lib;
using TSPAnde.Lib.GA;

namespace ProgramManager
{
    public class TSPManager
    {
        public DistanceOperator dOp { get; set; }
        
        public TSPAnde.Lib.GA.Environment Environment { get; set; }

        public Population Population { get; set; }
        
        public delegate void NextGenerationEventHandler(Population population);

        public event NextGenerationEventHandler NextGenerationEvent;

        public TSPManager(DistanceOperator dOp, int travellerAmount, int depoId, NextGenerationEventHandler nextGenerationEventHandler = null)
        {
            this.dOp = dOp;
            Environment = new TSPAnde.Lib.GA.Environment();
            Environment.TravelersAmount = travellerAmount;
            Environment.DepoId = depoId;
            DistanceOperator.InitializeOperator(dOp, Environment);
            Population = new Population(Environment);
            NextGenerationEvent = nextGenerationEventHandler;
        }

        public void Start(Func<Population, bool> stopFunc = null)
        {
            if (stopFunc == null)
                stopFunc = DefaultStopFunc;

            while (true)
            {
                Population.NextGeneration();
                if (NextGenerationEvent != null)
                {
                    NextGenerationEvent(Population);
                }

                if (stopFunc(Population))
                    break;
            }
        }

        public Chromosome GetTheBestChromosome()
        {
            return Population.TheBest;
        }

        public List<string> GetStringDistances()
        {
            var bestChromosome = GetTheBestChromosome();
            return SplitDistances(Environment.DepoId, bestChromosome.ToString());
        }

        public List<List<string>> GetFullDistancesInArray()
        {
            var tours = GetStringDistances();
            var response = new List<List<string>>();
            for (int j = 0; j < tours.Count; j++)
            {
                var ids = tours[j].Split('-').Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Trim());
                var t = ids.ToList();
                t.Add(t[0]);
                response.Add(t);
            }

            return response;
        }
        
        private List<string> SplitDistances(int depoId, string tour)
        {

            tour = tour.Substring(tour.IndexOf("-", tour.IndexOf("BD:")) + 1);
            var ids = tour.Split('-');
            var tours = new List<string>() { string.Empty };
            int curr = 0;
            for (int i = 0; i < ids.Length; i++)
            {
                if (int.Parse(ids[i]) == depoId)
                {
                    tours.Add(string.Empty);
                    curr++;
                }

                tours[curr] += "-" + ids[i];
            }

            tours[0] = tours.Last() + tours[0];
            tours.Remove(tours.Last());
            return tours;
        }

        private bool DefaultStopFunc(Population population)
        {
            if (population.CurrentStuckIteration == population.Environment.MaximumStuckIteration - 2)
                return true;
            return false;
        }
    }
}
