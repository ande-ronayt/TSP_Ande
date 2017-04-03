using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSPAnde.Lib;
using TSPAnde.Lib.GA;

namespace WebBaiduApp.Controllers
{
    public class BaiduApiMTspRequest{
        public int Count { get; set; }

        public int Home { get; set; }

        public int TravellerAmount { get; set; }

        public IList<IList<double>> DistancesMatrix { get; set; }
    }
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetShortestPath(BaiduApiMTspRequest request)
        {
            var response = new List<List<string>>();
            DistanceOperator dOp;
            dOp = new DistanceOperator(request.Count, request.DistancesMatrix);
            var environment = new TSPAnde.Lib.GA.Environment();
            environment.TravelersAmount = request.TravellerAmount;
            environment.DepoId = request.Home;
            DistanceOperator.InitializeOperator(dOp, environment);
            Population population = new Population(environment);

            while (true)
            {
                population.NextGeneration();
                if (StopGeneration(population))
                    break;
            }

            var bestChromosome = GetTheBest(population);

            var tours = SplitDistances(request.Home, bestChromosome.ToString());
            for (int j = 0; j < tours.Count; j++)
            {
                var ids = tours[j].Split('-').Where(x=>!string.IsNullOrEmpty(x)).Select(x=>x.Trim());
                var t = ids.ToList();
                t.Add(t[0]);
                response.Add(t);
            }
            return Json(response);
        }

        private static List<string> SplitDistances(int depoId, string tour)
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

        private Chromosome GetTheBest(Population population)
        {
            return population.TheBest;
        }

        private bool StopGeneration(Population population)
        {
            if (population.CurrentStuckIteration == population.Environment.MaximumStuckIteration - 2)
                return true;
            return false;
        }

    }
}
