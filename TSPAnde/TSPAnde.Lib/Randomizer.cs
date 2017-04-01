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

        public static void Shuffle<T>(this List<T> list)
        {
            for (int i = 0, j; i < list.Count; i++)
            {
                j = Random.Next(list.Count);
                //permute
                var tmp = list[i];
                list[i] = list[j];
                list[j] = tmp;
            }
        }
    }
}
