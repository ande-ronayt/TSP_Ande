using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TSPAnde.Lib;
using TSPAnde.Lib.GA;

namespace WinFormApp
{
    public partial class ChooseOperatorForm : Form
    {
        public ChooseOperatorForm()
        {
            InitializeComponent();
            SetCrossover();
            SetBalance();
        }

        private void SetCrossover()
        {
            switch (ChromosomeOperator.GetCrossoverType.Name)
            {
                case "CrosooverOperatorPMX":
                    rbCrossoverPMX.Checked = true;
                    break;
                case "CrossoverOperatorTwoPoints" :
                    rbCrossoverTwoPoint.Checked = true;
                    break;
            }
        }

        private void SetBalance()
        {
            switch (DistanceOperator.GetBalanceProportionType.Name)
            {
                case "BalanceProportionDispersionCalc":
                    rbBalanceProportionDispersion.Checked = true;
                    break;
                case "BalanceProportionMaxCalc":
                    rbBalanceProportionMaxSubtour.Checked = true;
                    break;
            }
        }

        private void rbCrossoverPMX_CheckedChanged(object sender, EventArgs e)
        {
            ChromosomeOperator.ChangeOperator(new CrosooverOperatorPMX());
        }

        private void rbCrossoverTwoPoint_CheckedChanged(object sender, EventArgs e)
        {
            ChromosomeOperator.ChangeOperator(new CrossoverOperatorOX());
        }

        private void rbBalanceProportionDispersion_CheckedChanged(object sender, EventArgs e)
        {
            DistanceOperator.SetBalanceProportionCalc(new BalanceProportionDispersionCalc());
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            DistanceOperator.SetBalanceProportionCalc(new BalanceProportionMaxCalc());
        }

    }
}
