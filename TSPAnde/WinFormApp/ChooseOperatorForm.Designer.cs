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
            this.rbCrossoverTwoPoint = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbCrossoverTwoPoint);
            this.groupBox1.Controls.Add(this.rbCrossoverPMX);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(158, 120);
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
            this.rbCrossoverPMX.TabStop = true;
            this.rbCrossoverPMX.Text = "PMX";
            this.rbCrossoverPMX.UseVisualStyleBackColor = true;
            this.rbCrossoverPMX.CheckedChanged += new System.EventHandler(this.rbCrossoverPMX_CheckedChanged);
            // 
            // rbCrossoverTwoPoint
            // 
            this.rbCrossoverTwoPoint.AutoSize = true;
            this.rbCrossoverTwoPoint.Checked = true;
            this.rbCrossoverTwoPoint.Location = new System.Drawing.Point(18, 70);
            this.rbCrossoverTwoPoint.Name = "rbCrossoverTwoPoint";
            this.rbCrossoverTwoPoint.Size = new System.Drawing.Size(131, 16);
            this.rbCrossoverTwoPoint.TabIndex = 0;
            this.rbCrossoverTwoPoint.TabStop = true;
            this.rbCrossoverTwoPoint.Text = "Two-point crossove";
            this.rbCrossoverTwoPoint.UseVisualStyleBackColor = true;
            this.rbCrossoverTwoPoint.CheckedChanged += new System.EventHandler(this.rbCrossoverPMX_CheckedChanged);
            // 
            // ChooseOperatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 477);
            this.Controls.Add(this.groupBox1);
            this.Name = "ChooseOperatorForm";
            this.Text = "ChooseOperatorForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbCrossoverPMX;
        private System.Windows.Forms.RadioButton rbCrossoverTwoPoint;
    }
}