using System;
using System.Windows.Forms;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;
using System.Threading;
using MES.Utils;
using MES.Model;
using System.Threading.Tasks;

namespace MES
{
    public partial class Form1 : Form
    {
        SimulationData simulationData;
        Mesh mesh;
        double maxT;
        List<Vector<double>> solutions;

        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            button1.Enabled = false;


            var sD = dataInput.getSimulationData();

            mesh = new MeshCreator()
                .createMesh(
                sD.SampleWidth,
                sD.SampleHeight,
                sD.XaxisNodesCount,
                sD.YaxisNodesCount);

            //progressBar1.Visible = true;
            //progressBar1.Minimum = 0;
            //progressBar1.Maximum = (int)Convert.ToUInt32(simulationtime / steptime);
            //progressBar1.Step = 1;


            visualiser.visualiseMesh(mesh);





            //uruchomienie obliczen na tasku 
            solutions?.Clear();

           Task task = Task.Run(() =>
            {
                Solver solver = new Solver();
                MatrixBuilder matrixBuilder = new MatrixBuilder(mesh, sD);
                var h = matrixBuilder.buildHglobalMatrix();
                var p = matrixBuilder.buildPglobalVector();
                var c = matrixBuilder.buildCglobalMatrix();

                solutions = solver.SolveUnstationary(h, c, p, sD);
                
                
                listView1.Invoke(new Action(() => listView1.Items.Clear()));
                for (int i = 0; i < solutions.Count; i++)
                {
                    ListViewItem item = new ListViewItem((0 * (i + 1)).ToString());
                    item.SubItems.Add(Math.Round(solutions[i].Minimum(), 2).ToString());
                    item.SubItems.Add(Math.Round(solutions[i].Maximum(), 2).ToString());
                    item.SubItems.Add((Math.Round(solutions[i].Sum() / solutions[i].Count, 2)).ToString());

                    listView1.Invoke(new Action(() => listView1.Items.Add(item)));
                }
                button1.Invoke(new Action(() => button1.Enabled = true));
            });




           




            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

        }

        //public void initializeImages()
        //{
        //    Bitmap bmp = new Bitmap(1, 1);
        //    using (Graphics g = Graphics.FromImage(bmp))
        //        g.Clear(Color.Transparent);
        //    for (int i = 0; i < series2.Points.Count; i++)
        //    {
        //        chart2.Images.Add(new NamedImage("NI" + i, bmp));
        //        series2.Points[i].MarkerImage = "NI" + i;
        //    }
        //}




        private async void button1_Click_1(object sender, EventArgs e)
        {
            //progressBar1.Value = 0;
            AutoResetEvent aevent = new AutoResetEvent(false);
            // animationTimer = new System.Threading.Timer(AnimateHeatFlow, aevent, 0, Timeout.Infinite);
            SimulationDataCreator creator = new SimulationDataCreator(mesh, solutions);
            var snapShoots = await creator.createSolutionsSnapshootsAsync();
            visualiser.visualiseSimulation(mesh, snapShoots, 400);
        }


        

        public void AnimateHeatFlow(object stateinfo)
        {
            button3.Invoke(new Action(() => button3.Enabled = false));
            button1.Invoke(new Action(() => button1.Enabled = false));

            button3.Invoke(new Action(() => button3.Enabled = true));
            button1.Invoke(new Action(() => button1.Enabled = true));
        }




    }
}
