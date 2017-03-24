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
            this.rbCrossoverTwoPoint = new System.Windows.Forms.RadioButton();
            this.rbCrossoverPMX = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbBalanceProportionDispersion = new System.Windows.Forms.RadioButton();
            this.rbBalanceProportionMaxSubtour = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbCrossoverTwoPoint);
            this.groupBox1.Controls.Add(this.rbCrossoverPMX);
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(158, 130);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Crossover Operator";
            // 
            // rbCrossoverTwoPoint
            // 
            this.rbCrossoverTwoPoint.AutoSize = true;
            this.rbCrossoverTwoPoint.Checked = true;
            this.rbCrossoverTwoPoint.Location = new System.Drawing.Point(18, 76);
            this.rbCrossoverTwoPoint.Name = "rbCrossoverTwoPoint";
            this.rbCrossoverTwoPoint.Size = new System.Drawing.Size(118, 17);
            this.rbCrossoverTwoPoint.TabIndex = 0;
            this.rbCrossoverTwoPoint.TabStop = true;
            this.rbCrossoverTwoPoint.Text = "Two-point crossove";
            this.rbCrossoverTwoPoint.UseVisualStyleBackColor = true;
            this.rbCrossoverTwoPoint.CheckedChanged += new System.EventHandler(this.rbCrossoverTwoPoint_CheckedChanged);
            // 
            // rbCrossoverPMX
            // 
            this.rbCrossoverPMX.AutoSize = true;
            this.rbCrossoverPMX.Location = new System.Drawing.Point(18, 35);
            this.rbCrossoverPMX.Name = "rbCrossoverPMX";
            this.rbCrossoverPMX.Size = new System.Drawing.Size(48, 17);
            this.rbCrossoverPMX.TabIndex = 0;
            this.rbCrossoverPMX.TabStop = true;
            this.rbCrossoverPMX.Text = "PMX";
            this.rbCrossoverPMX.UseVisualStyleBackColor = true;
            this.rbCrossoverPMX.CheckedChanged += new System.EventHandler(this.rbCrossoverPMX_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbBalanceProportionMaxSubtour);
            this.groupBox2.Controls.Add(this.rbBalanceProportionDispersion);
            this.groupBox2.Location = new System.Drawing.Point(214, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(181, 130);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Balance Proportion Operator";
            // 
            // rbBalanceProportionDispersion
            // 
            this.rbBalanceProportionDispersion.AutoSize = true;
            this.rbBalanceProportionDispersion.Location = new System.Drawing.Point(22, 35);
            this.rbBalanceProportionDispersion.Name = "rbBalanceProportionDispersion";
            this.rbBalanceProportionDispersion.Size = new System.Drawing.Size(74, 17);
            this.rbBalanceProportionDispersion.TabIndex = 0;
            this.rbBalanceProportionDispersion.Text = "Dispersion";
            this.rbBalanceProportionDispersion.UseVisualStyleBackColor = true;
            this.rbBalanceProportionDispersion.CheckedChanged += new System.EventHandler(this.rbBalanceProportionDispersion_CheckedChanged);
            // 
            // rbBalanceProportionMaxSubtour
            // 
            this.rbBalanceProportionMaxSubtour.AutoSize = true;
            this.rbBalanceProportionMaxSubtour.Checked = true;
            this.rbBalanceProportionMaxSubtour.Location = new System.Drawing.Point(22, 76);
            this.rbBalanceProportionMaxSubtour.Name = "rbBalanceProportionMaxSubtour";
            this.rbBalanceProportionMaxSubtour.Size = new System.Drawing.Size(83, 17);
            this.rbBalanceProportionMaxSubtour.TabIndex = 0;
            this.rbBalanceProportionMaxSubtour.TabStop = true;
            this.rbBalanceProportionMaxSubtour.Text = "Max subtour";
            this.rbBalanceProportionMaxSubtour.UseVisualStyleBackColor = true;
            this.rbBalanceProportionMaxSubtour.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // ChooseOperatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 517);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ChooseOperatorForm";
            this.Text = "ChooseOperatorForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbCrossoverPMX;
        private System.Windows.Forms.RadioButton rbCrossoverTwoPoint;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbBalanceProportionMaxSubtour;
        private System.Windows.Forms.RadioButton rbBalanceProportionDispersion;
    }
}