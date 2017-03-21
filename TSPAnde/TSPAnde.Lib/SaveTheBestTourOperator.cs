using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSPAnde.Lib.GA;

namespace TSPAnde.Lib
{
    public static class SaveTheBestTourOperator
    {

    }

    public class SaveTheBestTourForGA : ISaveTheBestTourForGA{

        public List<Chromosome> SuperElit { get; set; }

        public void AddNewOne(Chromosome chromosome)
        {
            //if (SuperF
            //    )
            throw new NotImplementedException();
        }
    }

    public interface ISaveTheBestTourForGA
    {
        List<Chromosome> SuperElit { get; set; } 

        void AddNewOne(Chromosome chromosome);
    }
}
