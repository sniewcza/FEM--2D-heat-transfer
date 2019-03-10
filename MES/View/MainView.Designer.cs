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
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.showSimulationButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.initializeButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.temperatureListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cancelSolverButton = new System.Windows.Forms.Button();
            this.dataInput = new MES.View.DataInput();
            this.visualiser = new MES.View.Visualiser();
            this.tabPage3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.Gray;
            this.tabPage3.Controls.Add(this.cancelSolverButton);
            this.tabPage3.Controls.Add(this.dataInput);
            this.tabPage3.Controls.Add(this.visualiser);
            this.tabPage3.Controls.Add(this.showSimulationButton);
            this.tabPage3.Controls.Add(this.progressBar);
            this.tabPage3.Controls.Add(this.initializeButton);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(753, 468);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Heat flow";
            // 
            // showSimulationButton
            // 
            this.showSimulationButton.Enabled = false;
            this.showSimulationButton.Location = new System.Drawing.Point(640, 418);
            this.showSimulationButton.Name = "showSimulationButton";
            this.showSimulationButton.Size = new System.Drawing.Size(100, 23);
            this.showSimulationButton.TabIndex = 25;
            this.showSimulationButton.Text = "Show Simulation";
            this.showSimulationButton.UseVisualStyleBackColor = true;
            this.showSimulationButton.Click += new System.EventHandler(this.showSimulationButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(32, 385);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(383, 18);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 4;
            this.progressBar.Visible = false;
            // 
            // initializeButton
            // 
            this.initializeButton.Location = new System.Drawing.Point(441, 418);
            this.initializeButton.Name = "initializeButton";
            this.initializeButton.Size = new System.Drawing.Size(75, 23);
            this.initializeButton.TabIndex = 24;
            this.initializeButton.Text = "Initialize";
            this.initializeButton.UseVisualStyleBackColor = true;
            this.initializeButton.Click += new System.EventHandler(this.initializeButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(761, 494);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Gray;
            this.tabPage1.Controls.Add(this.temperatureListView);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(753, 468);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "Temperatures";
            // 
            // temperatureListView
            // 
            this.temperatureListView.BackColor = System.Drawing.SystemColors.Window;
            this.temperatureListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.temperatureListView.FullRowSelect = true;
            this.temperatureListView.GridLines = true;
            this.temperatureListView.Location = new System.Drawing.Point(7, 7);
            this.temperatureListView.Name = "temperatureListView";
            this.temperatureListView.Size = new System.Drawing.Size(733, 383);
            this.temperatureListView.TabIndex = 0;
            this.temperatureListView.UseCompatibleStateImageBehavior = false;
            this.temperatureListView.View = System.Windows.Forms.View.Details;
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
            // cancelSolverButton
            // 
            this.cancelSolverButton.Location = new System.Drawing.Point(185, 418);
            this.cancelSolverButton.Name = "cancelSolverButton";
            this.cancelSolverButton.Size = new System.Drawing.Size(75, 23);
            this.cancelSolverButton.TabIndex = 27;
            this.cancelSolverButton.Text = "Cancel";
            this.cancelSolverButton.UseVisualStyleBackColor = true;
            this.cancelSolverButton.Visible = false;
            this.cancelSolverButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataInput
            // 
            this.dataInput.Location = new System.Drawing.Point(441, 15);
            this.dataInput.Name = "dataInput";
            this.dataInput.Size = new System.Drawing.Size(299, 364);
            this.dataInput.TabIndex = 26;
            // 
            // visualiser
            // 
            this.visualiser.Location = new System.Drawing.Point(6, 15);
            this.visualiser.Name = "visualiser";
            this.visualiser.Size = new System.Drawing.Size(429, 364);
            this.visualiser.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GrayText;
            this.ClientSize = new System.Drawing.Size(761, 494);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "FEM 2D Heat Transfer";
            this.tabPage3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button initializeButton;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button showSimulationButton;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListView temperatureListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ProgressBar progressBar;
        private View.Visualiser visualiser;
        private View.DataInput dataInput;
        private System.Windows.Forms.Button cancelSolverButton;
    }
}

