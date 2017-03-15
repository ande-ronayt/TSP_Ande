using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TspLibNet;
namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var tspLibPath = @"./TSPLIB95";
            Console.WriteLine(Path.GetFullPath(@"./TSPLIB95"));
            Console.ReadLine();
            //using (var writer = new StreamWriter(@"./test/text.txt"))
            //{
            //    writer.WriteLine("haha");
            //}

            TspLib95 lib = new TspLib95(tspLibPath);
            var tspList = lib.LoadAllTSP();
            Console.WriteLine(tspList.Count());
            Console.ReadLine();
        }
    }
}
