using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TspLibNet;
using System.Drawing;
namespace TSPAnde.Lib
{

    public class DistanceOperator
    {
        public static IBalanceProportionCalc BalanceProportionCalc;
        public static Type GetBalanceProportionType { get { return BalanceProportionCalc.GetType(); } }
        public static void SetBalanceProportionCalc(IBalanceProportionCalc bpc)
        {
            BalanceProportionCalc = bpc;
        }

        static DistanceOperator()
        {
            //BalanceProportionCalc = new BalanceProportionMaxCalc();
            //BalanceProportionCalc = new BalanceProportionDispersionCalc();
            //BalanceProportionCalc = new BalanceProportionMaxWithSumCalc();
            BalanceProportionCalc = new BalanceProportionMaxDevideByMin();
        }

        public static double GetBalanceProportion(List<double> distances, double distance){
            return BalanceProportionCalc.GetBalanceProportion(distances, distance);
        }

        public static DistanceOperator dOperator;
        public static void InitializeOperator(DistanceOperator op, GA.Environment environment){
            environment.CityAmount = op.Size;
            dOperator = op;
        }

        public DistanceOperator(int count, List<Point> points)
        {
            Points = points;
            Size = count;
            CalculateDistance(points);
        }

        public void CalculateDistance(List<Point> points)
        {
            Matrix = new double[Size + 1, Size + 1];
            for (int i = 0; i < points.Count; i++)
            {
                for (int j = 0; j < points.Count; j++)
                {
                    var dx = points[i].X - points[j].X;
                    var dy = points[i].Y - points[j].Y;
                    Matrix[i + 1, j + 1] = Math.Sqrt(dx * dx + dy * dy);
                }
            }
        }

        public IProblem problem;
        public int Size { get; set; }

        public List<Point> Points { get; set; }

        public double[,] Matrix { get; set; }

        public double GetDistanceFromTo(int i, int j)
        {
            return Matrix[i, j];
        }

        public DistanceOperator(int size)
        {
            Size = size;
            Matrix = new double[size + 1, size + 1];
        }

        /// <summary>
        /// Calculate by using TspLib95 functions
        /// </summary>
        /// <param name="tspLibProblem"></param>
        public void CalculateDistance(IProblem tspLibProblem)
        {
            problem = tspLibProblem;
            var nodes = problem.NodeProvider.GetNodes();
            for (int i = 1; i <= Size; i++)
            {
                for (int j = 1; j <= Size; j++)
                {
                    Matrix[i,j] = problem.EdgeWeightsProvider.GetWeight(nodes[i-1],nodes[j-1]);
                }
            }
        }

        //public int[] FindIntersections(int[] path)
        //{
        //    int a, b, c, d;
        //    for (int i = 0; i < path.Length; i++)
        //    {
        //        a = path[i];
        //        b = path[(i + 1)%path.Length];
        //        c = path[(i + 2) % path.Length];
        //        d = path[(i + 3) % path.Length];
        //    }
        //}
    }

    public class BalanceProportionMaxDevideByMin : IBalanceProportionCalc
    {
        public double GetBalanceProportion(List<double> distances, double distance)
        {
            double max = distances[0];
            double min = distances[0];
            for (int i = 1; i < distances.Count; i++)
            {
                if (distances[i] > max) max = distances[i];
                if (distances[i] < min) min = distances[i];
            }

            if (min/max >= GA.Environment.BalanceCoefficient)
                return distance;
            
            return GA.Environment.BigDist;
        }
    }

    public class BalanceProportionMaxWithSumCalc : IBalanceProportionCalc
    {
        public double GetBalanceProportion(List<double> distances, double distance)
        {
            //find maximum:
            double max = distances[0];
            for (int i = 1; i < distances.Count; i++)
            {
                if (distances[i] > max) max = distances[i];
            }

            double temp = 0;
            for (int i = 0; i < distances.Count; i++)
            {
                temp += max - distances[i];
            }

            return max + temp;
        }
    }

    public class BalanceProportionMaxCalc : IBalanceProportionCalc
    {
        public double GetBalanceProportion(List<double> distances, double distance)
        {
            //find maximum:
            double max = distances[0];
            for (int i = 1; i < distances.Count; i++)
            {
                if (distances[i] > max) max = distances[i];
            }

            return max;
        }
    }

    public class BalanceProportionDispersionCalc : IBalanceProportionCalc
    {

        public double GetBalanceProportion(List<double> distances, double distance)
        {
            //----Find balanced tour:
            //find dispersion
            //1
            var aver = distance / distances.Count;

            //2
            var tmpSum = 0d;
            for (int i = 0; i < distances.Count; i++)
            {
                tmpSum += distances[i] * distances[i];
            }
            tmpSum /= distances.Count;

            //3
            var dispersion2 = tmpSum - aver * aver;
            var dispersion = Math.Sqrt((dispersion2));

            return dispersion;
        }
    }

    public interface IBalanceProportionCalc
    {
        double GetBalanceProportion(List<double> distances, double distance);
    }
}
