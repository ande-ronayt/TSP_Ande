using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPAnde.Lib
{
    public static class Randomizer
    {
        public static Random Random { get; set; }

        static Randomizer()
        {
            Random = new Random();
        }
    }
}
