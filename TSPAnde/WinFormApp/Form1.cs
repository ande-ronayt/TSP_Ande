﻿using System;
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
using ProgramManager;
using TSPAnde.Lib;
using TSPAnde.Lib.GA;
using TspLibNet;
using TspLibNet.Graph.Nodes;
namespace WinFormApp
{
    public partial class Form1 : Form
    {
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            try
            {
                ControlProgram.RunPopulation.Abort();
            }
            catch { }
        }

        private Point MousePoint = new Point();

        public List<Point> Points { get; set; }

        static string tspLibPath = "TSPLIB95"; //"C:\\Users\\greedy\\Desktop\\Lunwen\\Programming\\4.5\\TSP_Ande\\TSPAnde\\packages\\TSPLib.Net.1.1.5\\TSPLIB95"
        ControlProgram cp = new ControlProgram();
        public Form1()
        {
            InitializeComponent();
            IsProgramAlive = true;
            Points = new List<Point>();
            DisplayTspLib95Data();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cp.ChangeTspLib95Path(ref tspLibPath);
            DisplayTspLib95Data();
        }

        private void DisplayTspLib95Data()
        {
            TspLib95 lib;
            List<TspLib95Item> tspList;
            try
            {
                lib = new TspLib95(tspLibPath);
                tspList = lib.LoadAllTSP().ToList();
            }
            catch(Exception){
                MessageBox.Show("Please choose TspLib95 path");
                return;
            }

            var info = "";
            
            for(var i=0; i< tspList.Count; i++)
            {
                info += string.Format(@"{0}:  {1}

", i, tspList[i].ToString());
            }

            textBox1.Text = info;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int number;
            if (!int.TryParse(txtTspLibChooseOne.Text, out number))
            {
                return;
            }
            //Get one
            TspLib95 lib = new TspLib95(tspLibPath);
            var tspList = lib.LoadAllTSP().ToList();
            var tsp = tspList[number]; //-24
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
            TransferTspLibItemToPoints();
            /*btnCreateProblem.Enabled = false;
            btnRun.Enabled = true;
            btnChooseOperator.Enabled = true;*/

        }

        private void button3_Click(object sender, EventArgs e)
        {/*
            var tsp = ControlProgram.tsp;
            DistanceOperator dOp = new DistanceOperator(tsp.Problem.NodeProvider.GetNodes().Count());
            dOp.CalculateDistance(tsp.Problem);
            var environment = new TSPAnde.Lib.GA.Environment();
            DistanceOperator.InitializeOperator(dOp, environment);
            Population population = new Population(environment);
            ControlProgram.Start(population, dOp, environment);*/
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            this.MousePoint.X = e.X;
            this.MousePoint.Y = e.Y;
        }

        private void AddPoint(int x, int y)
        {
            Points.Add(new Point(x, y));
            lblCityAmount.Text = Points.Count.ToString();
            //Add to depo list:
            cmbDepoId.Items.Add(Points.Count);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            AddPoint(MousePoint.X, MousePoint.Y);
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
        private void button5_Click(object sender, EventArgs e) // create a problem
        {
            int travelers;
            if (!int.TryParse(txtTravelersAmount.Text, out travelers))
            {
                MessageBox.Show("Please write a number");
                return;
            }

            DistanceOperator dOp;
            if (ControlProgram.tsp != null) // Load From TSPLib95
            {
                TransferTspLibItemToPoints();
                dOp = new DistanceOperator(ControlProgram.tsp.Problem.NodeProvider.CountNodes());
                dOp.CalculateDistance(ControlProgram.tsp.Problem);
            }
            else
            {
                dOp = new DistanceOperator(this.Points.Count, Points);
            }

            TspManager = new TSPManager(dOp, travelers, int.Parse(cmbDepoId.Text), NextGeneration);
            ReportManager pManager = new ReportManager(1, 2, ControlProgram.tsp.Problem.Name);
            TspManager.NextGenerationEvent += pManager.NextGeneration;

            ControlProgram.Start(TspManager.Population, TspManager.dOp, TspManager.Environment);
            btnRun.Enabled = true;
            btnChooseOperator.Enabled = true;

        }

        public void NextGeneration(Population population)
        {
            SetControlPropertyThreadSafe(lblCurGen, "Text", "Gen: " + population.CurrentGeneration);
            MyReport.CheckAndAddBest(ControlProgram.Population);
            //lblCurGen.Text = "Gen: " + ControlProgram.Population.CurrentGeneration;

            var p = population;
            //GetBestFromProblem
            var bestFromProblem = "null";
            if (ControlProgram.tsp != null)
            {
                bestFromProblem = "Dis= " + ControlProgram.tsp.OptimalTourDistance;
            }

            if (population.Environment.IsuseOneFit)
            {
                if (MyReport.BestList.Count > 1 && MyReport.BestList.Last().Chromosome.ToString(population.Environment) == ControlProgram.LastTour)
                    return;
                SetControlPropertyThreadSafe(textBox1, "Text",
                    string.Format(
                        @"Distance: {0}  
Fit {1} 
Fit1 {4}
Fit2 {5}
Tour with Fit1: {2}
BestList from problem: {3}",
                        p.BestOneFitChromosome.Distance,    //1
                        p.BestOneFit,                       //2
                        p.BestOneFitChromosome,             //3 
                        bestFromProblem,                    //4
                        p.BestOneFitChromosome.Fit1,        //5
                        p.BestOneFitChromosome.Fit2));      //6

                DrawATour(p.BestOneFitChromosome.ToString(population.Environment), Color.Green);
            }
            #region two fit don't use
            //two fit don't use
            else
            {
                textBox1.Text = string.Format(
                    @"Distance: {0}  
Fit1: {1} 
Fit2: {2} 
Tour with Fit1: {3} 
Tour with Fit2 {4}
BestList from problem: {5}",
                    p.BestFit1Chromosome.Distance, p.BestFit1, p.BestFit2, p.BestFit1Chromosome, p.BestFit2Chromosome,
                    "null"); //ControlProgram.tsp.OptimalTourDistance??"null");
                DrawATour(p.BestFit2Chromosome.ToString(ControlProgram.Environment), Color.Green);
            }
            #endregion
        }

        public TSPManager TspManager { get; set; }

        private void TransferTspLibItemToPoints()
        {
            //if Nodes are 2D
            Points.Clear();
            var nodes = ControlProgram.tsp.Problem.NodeProvider.GetNodes();
            if (nodes[0] is Node2D)
            {
                var nodes2D = nodes.Select(x=>(Node2D)x).OrderBy(x=>x.Id).ToList();
                var maxX = nodes2D.Max(x => x.X);
                var maxY = nodes2D.Max(x => x.Y);
                var dX = pictureBox1.Width*0.66 / maxX;
                var dY = pictureBox1.Height*0.66 / maxY;
                foreach (var node in nodes2D)
                {
                    AddPoint((int)(node.X*dX), (int)(node.Y*dY));
                }

                DrawAllPoints(1);
            }
        }

        //Thread Start
        private void button6_Click(object sender, EventArgs e)
        {
            if (this.IsStarted)
            {
                if (this.IsPaused)
                {
                    ControlProgram.RunPopulation.Resume();
                    this.IsPaused = false;
                    btnRun.Text = "Suspend";
                }
                else
                {
                    ControlProgram.RunPopulation.Suspend();
                    this.IsPaused = true;
                    btnRun.Text = "Resume";
                }
            }
            else
            {
                this.IsStarted = true;
                btnRun.Text = "Suspend";
                MyReport.StartTimer(ControlProgram.Population, ControlProgram.tsp);
                ControlProgram.RunPopulation = new Thread(Process);
                ControlProgram.RunPopulation.Start();
            }
        }

        public bool IsProgramAlive { get; set; }

        public void Process()
        {
            TspManager.Start((x => !IsProgramAlive));
            //while (true)
            while(false)
            {
                
                ControlProgram.Population.NextGeneration();
                SetControlPropertyThreadSafe(lblCurGen, "Text","Gen: " + ControlProgram.Population.CurrentGeneration);
                MyReport.CheckAndAddBest(ControlProgram.Population);
                //lblCurGen.Text = "Gen: " + ControlProgram.Population.CurrentGeneration;

                var p = ControlProgram.Population;
                //GetBestFromProblem
                var bestFromProblem = "null";
                if (ControlProgram.tsp != null)
                {
                    bestFromProblem = "Dis= " + ControlProgram.tsp.OptimalTourDistance;
                }

                if (ControlProgram.Environment.IsuseOneFit)
                {
                    if (MyReport.BestList.Count > 1 && MyReport.BestList.Last().Chromosome.ToString(ControlProgram.Environment) == ControlProgram.LastTour)
                        continue;
                    SetControlPropertyThreadSafe(textBox1, "Text",
                        string.Format(
                            @"Distance: {0}  
Fit {1} 
Fit1 {4}
Fit2 {5}
Tour with Fit1: {2}
BestList from problem: {3}",
                            p.BestOneFitChromosome.Distance,    //1
                            p.BestOneFit,                       //2
                            p.BestOneFitChromosome,             //3 
                            bestFromProblem,                    //4
                            p.BestOneFitChromosome.Fit1,        //5
                            p.BestOneFitChromosome.Fit2));      //6

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
BestList from problem: {5}",
                        p.BestFit1Chromosome.Distance, p.BestFit1, p.BestFit2, p.BestFit1Chromosome, p.BestFit2Chromosome,
                        "null"); //ControlProgram.tsp.OptimalTourDistance??"null");
                    DrawATour(p.BestFit2Chromosome.ToString(ControlProgram.Environment), Color.Green); 
                }

                
            }

            
        }

        public void DrawATour(string tour, Color color)
        {
            if (this.Points.Count == 0) return;
            Color[] colors = {Color.Red, Color.Blue, Color.Green, Color.Black, Color.MediumSlateBlue};
            
            if (ControlProgram.LastTour != tour)
            {
                ControlProgram.LastTour = tour;

                var tours = ControlProgram.SplitDistances(tour);
                var g = pictureBox1.CreateGraphics();
                g.Clear(Color.White);
                DrawAllPoints(ControlProgram.Environment.DepoId);
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
                if (number <= 0.0001) //if (number == 0 & ControlProgram.Environment.Beta == 0)
                {
                    txtAlpha.Text = "1";
                    return;
                }
                ControlProgram.Environment.Alpha = number;
            }
        }

        private void txtBeta_TextChanged(object sender, EventArgs e)
        {
            double number;
            if (double.TryParse(txtBeta.Text, out number))
            {
                if (number <= 0.0001) //if (number == 0 & ControlProgram.Environment.Alpha == 0)
                {
                    txtBeta.Text = "1";
                    return;
                }
                ControlProgram.Environment.Beta = number;
            }
        }
        #endregion

        private void btnChooseOperator_Click(object sender, EventArgs e)
        {
            var chooseOperatorForm = new ChooseOperatorForm();
            if (this.IsStarted)
            {
                ControlProgram.RunPopulation.Suspend();
                this.IsPaused = true;
                btnRun.Text = "Resume";
            }
            //button6_Click(sender, e);
            chooseOperatorForm.ShowDialog();
        }

        public bool IsStarted { get; set; }

        public bool IsPaused { get; set; }

        private void cmbDepoId_SelectedIndexChanged(object sender, EventArgs e)
        {
            var id = int.Parse(cmbDepoId.Text);
            DrawAllPoints(id);
            
        }

        private void DrawAllPoints(int depoId)
        {
            var g = pictureBox1.CreateGraphics();
            foreach (var item in Points)
            {
                DrawAPoint(g, item, Color.Red);
            }
            DrawAPoint(g, Points[depoId - 1], Color.Blue);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.IsProgramAlive = false;
        }

    }

    



    public class ControlProgram
    {
        #region TspLibPath
        static string tspLibPath = "\\\\Mac\\Home\\Documents\\Visual Studio 2013\\Projects\\TSPLIB95";//"\\\\Mac\\Home\\Documents\\Visual Studio 2013\\Projects\\TSP_Ande\\TSPAnde\\TSPLIB95";

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

            tour = tour.Substring(tour.IndexOf("-", tour.IndexOf("BD:")) + 1);
            var ids = tour.Split('-');
            var depoId = Environment.DepoId;
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