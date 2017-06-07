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
using TSPAnde.Lib.GA.Crossover;
using TSPAnde.Lib.GA.Mutation;

namespace WinFormApp
{
    public partial class ChooseOperatorForm : Form
    {
        public ChooseOperatorForm()
        {
            InitializeComponent();
            SetCrossover();
            SetBalance();
            SetMutation();
            
        }

        private void SetMutation()
        {
            switch (ChromosomeOperator.GetMutationOperator.Name)
            {
                case "MutationOperatorInsertion":
                    rbMutationInsertion.Checked = true;
                    break;
                case "MutationOperatorPSM":
                    rbMutationPSM.Checked = true;
                    break;
                case "MutationOperatorRSM":
                    rbMutationRSM.Checked = true;
                    break;
            }
        }

        private void SetCrossover()
        {
            switch (ChromosomeOperator.GetCrossoverType.Name)
            {
                case "CrossoverOperatorPMX":
                    rbCrossoverPMX.Checked = true;
                    break;
                case "CrossoverOperatorAEX" :
                    rbCrossoverAEX.Checked = true;
                    break;
                case "CrossoverOperatorOX":
                    rbCrossoverOX.Checked = true;
                    break;
            }
        }

        private void SetBalance()
        {
            switch (DistanceOperator.GetBalanceProportionType.Name)
            {
                case "BalanceProportionVarianceCalc":
                    rbBalanceProportionDispersion.Checked = true;
                    break;
                case "BalanceProportionMaxWithSumCalc":
                    rbBalanceProportionMaxSubtour.Checked = true;
                    break;
                case "BalanceProportionMinDevideByMax":
                    rbBalanceDeviding.Checked = true;
                    break; 
                case "BalanceProportionMinDevideByMaxWithPercent":
                    rbBalanceDevPlusPercent.Checked = true;
                    break; 
            }
        }
        #region Crossover
        private void rbCrossoverPMX_CheckedChanged(object sender, EventArgs e)
        {
            ChromosomeOperator.ChangeOperator(new CrossoverOperatorPMX());
        }

        private void rbCrossoverTwoPoint_CheckedChanged(object sender, EventArgs e)
        {
            ChromosomeOperator.ChangeOperator(new CrossoverOperatorOX());
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            ChromosomeOperator.ChangeOperator(new CrossoverOperatorAEX());
        }
        #endregion
       

        #region balance
        private void rbBalanceProportionDispersion_CheckedChanged(object sender, EventArgs e)
        {
            DistanceOperator.SetBalanceProportionCalc(new BalanceProportionVarianceCalc());
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            DistanceOperator.SetBalanceProportionCalc(new BalanceProportionMaxWithSumCalc());
        }

        private void rbDev_CheckedChanged(object sender, EventArgs e)
        {
            DistanceOperator.SetBalanceProportionCalc(new BalanceProportionMinDevideByMax());
        }

        private void rbBalanceDevPlusPercent_CheckedChanged(object sender, EventArgs e)
        {
            DistanceOperator.SetBalanceProportionCalc(new BalanceProportionMinDevideByMaxWithPercent());
        }
        #endregion

        #region mutation
        private void rbMutationInsertion_CheckedChanged(object sender, EventArgs e)
        {
            ChromosomeOperator.ChangeOperator(new MutationOperatorInsertions());
        }

        private void rbMutationPSM_CheckedChanged(object sender, EventArgs e)
        {
            ChromosomeOperator.ChangeOperator(new MutationOperatorPSM());
        }

        #endregion

        private void rbMutationRSM_CheckedChanged(object sender, EventArgs e)
        {
            ChromosomeOperator.ChangeOperator(new MutationOperatorRSM());
        }

        

       


    }
}
