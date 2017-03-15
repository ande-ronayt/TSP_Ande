using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TSPAnde.Lib;
using TspLibNet;
namespace WinFormApp
{
    public partial class Form1 : Form
    {
        static string tspLibPath = "\\\\Mac\\Home\\Documents\\Visual Studio 2013\\Projects\\TSP_Ande\\TSPAnde\\TSPLIB95";
        ControlProgram cp = new ControlProgram();
        public Form1()
        {
            InitializeComponent();
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
            var tsp = tspList[5];
            //MessageBox.Show(tsp.ToString());
            //MessageBox.Show(File.Exists(Path.Combine(tspLibPath,"TSP",string.Concat(tsp.Problem.Name, ".tsp"))).ToString());

            var filePath = Path.Combine(tspLibPath, "TSP", string.Concat(tsp.Problem.Name, ".tsp"));
            using (var reader = new StreamReader(filePath))
            {
                MessageBox.Show(reader.ReadToEnd());
            }

            var problem = tsp.Problem;
            var Matrix = new double[16,16];
            var nodes = problem.NodeProvider.GetNodes();
            for (int i = 1; i <= 15; i++)
            {
                for (int j = 1; j <= 15; j++)
                {
                    Matrix[i, j] = problem.EdgeWeightsProvider.GetWeight(nodes[i - 1], nodes[j - 1]);
                }
            }

            DistanceOperator dOp = new DistanceOperator(tsp.Problem.NodeProvider.GetNodes().Count());
            dOp.CalculateDistance(tsp.Problem);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text += @"Hello
My name is

what about you  ? ";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }

    public class ControlProgram
    {
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
    }
}
