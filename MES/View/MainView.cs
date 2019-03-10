using System;
using System.Windows.Forms;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;
using System.Threading;
using MES.Utils;
using MES.Model;

namespace MES
{
    public partial class Form1 : Form
    {
        Mesh mesh;
        List<Vector<double>> solutions;
        CancellationTokenSource token;
        public Form1()
        {
            InitializeComponent();
            progressBar.Minimum = 0;
            progressBar.Step = 1;
            visualiser.onSnapshootChange += Visualiser_onSnapshootChange;
        }

        private void Visualiser_onSnapshootChange(object sender, EventArgs e)
        {
            Invoke((Action)(() =>
            {
                progressBar.PerformStep();
            }));
        }

        private async void initializeButton_Click(object sender, EventArgs e)
        {
            try
            {
                temperatureListView.Items.Clear();
                var sD = dataInput.getSimulationData();

                mesh = new MeshCreator()
                    .createMesh(
                    sD.SampleWidth,
                    sD.SampleHeight,
                    sD.XaxisNodesCount,
                    sD.YaxisNodesCount);

                progressBar.Value = 0;
                progressBar.Visible = true;

                progressBar.Maximum = Convert.ToInt32(sD.Simulation_Time / sD.Simulation_Step_Time);

                visualiser.visualiseMesh(mesh);

                solutions?.Clear();

                showSimulationButton.Enabled = false;
                initializeButton.Enabled = false;
                cancelSolverButton.Visible = true;
                //await Task.Run(() =>
                //  {
                Solver solver = new Solver();
                solver.onStepTimeSolve += Solver_onStepTimeSolve;
                MatrixBuilder matrixBuilder = new MatrixBuilder(mesh, sD);
                var h = matrixBuilder.buildHglobalMatrix();
                var p = matrixBuilder.buildPglobalVector();
                var c = matrixBuilder.buildCglobalMatrix();

                token = new CancellationTokenSource();
                solutions = await solver.SolveUnstationaryAsync(h, c, p, sD, token.Token);

                //},token.Token);
                showSimulationButton.Enabled = true;
                initializeButton.Enabled = true;
                Cursor.Current = Cursors.Arrow;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Solver_onStepTimeSolve(object sender, CustomEventArgs e)
        {
            Invoke((Action)(() =>
            {
                var solution = e.Solution;
                ListViewItem item = new ListViewItem(e.TimeElapsed.ToString());
                item.SubItems.Add(Math.Round(solution.Minimum(), 2).ToString());
                item.SubItems.Add(Math.Round(solution.Maximum(), 2).ToString());
                item.SubItems.Add((Math.Round(solution.Sum() / solution.Count, 2)).ToString());
                temperatureListView.Items.Add(item);
                progressBar.PerformStep();
            }));
        }

        private async void showSimulationButton_Click(object sender, EventArgs e)
        {
            progressBar.Value = 0;
            SimulationDataCreator creator = new SimulationDataCreator(mesh, solutions);
            var snapShoots = await creator.createSolutionsSnapshootsAsync();

            visualiser.visualiseSimulation(mesh, snapShoots, 400);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            token.Cancel();
            Cursor.Current = Cursors.WaitCursor;
        }
    }
}
