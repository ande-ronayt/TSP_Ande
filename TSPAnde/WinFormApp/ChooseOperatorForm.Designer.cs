namespace WinFormApp
{
    partial class ChooseOperatorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbCrossoverPMX = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbBalanceProportionMaxSubtour = new System.Windows.Forms.RadioButton();
            this.rbBalanceProportionDispersion = new System.Windows.Forms.RadioButton();
            this.rbBalanceDeviding = new System.Windows.Forms.RadioButton();
            this.rbCrossoverAEX = new System.Windows.Forms.RadioButton();
            this.rbCrossoverOX = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbMutationRSM = new System.Windows.Forms.RadioButton();
            this.rbMutationPSM = new System.Windows.Forms.RadioButton();
            this.rbMutationInsertion = new System.Windows.Forms.RadioButton();
            this.rbBalanceDevPlusPercent = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbCrossoverOX);
            this.groupBox1.Controls.Add(this.rbCrossoverAEX);
            this.groupBox1.Controls.Add(this.rbCrossoverPMX);
            this.groupBox1.Location = new System.Drawing.Point(36, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(156, 202);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Crossover Operator";
            // 
            // rbCrossoverPMX
            // 
            this.rbCrossoverPMX.AutoSize = true;
            this.rbCrossoverPMX.Location = new System.Drawing.Point(18, 32);
            this.rbCrossoverPMX.Name = "rbCrossoverPMX";
            this.rbCrossoverPMX.Size = new System.Drawing.Size(41, 16);
            this.rbCrossoverPMX.TabIndex = 0;
            this.rbCrossoverPMX.Text = "PMX";
            this.rbCrossoverPMX.UseVisualStyleBackColor = true;
            this.rbCrossoverPMX.CheckedChanged += new System.EventHandler(this.rbCrossoverPMX_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbBalanceDevPlusPercent);
            this.groupBox2.Controls.Add(this.rbBalanceDeviding);
            this.groupBox2.Controls.Add(this.rbBalanceProportionMaxSubtour);
            this.groupBox2.Controls.Add(this.rbBalanceProportionDispersion);
            this.groupBox2.Location = new System.Drawing.Point(425, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(179, 202);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Balance Proportion Operator";
            // 
            // rbBalanceProportionMaxSubtour
            // 
            this.rbBalanceProportionMaxSubtour.AutoSize = true;
            this.rbBalanceProportionMaxSubtour.Location = new System.Drawing.Point(22, 70);
            this.rbBalanceProportionMaxSubtour.Name = "rbBalanceProportionMaxSubtour";
            this.rbBalanceProportionMaxSubtour.Size = new System.Drawing.Size(113, 16);
            this.rbBalanceProportionMaxSubtour.TabIndex = 0;
            this.rbBalanceProportionMaxSubtour.Text = "max + (max-min)";
            this.rbBalanceProportionMaxSubtour.UseVisualStyleBackColor = true;
            this.rbBalanceProportionMaxSubtour.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // rbBalanceProportionDispersion
            // 
            this.rbBalanceProportionDispersion.AutoSize = true;
            this.rbBalanceProportionDispersion.Location = new System.Drawing.Point(22, 32);
            this.rbBalanceProportionDispersion.Name = "rbBalanceProportionDispersion";
            this.rbBalanceProportionDispersion.Size = new System.Drawing.Size(71, 16);
            this.rbBalanceProportionDispersion.TabIndex = 0;
            this.rbBalanceProportionDispersion.Text = "Variance";
            this.rbBalanceProportionDispersion.UseVisualStyleBackColor = true;
            this.rbBalanceProportionDispersion.CheckedChanged += new System.EventHandler(this.rbBalanceProportionDispersion_CheckedChanged);
            // 
            // rbBalanceDeviding
            // 
            this.rbBalanceDeviding.AutoSize = true;
            this.rbBalanceDeviding.Checked = true;
            this.rbBalanceDeviding.Location = new System.Drawing.Point(22, 104);
            this.rbBalanceDeviding.Name = "rbBalanceDeviding";
            this.rbBalanceDeviding.Size = new System.Drawing.Size(65, 16);
            this.rbBalanceDeviding.TabIndex = 0;
            this.rbBalanceDeviding.Text = "min/max";
            this.rbBalanceDeviding.UseVisualStyleBackColor = true;
            this.rbBalanceDeviding.CheckedChanged += new System.EventHandler(this.rbDev_CheckedChanged);
            // 
            // rbCrossoverAEX
            // 
            this.rbCrossoverAEX.AutoSize = true;
            this.rbCrossoverAEX.Location = new System.Drawing.Point(18, 70);
            this.rbCrossoverAEX.Name = "rbCrossoverAEX";
            this.rbCrossoverAEX.Size = new System.Drawing.Size(41, 16);
            this.rbCrossoverAEX.TabIndex = 0;
            this.rbCrossoverAEX.Text = "AEX";
            this.rbCrossoverAEX.UseVisualStyleBackColor = true;
            this.rbCrossoverAEX.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // rbCrossoverOX
            // 
            this.rbCrossoverOX.AutoSize = true;
            this.rbCrossoverOX.Checked = true;
            this.rbCrossoverOX.Location = new System.Drawing.Point(18, 107);
            this.rbCrossoverOX.Name = "rbCrossoverOX";
            this.rbCrossoverOX.Size = new System.Drawing.Size(35, 16);
            this.rbCrossoverOX.TabIndex = 0;
            this.rbCrossoverOX.TabStop = true;
            this.rbCrossoverOX.Text = "OX";
            this.rbCrossoverOX.UseVisualStyleBackColor = true;
            this.rbCrossoverOX.CheckedChanged += new System.EventHandler(this.rbCrossoverTwoPoint_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbMutationRSM);
            this.groupBox3.Controls.Add(this.rbMutationPSM);
            this.groupBox3.Controls.Add(this.rbMutationInsertion);
            this.groupBox3.Location = new System.Drawing.Point(228, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(156, 202);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Mutation Operator";
            // 
            // rbMutationRSM
            // 
            this.rbMutationRSM.AutoSize = true;
            this.rbMutationRSM.Location = new System.Drawing.Point(18, 104);
            this.rbMutationRSM.Name = "rbMutationRSM";
            this.rbMutationRSM.Size = new System.Drawing.Size(41, 16);
            this.rbMutationRSM.TabIndex = 0;
            this.rbMutationRSM.Text = "RSM";
            this.rbMutationRSM.UseVisualStyleBackColor = true;
            this.rbMutationRSM.CheckedChanged += new System.EventHandler(this.rbMutationRSM_CheckedChanged);
            // 
            // rbMutationPSM
            // 
            this.rbMutationPSM.AutoSize = true;
            this.rbMutationPSM.Location = new System.Drawing.Point(18, 70);
            this.rbMutationPSM.Name = "rbMutationPSM";
            this.rbMutationPSM.Size = new System.Drawing.Size(41, 16);
            this.rbMutationPSM.TabIndex = 0;
            this.rbMutationPSM.Text = "PSM";
            this.rbMutationPSM.UseVisualStyleBackColor = true;
            this.rbMutationPSM.CheckedChanged += new System.EventHandler(this.rbMutationPSM_CheckedChanged);
            // 
            // rbMutationInsertion
            // 
            this.rbMutationInsertion.AutoSize = true;
            this.rbMutationInsertion.Location = new System.Drawing.Point(18, 32);
            this.rbMutationInsertion.Name = "rbMutationInsertion";
            this.rbMutationInsertion.Size = new System.Drawing.Size(77, 16);
            this.rbMutationInsertion.TabIndex = 0;
            this.rbMutationInsertion.Text = "Insertion";
            this.rbMutationInsertion.UseVisualStyleBackColor = true;
            this.rbMutationInsertion.CheckedChanged += new System.EventHandler(this.rbMutationInsertion_CheckedChanged);
            // 
            // rbBalanceDevPlusPercent
            // 
            this.rbBalanceDevPlusPercent.AutoSize = true;
            this.rbBalanceDevPlusPercent.Location = new System.Drawing.Point(22, 138);
            this.rbBalanceDevPlusPercent.Name = "rbBalanceDevPlusPercent";
            this.rbBalanceDevPlusPercent.Size = new System.Drawing.Size(89, 16);
            this.rbBalanceDevPlusPercent.TabIndex = 0;
            this.rbBalanceDevPlusPercent.Text = "min/max + %";
            this.rbBalanceDevPlusPercent.UseVisualStyleBackColor = true;
            this.rbBalanceDevPlusPercent.CheckedChanged += new System.EventHandler(this.rbBalanceDevPlusPercent_CheckedChanged);
            // 
            // ChooseOperatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 280);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "ChooseOperatorForm";
            this.Text = "ChooseOperatorForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbCrossoverPMX;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbBalanceProportionMaxSubtour;
        private System.Windows.Forms.RadioButton rbBalanceProportionDispersion;
        private System.Windows.Forms.RadioButton rbBalanceDeviding;
        private System.Windows.Forms.RadioButton rbCrossoverOX;
        private System.Windows.Forms.RadioButton rbCrossoverAEX;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbMutationRSM;
        private System.Windows.Forms.RadioButton rbMutationPSM;
        private System.Windows.Forms.RadioButton rbMutationInsertion;
        private System.Windows.Forms.RadioButton rbBalanceDevPlusPercent;
    }
}