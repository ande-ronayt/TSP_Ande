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
            this.btnCreateProblem = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.lblCurGen = new System.Windows.Forms.Label();
            this.txtAlpha = new System.Windows.Forms.TextBox();
            this.txtBeta = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.scAlpha = new System.Windows.Forms.HScrollBar();
            this.txtAlphaMax = new System.Windows.Forms.TextBox();
            this.txtBetaMax = new System.Windows.Forms.TextBox();
            this.scBeta = new System.Windows.Forms.HScrollBar();
            this.btnChooseOperator = new System.Windows.Forms.Button();
            this.txtTravelersAmount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbDepoId = new System.Windows.Forms.ComboBox();
            this.lblDepoId = new System.Windows.Forms.Label();
            this.lblCityAmount = new System.Windows.Forms.Label();
            this.txtTspLibChooseOne = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(29, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(129, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "ChangeTspLib95 Path";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(29, 41);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "GetOne";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Some results";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(29, 75);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(111, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "Load TspLibItem";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(29, 146);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(303, 366);
            this.textBox1.TabIndex = 4;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(491, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(500, 500);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // btnCreateProblem
            // 
            this.btnCreateProblem.Location = new System.Drawing.Point(356, 67);
            this.btnCreateProblem.Name = "btnCreateProblem";
            this.btnCreateProblem.Size = new System.Drawing.Size(110, 23);
            this.btnCreateProblem.TabIndex = 7;
            this.btnCreateProblem.Text = "Create a problem";
            this.btnCreateProblem.UseVisualStyleBackColor = true;
            this.btnCreateProblem.Click += new System.EventHandler(this.button5_Click);
            // 
            // btnRun
            // 
            this.btnRun.Enabled = false;
            this.btnRun.Location = new System.Drawing.Point(355, 96);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(111, 23);
            this.btnRun.TabIndex = 8;
            this.btnRun.Text = "Run ";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.button6_Click);
            // 
            // lblCurGen
            // 
            this.lblCurGen.AutoSize = true;
            this.lblCurGen.Location = new System.Drawing.Point(353, 122);
            this.lblCurGen.Name = "lblCurGen";
            this.lblCurGen.Size = new System.Drawing.Size(96, 13);
            this.lblCurGen.TabIndex = 2;
            this.lblCurGen.Text = "Current Generation";
            // 
            // txtAlpha
            // 
            this.txtAlpha.Location = new System.Drawing.Point(548, 541);
            this.txtAlpha.Name = "txtAlpha";
            this.txtAlpha.Size = new System.Drawing.Size(40, 20);
            this.txtAlpha.TabIndex = 10;
            this.txtAlpha.Text = "1";
            this.txtAlpha.TextChanged += new System.EventHandler(this.txtAlpha_TextChanged);
            // 
            // txtBeta
            // 
            this.txtBeta.Location = new System.Drawing.Point(548, 584);
            this.txtBeta.Name = "txtBeta";
            this.txtBeta.Size = new System.Drawing.Size(40, 20);
            this.txtBeta.TabIndex = 11;
            this.txtBeta.Text = "1";
            this.txtBeta.TextChanged += new System.EventHandler(this.txtBeta_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(489, 544);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Alpha";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(489, 587);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Beta";
            // 
            // scAlpha
            // 
            this.scAlpha.Location = new System.Drawing.Point(605, 544);
            this.scAlpha.Name = "scAlpha";
            this.scAlpha.Size = new System.Drawing.Size(330, 18);
            this.scAlpha.TabIndex = 12;
            this.scAlpha.Value = 1;
            this.scAlpha.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scAlpha_Scroll);
            // 
            // txtAlphaMax
            // 
            this.txtAlphaMax.Location = new System.Drawing.Point(951, 544);
            this.txtAlphaMax.Name = "txtAlphaMax";
            this.txtAlphaMax.Size = new System.Drawing.Size(40, 20);
            this.txtAlphaMax.TabIndex = 10;
            this.txtAlphaMax.Text = "50";
            this.txtAlphaMax.TextChanged += new System.EventHandler(this.txtAlphaMax_TextChanged);
            // 
            // txtBetaMax
            // 
            this.txtBetaMax.Location = new System.Drawing.Point(951, 587);
            this.txtBetaMax.Name = "txtBetaMax";
            this.txtBetaMax.Size = new System.Drawing.Size(40, 20);
            this.txtBetaMax.TabIndex = 10;
            this.txtBetaMax.Text = "50";
            this.txtBetaMax.TextChanged += new System.EventHandler(this.txtBetaMax_TextChanged);
            // 
            // scBeta
            // 
            this.scBeta.Location = new System.Drawing.Point(605, 587);
            this.scBeta.Name = "scBeta";
            this.scBeta.Size = new System.Drawing.Size(330, 18);
            this.scBeta.TabIndex = 12;
            this.scBeta.Value = 1;
            this.scBeta.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scBeta_Scroll);
            // 
            // btnChooseOperator
            // 
            this.btnChooseOperator.Enabled = false;
            this.btnChooseOperator.Location = new System.Drawing.Point(192, 39);
            this.btnChooseOperator.Name = "btnChooseOperator";
            this.btnChooseOperator.Size = new System.Drawing.Size(128, 23);
            this.btnChooseOperator.TabIndex = 13;
            this.btnChooseOperator.Text = "Change Operators";
            this.btnChooseOperator.UseVisualStyleBackColor = true;
            this.btnChooseOperator.Click += new System.EventHandler(this.btnChooseOperator_Click);
            // 
            // txtTravelersAmount
            // 
            this.txtTravelersAmount.Location = new System.Drawing.Point(356, 41);
            this.txtTravelersAmount.Name = "txtTravelersAmount";
            this.txtTravelersAmount.Size = new System.Drawing.Size(100, 20);
            this.txtTravelersAmount.TabIndex = 14;
            this.txtTravelersAmount.Text = "2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(353, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Travelers";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbDepoId);
            this.groupBox1.Controls.Add(this.lblDepoId);
            this.groupBox1.Controls.Add(this.lblCityAmount);
            this.groupBox1.Location = new System.Drawing.Point(355, 175);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(111, 216);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "City Section";
            // 
            // cmbDepoId
            // 
            this.cmbDepoId.FormattingEnabled = true;
            this.cmbDepoId.Location = new System.Drawing.Point(9, 81);
            this.cmbDepoId.Name = "cmbDepoId";
            this.cmbDepoId.Size = new System.Drawing.Size(92, 21);
            this.cmbDepoId.TabIndex = 1;
            this.cmbDepoId.Text = "1";
            this.cmbDepoId.SelectedIndexChanged += new System.EventHandler(this.cmbDepoId_SelectedIndexChanged);
            // 
            // lblDepoId
            // 
            this.lblDepoId.AutoSize = true;
            this.lblDepoId.Location = new System.Drawing.Point(6, 56);
            this.lblDepoId.Name = "lblDepoId";
            this.lblDepoId.Size = new System.Drawing.Size(39, 13);
            this.lblDepoId.TabIndex = 0;
            this.lblDepoId.Text = "Depo: ";
            // 
            // lblCityAmount
            // 
            this.lblCityAmount.AutoSize = true;
            this.lblCityAmount.Location = new System.Drawing.Point(6, 27);
            this.lblCityAmount.Name = "lblCityAmount";
            this.lblCityAmount.Size = new System.Drawing.Size(49, 13);
            this.lblCityAmount.TabIndex = 0;
            this.lblCityAmount.Text = "Amount: ";
            // 
            // txtTspLibChooseOne
            // 
            this.txtTspLibChooseOne.Location = new System.Drawing.Point(110, 44);
            this.txtTspLibChooseOne.Name = "txtTspLibChooseOne";
            this.txtTspLibChooseOne.Size = new System.Drawing.Size(48, 20);
            this.txtTspLibChooseOne.TabIndex = 16;
            this.txtTspLibChooseOne.Text = "11";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 671);
            this.Controls.Add(this.txtTspLibChooseOne);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtTravelersAmount);
            this.Controls.Add(this.btnChooseOperator);
            this.Controls.Add(this.scBeta);
            this.Controls.Add(this.scAlpha);
            this.Controls.Add(this.txtBetaMax);
            this.Controls.Add(this.txtBeta);
            this.Controls.Add(this.txtAlphaMax);
            this.Controls.Add(this.txtAlpha);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.btnCreateProblem);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.lblCurGen);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "O";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.Button btnCreateProblem;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Label lblCurGen;
        private System.Windows.Forms.TextBox txtAlpha;
        private System.Windows.Forms.TextBox txtBeta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.HScrollBar scAlpha;
        private System.Windows.Forms.TextBox txtAlphaMax;
        private System.Windows.Forms.TextBox txtBetaMax;
        private System.Windows.Forms.HScrollBar scBeta;
        private System.Windows.Forms.Button btnChooseOperator;
        private System.Windows.Forms.TextBox txtTravelersAmount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbDepoId;
        private System.Windows.Forms.Label lblDepoId;
        private System.Windows.Forms.Label lblCityAmount;
        private System.Windows.Forms.TextBox txtTspLibChooseOne;
    }
}

