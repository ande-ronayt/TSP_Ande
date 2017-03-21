namespace WinFormApp
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.lblCurGen = new System.Windows.Forms.Label();
            this.txtAlpha = new System.Windows.Forms.TextBox();
            this.txtBeta = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.scAlpha = new System.Windows.Forms.HScrollBar();
            this.txtAlphaMax = new System.Windows.Forms.TextBox();
            this.txtBetaMax = new System.Windows.Forms.TextBox();
            this.scBeta = new System.Windows.Forms.HScrollBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(29, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(129, 21);
            this.button1.TabIndex = 0;
            this.button1.Text = "ChangeTspLib95 Path";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(29, 38);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 21);
            this.button2.TabIndex = 1;
            this.button2.Text = "GetOne";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(161, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Some results";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(29, 69);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(111, 21);
            this.button3.TabIndex = 3;
            this.button3.Text = "Load TspLibItem";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(163, 135);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(303, 338);
            this.textBox1.TabIndex = 4;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(491, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(500, 462);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(357, 42);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(110, 21);
            this.button4.TabIndex = 6;
            this.button4.Text = "Clear";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(357, 15);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(110, 21);
            this.button5.TabIndex = 7;
            this.button5.Text = "Create a problem";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(355, 69);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(111, 21);
            this.button6.TabIndex = 8;
            this.button6.Text = "Run ";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // lblCurGen
            // 
            this.lblCurGen.AutoSize = true;
            this.lblCurGen.Location = new System.Drawing.Point(353, 93);
            this.lblCurGen.Name = "lblCurGen";
            this.lblCurGen.Size = new System.Drawing.Size(113, 12);
            this.lblCurGen.TabIndex = 2;
            this.lblCurGen.Text = "Current Generation";
            // 
            // txtAlpha
            // 
            this.txtAlpha.Location = new System.Drawing.Point(548, 499);
            this.txtAlpha.Name = "txtAlpha";
            this.txtAlpha.Size = new System.Drawing.Size(40, 21);
            this.txtAlpha.TabIndex = 10;
            this.txtAlpha.Text = "1";
            this.txtAlpha.TextChanged += new System.EventHandler(this.txtAlpha_TextChanged);
            // 
            // txtBeta
            // 
            this.txtBeta.Location = new System.Drawing.Point(548, 539);
            this.txtBeta.Name = "txtBeta";
            this.txtBeta.Size = new System.Drawing.Size(40, 21);
            this.txtBeta.TabIndex = 11;
            this.txtBeta.Text = "1";
            this.txtBeta.TextChanged += new System.EventHandler(this.txtBeta_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(489, 502);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Alpha";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(489, 542);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "Beta";
            // 
            // scAlpha
            // 
            this.scAlpha.Location = new System.Drawing.Point(605, 502);
            this.scAlpha.Name = "scAlpha";
            this.scAlpha.Size = new System.Drawing.Size(330, 18);
            this.scAlpha.TabIndex = 12;
            this.scAlpha.Value = 1;
            this.scAlpha.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scAlpha_Scroll);
            // 
            // txtAlphaMax
            // 
            this.txtAlphaMax.Location = new System.Drawing.Point(951, 502);
            this.txtAlphaMax.Name = "txtAlphaMax";
            this.txtAlphaMax.Size = new System.Drawing.Size(40, 21);
            this.txtAlphaMax.TabIndex = 10;
            this.txtAlphaMax.Text = "50";
            this.txtAlphaMax.TextChanged += new System.EventHandler(this.txtAlphaMax_TextChanged);
            // 
            // txtBetaMax
            // 
            this.txtBetaMax.Location = new System.Drawing.Point(951, 542);
            this.txtBetaMax.Name = "txtBetaMax";
            this.txtBetaMax.Size = new System.Drawing.Size(40, 21);
            this.txtBetaMax.TabIndex = 10;
            this.txtBetaMax.Text = "50";
            this.txtBetaMax.TextChanged += new System.EventHandler(this.txtBetaMax_TextChanged);
            // 
            // scBeta
            // 
            this.scBeta.Location = new System.Drawing.Point(605, 542);
            this.scBeta.Name = "scBeta";
            this.scBeta.Size = new System.Drawing.Size(330, 18);
            this.scBeta.TabIndex = 12;
            this.scBeta.Value = 1;
            this.scBeta.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scBeta_Scroll);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 619);
            this.Controls.Add(this.scBeta);
            this.Controls.Add(this.scAlpha);
            this.Controls.Add(this.txtBetaMax);
            this.Controls.Add(this.txtBeta);
            this.Controls.Add(this.txtAlphaMax);
            this.Controls.Add(this.txtAlpha);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.lblCurGen);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "O";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label lblCurGen;
        private System.Windows.Forms.TextBox txtAlpha;
        private System.Windows.Forms.TextBox txtBeta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.HScrollBar scAlpha;
        private System.Windows.Forms.TextBox txtAlphaMax;
        private System.Windows.Forms.TextBox txtBetaMax;
        private System.Windows.Forms.HScrollBar scBeta;
    }
}

