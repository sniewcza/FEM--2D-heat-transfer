namespace MES
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SteptimeBox = new System.Windows.Forms.TextBox();
            this.SimulationtimeBox = new System.Windows.Forms.TextBox();
            this.SpecificheatBox = new System.Windows.Forms.TextBox();
            this.DensityBox = new System.Windows.Forms.TextBox();
            this.AmbienttempBox = new System.Windows.Forms.TextBox();
            this.InittempBox = new System.Windows.Forms.TextBox();
            this.ConductivityBox = new System.Windows.Forms.TextBox();
            this.AlfaFactorBox = new System.Windows.Forms.TextBox();
            this.NodesYBox = new System.Windows.Forms.TextBox();
            this.NodesXBox = new System.Windows.Forms.TextBox();
            this.HeighttextBox = new System.Windows.Forms.TextBox();
            this.WidthtextBox = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.Gray;
            this.tabPage3.Controls.Add(this.progressBar1);
            this.tabPage3.Controls.Add(this.chart2);
            this.tabPage3.Controls.Add(this.groupBox3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(746, 396);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Heat flow";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(72, 377);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(290, 10);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 4;
            this.progressBar1.Visible = false;
            // 
            // chart2
            // 
            this.chart2.BackColor = System.Drawing.Color.Gray;
            this.chart2.BackImageWrapMode = System.Windows.Forms.DataVisualization.Charting.ChartImageWrapMode.TileFlipXY;
            chartArea1.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea1);
            this.chart2.Location = new System.Drawing.Point(6, 6);
            this.chart2.Name = "chart2";
            this.chart2.Size = new System.Drawing.Size(429, 365);
            this.chart2.TabIndex = 3;
            this.chart2.Text = "chart2";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.SteptimeBox);
            this.groupBox3.Controls.Add(this.SimulationtimeBox);
            this.groupBox3.Controls.Add(this.SpecificheatBox);
            this.groupBox3.Controls.Add(this.DensityBox);
            this.groupBox3.Controls.Add(this.AmbienttempBox);
            this.groupBox3.Controls.Add(this.InittempBox);
            this.groupBox3.Controls.Add(this.ConductivityBox);
            this.groupBox3.Controls.Add(this.AlfaFactorBox);
            this.groupBox3.Controls.Add(this.NodesYBox);
            this.groupBox3.Controls.Add(this.NodesXBox);
            this.groupBox3.Controls.Add(this.HeighttextBox);
            this.groupBox3.Controls.Add(this.WidthtextBox);
            this.groupBox3.Location = new System.Drawing.Point(441, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(299, 384);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Simulation Data";
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(190, 343);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 25;
            this.button1.Text = "Show Simulation";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(106, 343);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 24;
            this.button3.Text = "Initialize";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(69, 310);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(114, 13);
            this.label13.TabIndex = 23;
            this.label13.Text = "Simulation step time [s]";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(81, 288);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(91, 13);
            this.label12.TabIndex = 22;
            this.label12.Text = "Simulation time [s]";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(71, 262);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(111, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "Specific heat [J/kg*K]";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(79, 236);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(94, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = " Density [ kg/m^3]\r\n";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(66, 206);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(120, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Ambient temperature [C]";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(73, 180);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(106, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Initial temperature [C]";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(72, 154);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(109, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Conductivity [W/m*K]";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(65, 125);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(123, 26);
            this.label6.TabIndex = 16;
            this.label6.Text = "Heat transfer coefficient \r\n           [W/(m^2 *K]\r\n";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(92, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Nodes Y axis";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(92, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Nodes X axis";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(102, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Width [m]";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(99, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Height [m]";
            // 
            // SteptimeBox
            // 
            this.SteptimeBox.Location = new System.Drawing.Point(190, 307);
            this.SteptimeBox.Name = "SteptimeBox";
            this.SteptimeBox.Size = new System.Drawing.Size(100, 20);
            this.SteptimeBox.TabIndex = 11;
            // 
            // SimulationtimeBox
            // 
            this.SimulationtimeBox.Location = new System.Drawing.Point(190, 281);
            this.SimulationtimeBox.Name = "SimulationtimeBox";
            this.SimulationtimeBox.Size = new System.Drawing.Size(100, 20);
            this.SimulationtimeBox.TabIndex = 10;
            // 
            // SpecificheatBox
            // 
            this.SpecificheatBox.Location = new System.Drawing.Point(190, 255);
            this.SpecificheatBox.Name = "SpecificheatBox";
            this.SpecificheatBox.Size = new System.Drawing.Size(100, 20);
            this.SpecificheatBox.TabIndex = 9;
            // 
            // DensityBox
            // 
            this.DensityBox.Location = new System.Drawing.Point(190, 229);
            this.DensityBox.Name = "DensityBox";
            this.DensityBox.Size = new System.Drawing.Size(100, 20);
            this.DensityBox.TabIndex = 8;
            // 
            // AmbienttempBox
            // 
            this.AmbienttempBox.Location = new System.Drawing.Point(190, 203);
            this.AmbienttempBox.Name = "AmbienttempBox";
            this.AmbienttempBox.Size = new System.Drawing.Size(100, 20);
            this.AmbienttempBox.TabIndex = 7;
            // 
            // InittempBox
            // 
            this.InittempBox.Location = new System.Drawing.Point(190, 177);
            this.InittempBox.Name = "InittempBox";
            this.InittempBox.Size = new System.Drawing.Size(100, 20);
            this.InittempBox.TabIndex = 6;
            // 
            // ConductivityBox
            // 
            this.ConductivityBox.Location = new System.Drawing.Point(190, 151);
            this.ConductivityBox.Name = "ConductivityBox";
            this.ConductivityBox.Size = new System.Drawing.Size(100, 20);
            this.ConductivityBox.TabIndex = 5;
            // 
            // AlfaFactorBox
            // 
            this.AlfaFactorBox.Location = new System.Drawing.Point(190, 125);
            this.AlfaFactorBox.Name = "AlfaFactorBox";
            this.AlfaFactorBox.Size = new System.Drawing.Size(100, 20);
            this.AlfaFactorBox.TabIndex = 4;
            // 
            // NodesYBox
            // 
            this.NodesYBox.Location = new System.Drawing.Point(190, 99);
            this.NodesYBox.Name = "NodesYBox";
            this.NodesYBox.Size = new System.Drawing.Size(100, 20);
            this.NodesYBox.TabIndex = 3;
            // 
            // NodesXBox
            // 
            this.NodesXBox.Location = new System.Drawing.Point(190, 73);
            this.NodesXBox.Name = "NodesXBox";
            this.NodesXBox.Size = new System.Drawing.Size(100, 20);
            this.NodesXBox.TabIndex = 2;
            // 
            // HeighttextBox
            // 
            this.HeighttextBox.Location = new System.Drawing.Point(190, 47);
            this.HeighttextBox.Name = "HeighttextBox";
            this.HeighttextBox.Size = new System.Drawing.Size(100, 20);
            this.HeighttextBox.TabIndex = 1;
            // 
            // WidthtextBox
            // 
            this.WidthtextBox.Location = new System.Drawing.Point(190, 21);
            this.WidthtextBox.Name = "WidthtextBox";
            this.WidthtextBox.Size = new System.Drawing.Size(100, 20);
            this.WidthtextBox.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(754, 422);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Gray;
            this.tabPage1.Controls.Add(this.listView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(746, 396);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "Temperatures";
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.SystemColors.Window;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(7, 7);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(733, 383);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Time elapsed [s]";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Min Temp [C]";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 120;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Max Temp [C]";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 105;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Avg Temp [C]";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 116;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 347);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(87, 17);
            this.checkBox1.TabIndex = 26;
            this.checkBox1.Text = "Optimal step ";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GrayText;
            this.ClientSize = new System.Drawing.Size(778, 446);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "FEM 2D Heat Transfer";
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox SteptimeBox;
        private System.Windows.Forms.TextBox SimulationtimeBox;
        private System.Windows.Forms.TextBox SpecificheatBox;
        private System.Windows.Forms.TextBox DensityBox;
        private System.Windows.Forms.TextBox AmbienttempBox;
        private System.Windows.Forms.TextBox InittempBox;
        private System.Windows.Forms.TextBox ConductivityBox;
        private System.Windows.Forms.TextBox AlfaFactorBox;
        private System.Windows.Forms.TextBox NodesYBox;
        private System.Windows.Forms.TextBox NodesXBox;
        private System.Windows.Forms.TextBox HeighttextBox;
        private System.Windows.Forms.TextBox WidthtextBox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

