using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProgramManager;
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
            var dOp = new DistanceOperator(request.Count, request.DistancesMatrix);
            TSPManager tspManager = new TSPManager(dOp, request.TravellerAmount, request.Home);
            
            tspManager.Start();

            var response = tspManager.GetFullDistancesInArray();
            return Json(response);
        }

    }
}
