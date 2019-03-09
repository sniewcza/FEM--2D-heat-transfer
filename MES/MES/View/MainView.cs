using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MathNet.Numerics.LinearAlgebra;
using System.Threading;
using MES.Utils;
using MES.Model;

namespace MES
{
    public partial class Form1 : Form
    {
        
        // Integral integral = new Integral();        
        //OpenFileDialog dialog = new OpenFileDialog();
        SimulationData simulationData;
        Series series = new Series();
        Series series2;
        Mesh mesh;
        
       
        double maxT;
       // ObservableCollection<Vector<double>> solutions = new ObservableCollection<Vector<double>>();
        System.Threading.Timer animationTimer;
        List<Vector<double>> solutions;

        public Form1()
        {
            //Collection<NamedImage> k = new Collection<NamedImage>();
            
            InitializeComponent();
            series.ChartType = SeriesChartType.Point;
            series.Name = "Nodes";
            series.Color = Color.Red;
            series.MarkerSize = 3;
            //solutions.CollectionChanged += (object obj, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) =>
            //{
            //    if (e.NewItems != null)
            //        if (progressBar1.InvokeRequired)
            //            progressBar1.Invoke(new Action(() => progressBar1.PerformStep()));

            //};
            

        }



        private void button3_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            button1.Enabled = false;
            double width;
            double height;
            int nodesXaxis;
            int nodesYaxis;
            double alfa;
            double conductivity;
            double inittemp;
            double ambienttemp;
            double density;
            double specificheat;
            double simulationtime;
            double steptime;

           // try
           // {
                //czytanie danych od usera
                double.TryParse(WidthtextBox.Text, out width);
                double.TryParse(HeighttextBox.Text, out height);
                int.TryParse(NodesXBox.Text, out nodesXaxis);
                int.TryParse(NodesYBox.Text, out nodesYaxis);
                double.TryParse(AlfaFactorBox.Text, out alfa);
                double.TryParse(ConductivityBox.Text, out conductivity);
                double.TryParse(InittempBox.Text, out inittemp);
                double.TryParse(AmbienttempBox.Text, out ambienttemp);
                double.TryParse(DensityBox.Text, out density);
                double.TryParse(SpecificheatBox.Text, out specificheat);
                double.TryParse(SimulationtimeBox.Text, out simulationtime);
                if (checkBox1.Checked)
                {
                    double asr = conductivity / (density * specificheat);
                    double ellength = width / nodesXaxis;
                    SteptimeBox.Text = Convert.ToInt32(Math.Ceiling(Math.Pow(ellength, 2) / (0.5 * asr))).ToString();
                }
                double.TryParse(SteptimeBox.Text, out steptime);
                mesh = new MeshCreator().createMesh(width, height, nodesXaxis, nodesYaxis);
                //naniesienie punktow na wykres
               // chart2.Series.Clear();
                series.Points.Clear();
                //chart2.ChartAreas[0].AxisX.IsMarginVisible = false;
                //chart2.ChartAreas[0].AxisY.IsMarginVisible = false;
                //chart2.ChartAreas[0].AxisX.IsInterlaced = false;
                //ustawienie wartości na progressbar
                progressBar1.Visible = true;
                progressBar1.Minimum = 0;
                progressBar1.Maximum = (int)Convert.ToUInt32(simulationtime / steptime);
                progressBar1.Step = 1;

            //series.Enabled = true;
            //foreach (Node el in mesh.Nodes)
            //    series.Points.Add(new DataPoint(el.X, el.Y));
            //chart2.Series.Add(series);

            this.meshVisualiser1.visualiseMesh(mesh);
                //mesh.SetSimulationData(alfa, conductivity, inittemp, ambienttemp, density, specificheat, simulationtime, steptime);
                simulationData = new SimulationData()
                {
                    Convection_Factor = alfa,
                    Conductivity = conductivity,
                    Initial_Temperature = inittemp,
                    Ambient_Temperature = ambienttemp,
                    Density = density,
                    Specific_Heat = specificheat,
                    Simulation_Step_Time = steptime,
                    Simulation_Time = simulationtime
                };

               // mesh.GenerateMatrices();


                //uruchomienie obliczen na tasku 
                solutions?.Clear();

                System.Threading.Tasks.Task task = System.Threading.Tasks.Task.Run(() =>
                {
                    Solver solver = new Solver();
                    MatrixBuilder matrixBuilder = new MatrixBuilder(mesh, simulationData);
                    var h = matrixBuilder.buildHglobalMatrix();
                    var p = matrixBuilder.buildPglobalVector();
                    var c = matrixBuilder.buildCglobalMatrix();
                  solutions =  solver.SolveUnstationary(h, c, p, simulationData);
                   // mesh.SolveUnstationary(solutions);
                    maxT = findMax();
                    listView1.Invoke(new Action(() => listView1.Items.Clear()));
                    for (int i = 0; i < solutions.Count; i++)
                    {
                        ListViewItem item = new ListViewItem((steptime * (i + 1)).ToString());
                        item.SubItems.Add(Math.Round(solutions[i].Minimum(), 2).ToString());
                        item.SubItems.Add(Math.Round(solutions[i].Maximum(), 2).ToString());
                        item.SubItems.Add((Math.Round(solutions[i].Sum() / solutions[i].Count, 2)).ToString());

                        listView1.Invoke(new Action(() => listView1.Items.Add(item)));
                    }
                    button1.Invoke(new Action(() => button1.Enabled = true));
                });



                
               // series2.MarkerSize = 0;
                //chart2.Series.Add(series2);
                //initializeImages();




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
            series.Enabled = false;
            //progressBar1.Value = 0;
            AutoResetEvent aevent = new AutoResetEvent(false);
            // animationTimer = new System.Threading.Timer(AnimateHeatFlow, aevent, 0, Timeout.Infinite);
            SimulationDataCreator creator = new SimulationDataCreator(mesh, solutions);
            var snapShoots = await creator.createSolutionsSnapshootsAsync();
            meshVisualiser1.visualiseSimulation(mesh, snapShoots, 400);
        }

       
        public double findMax()
        {
            double max = 0.0;
            foreach (Vector<double> v in this.solutions)
                if (v.Maximum() > max)
                    max = v.Maximum();
            return max;
        }

        public void AnimateHeatFlow(object stateinfo)
        {
            button3.Invoke(new Action(() => button3.Enabled = false));
            button1.Invoke(new Action(() => button1.Enabled = false));

            object lck = new object();
            int count = mesh.Nodes.Count;
           
           

           
                //chart2.Invoke(new Action(() =>
                //{
                //    Monitor.Enter(lck);
                //    createMarkers(series2.Points.Count, colors, sw + 1, sh + 1);
                //    // progressBar1.PerformStep();
                //    Monitor.Exit(lck);

                //}
                //));
                //while (!Monitor.TryEnter(lck))
                //{ }
                //Monitor.Exit(lck);




                //Thread.Sleep(400);
            
            button3.Invoke(new Action(() => button3.Enabled = true));
            button1.Invoke(new Action(() => button1.Enabled = true));
        }

        


    }
}
