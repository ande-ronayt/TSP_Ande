using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TSPAnde.Lib;
using TSPAnde.Lib.GA;
using TspLibNet;
namespace WinFormApp
{
    public partial class Form1 : Form
    {
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            ControlProgram.RunPopulation.Abort();
        }

        string MYTODO = 
@"1. Сохранять элиту в список
2. TSP для подтуров
3. Рисовать элиту
4. Сохранять дженерейшин
5. Таймер

3. В Пейпере говорить об аутлаеров. об ";

        private Point MousePoint = new Point();

        public List<Point> Points { get; set; }

        static string tspLibPath = "\\\\Mac\\Home\\Documents\\Visual Studio 2013\\Projects\\TSP_Ande\\TSPAnde\\TSPLIB95";
        ControlProgram cp = new ControlProgram();
        public Form1()
        {
            InitializeComponent();
            Points = new List<Point>();
            MessageBox.Show(MYTODO);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cp.ChangeTspLib95Path(ref tspLibPath);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Get one
            TspLib95 lib = new TspLib95(tspLibPath);
            var tspList = lib.LoadAllTSP().ToList();
            var tsp = tspList[41]; //-24
            //var tsp = tspList[5]; // 29
            //var tsp = tspList[11];
            //MessageBox.Show(tsp.ToString());
            //MessageBox.Show(File.Exists(Path.Combine(tspLibPath,"TSP",string.Concat(tsp.Problem.Name, ".tsp"))).ToString());

            var filePath = Path.Combine(tspLibPath, "TSP", string.Concat(tsp.Problem.Name, ".tsp"));
            using (var reader = new StreamReader(filePath))
            {
                MessageBox.Show(reader.ReadToEnd());
            }

            ControlProgram.SetTspItem(tsp);
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var tsp = ControlProgram.tsp;
            DistanceOperator dOp = new DistanceOperator(tsp.Problem.NodeProvider.GetNodes().Count());
            dOp.CalculateDistance(tsp.Problem);
            var environment = new TSPAnde.Lib.GA.Environment();
            DistanceOperator.InitializeOperator(dOp, environment);
            Population population = new Population(environment);
            ControlProgram.Start(population, dOp, environment);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            this.MousePoint.X = e.X;
            this.MousePoint.Y = e.Y;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Points.Add(new Point(MousePoint.X, MousePoint.Y));
            var g = pictureBox1.CreateGraphics();
            DrawAPoint(g, Points.Last(), Color.Red);
        }

        private void DrawAPoint(Graphics g, Point point, Color color)
        {
            g.FillEllipse(new SolidBrush(color), point.X - 2, point.Y - 2, 4, 4);
        }

        /// <summary>
        /// Create a problem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            DistanceOperator dOp = new DistanceOperator(this.Points.Count, Points);
            var environment = new TSPAnde.Lib.GA.Environment();
            DistanceOperator.InitializeOperator(dOp, environment);
            Population population = new Population(environment);
            ControlProgram.Start(population, dOp, environment);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
           ControlProgram.RunPopulation = new Thread(Process);
           ControlProgram.RunPopulation.Start();
        }

        public void Process()
        {
            //var count = int.Parse(txtGenerCount.Text);
            while (true)
            {
                ControlProgram.Population.NextGeneration();
                SetControlPropertyThreadSafe(lblCurGen, "Text","Gen: " + ControlProgram.Population.CurrentGeneration);
                //lblCurGen.Text = "Gen: " + ControlProgram.Population.CurrentGeneration;

                var p = ControlProgram.Population;
                
                if (ControlProgram.Environment.IsuseOneFit)
                {
                    if (p.BestOneFitChromosome.ToString(ControlProgram.Environment) == ControlProgram.LastTour)
                        continue;

                    SetControlPropertyThreadSafe(textBox1, "Text",
                        string.Format(
                            @"Distance: {0}  
Fit {1} 
Fit1 {4}
Fit2 {5}
Tour with Fit1: {2}
Best from problem: {3}",
                            p.BestOneFitChromosome.Distance, p.BestOneFit, p.BestOneFitChromosome, "null",
                            p.BestOneFitChromosome.Fit1, p.BestOneFitChromosome.Fit2));
                    //ControlProgram.tsp.OptimalTourDistance??"null");
                    DrawATour(p.BestOneFitChromosome.ToString(ControlProgram.Environment), Color.Green);
                }
                else
                {
                    textBox1.Text = string.Format(
                        @"Distance: {0}  
Fit1: {1} 
Fit2: {2} 
Tour with Fit1: {3} 
Tour with Fit2 {4}
Best from problem: {5}",
                        p.BestFit1Chromosome.Distance, p.BestFit1, p.BestFit2, p.BestFit1Chromosome, p.BestFit2Chromosome,
                        "null"); //ControlProgram.tsp.OptimalTourDistance??"null");
                    DrawATour(p.BestFit2Chromosome.ToString(ControlProgram.Environment), Color.Green); 
                }
            }

            
        }

        public void DrawATour(string tour, Color color)
        {
            Color[] colors = {Color.Red, Color.Blue, Color.Green, Color.Black, Color.MediumSlateBlue};
            
            if (ControlProgram.LastTour != tour)
            {
                ControlProgram.LastTour = tour;

                var tours = ControlProgram.SplitDistances(tour);
                var g = pictureBox1.CreateGraphics();
                g.Clear(Color.White);
                for (int j = 0; j < tours.Count; j++)
                {

                    var ids = tours[j].Split('-');
                    var pen = new Pen(colors[j%colors.Length]);
                    for (int i = 1; i < ids.Length - 1; i++)
                    {
                        g.DrawLine(pen, Points[int.Parse(ids[i]) - 1], Points[int.Parse(ids[i + 1]) - 1]);
                    }
                    g.DrawLine(pen, Points[int.Parse(ids[ids.Length - 1]) - 1], Points[int.Parse(ids[1]) - 1]);    
                }
                
            }
        }

        #region thread safe

        private delegate void SetControlPropertyThreadSafeDelegate(
    Control control,
    string propertyName,
    object propertyValue);

        public static void SetControlPropertyThreadSafe(
            Control control,
            string propertyName,
            object propertyValue)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new SetControlPropertyThreadSafeDelegate
                (SetControlPropertyThreadSafe),
                new object[] { control, propertyName, propertyValue });
            }
            else
            {
                control.GetType().InvokeMember(
                    propertyName,
                    BindingFlags.SetProperty,
                    null,
                    control,
                    new object[] { propertyValue });
            }
        }
        #endregion

        #region AlphaBetaGA
        private void txtAlphaMax_TextChanged(object sender, EventArgs e)
        {
            int number;
            if (int.TryParse(txtAlphaMax.Text, out number))
            {
                scAlpha.Maximum = number;
            }
        }

        private void txtBetaMax_TextChanged(object sender, EventArgs e)
        {
            int number;
            if (int.TryParse(txtBetaMax.Text, out number))
            {
                scBeta.Maximum = number;
            }
        }

        private void scAlpha_Scroll(object sender, ScrollEventArgs e)
        {
            txtAlpha.Text = scAlpha.Value.ToString();
        }

        private void scBeta_Scroll(object sender, ScrollEventArgs e)
        {
            txtBeta.Text = scBeta.Value.ToString();
        }

        private void txtAlpha_TextChanged(object sender, EventArgs e)
        {
            double number;
            if (double.TryParse(txtAlpha.Text, out number))
            {
                ControlProgram.Environment.Alpha = number;
            }
        }

        private void txtBeta_TextChanged(object sender, EventArgs e)
        {
            double number;
            if (double.TryParse(txtBeta.Text, out number))
            {
                ControlProgram.Environment.Beta = number;
            }
        }
        #endregion
    }

    



    public class ControlProgram
    {
        #region TspLibPath
        static string tspLibPath = "\\\\Mac\\Home\\Documents\\Visual Studio 2013\\Projects\\TSP_Ande\\TSPAnde\\TSPLIB95";

        public string GetLibPath
        {
            get { return tspLibPath; }
        }

        public void ChangeTspLib95Path(ref string path)
        {
            OpenFileDialog odf = new OpenFileDialog();
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = Path.GetFullPath(@".");
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tspLibPath = fbd.SelectedPath;
                tspLibPath = Path.GetFullPath(tspLibPath);
                MessageBox.Show(tspLibPath);
                path = tspLibPath;
            }
        }
        #endregion
        #region TspLibItem
        public static TspLib95Item tsp;
        public static void SetTspItem(TspLib95Item t)
        {
            tsp = t;
        }
        #endregion

        public static Thread RunPopulation;

        public static Population Population { get; set; }

        public static List<string> SplitDistances(string tour)
        {
            tour = tour.Substring(tour.IndexOf("-")+1);
            var ids = tour.Split('-');
            var depoId = Environment.depoId;
            var tours = new List<string>() {string.Empty};
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

        public static TSPAnde.Lib.GA.Environment Environment { get; set; }
        public static void Start(Population population, DistanceOperator dOp, TSPAnde.Lib.GA.Environment environment)
        {
            Environment = environment;
            Population = population;
           // population.Init(travelers: TSPAnde.Lib.GA.Environment.travelers, depo : 1);
        }

        public static string LastTour { get; set; }
    }

}