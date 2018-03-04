using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MathNet.Numerics.LinearAlgebra;
using System.Threading;

namespace MES
{
    public partial class Form1 : Form
    {      
        Integral integral = new Integral();        
        OpenFileDialog dialog = new OpenFileDialog();
        Series series = new Series();
        Series series2;                        
        Mesh mesh;
        public byte Alpha = 0xff;
        public List<Color> ColorsOfMap = new List<Color>();        
        double maxT;
        ObservableCollection<Vector<double>> solvings = new ObservableCollection<Vector<double>>();               
        System.Threading.Timer animationTimer;
        

        public Form1()
        {
            //Collection<NamedImage> k = new Collection<NamedImage>();
            
            InitializeComponent();            
            series.ChartType = SeriesChartType.Point;
            series.Name = "Nodes";           
            series.Color = Color.Red;
            series.MarkerSize = 3;
            solvings.CollectionChanged += (object obj, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) =>
            {
                if (e.NewItems != null)
                    if (progressBar1.InvokeRequired)
                        progressBar1.Invoke(new Action(() => progressBar1.PerformStep()));
              
            };      
            ColorsOfMap.AddRange(new Color[]{            
            Color.FromArgb(Alpha, 0, 0, 0xFF) ,//Blue
            Color.FromArgb(Alpha, 0, 0xFF, 0xFF) ,//Cyan
            Color.FromArgb(Alpha, 0, 0xFF, 0) ,//Green
            Color.GreenYellow,
            Color.FromArgb(Alpha, 0xFF, 0xFF, 0) ,//Yellow
            Color.Orange,
            Color.OrangeRed,          
            Color.FromArgb(Alpha, 0xFF, 0, 0) ,//Red
            Color.Firebrick,
            Color.DarkRed
            //Color.FromArgb(Alpha, 0xFF, 0xFF, 0xFF) // White
        });

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

            try
            {
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
                if(checkBox1.Checked)
                {
                    double asr = conductivity / (density * specificheat);
                    double ellength = width / nodesXaxis;
                    SteptimeBox.Text = Convert.ToInt32(Math.Ceiling(Math.Pow(ellength, 2) / (0.5 * asr))).ToString();
                }
                double.TryParse(SteptimeBox.Text, out steptime);
                mesh = new Mesh(width, height, nodesXaxis, nodesYaxis);
                //naniesienie punktow na wykres
                chart2.Series.Clear();
                series.Points.Clear();
                chart2.ChartAreas[0].AxisX.IsMarginVisible = false;             
                chart2.ChartAreas[0].AxisY.IsMarginVisible = false;
                chart2.ChartAreas[0].AxisX.IsInterlaced = false;
                //ustawienie wartości na progressbar
                progressBar1.Visible = true;
                progressBar1.Minimum = 0;
                progressBar1.Maximum = (int)Convert.ToUInt32(simulationtime / steptime);
                progressBar1.Step = 1;
               
                series.Enabled = true;
                foreach (Node el in mesh.Nodes)
                    series.Points.Add(new DataPoint(el.X, el.Y));
                chart2.Series.Add(series);

                mesh.SetSimulationData(alfa, conductivity, inittemp, ambienttemp, density, specificheat, simulationtime, steptime);
                mesh.GenerateMatrices();

                //uruchomienie obliczen na tasku 
                solvings.Clear();
                System.Threading.Tasks.Task task = System.Threading.Tasks.Task.Run(() =>
                {
                    mesh.SolveUnstationary(solvings);
                    maxT = findMax();
                    listView1.Invoke(new Action(() => listView1.Items.Clear()));
                    for (int i = 0; i < solvings.Count; i++) 
                    {
                        ListViewItem item = new ListViewItem((steptime * (i + 1)).ToString());
                        item.SubItems.Add(Math.Round(solvings[i].Minimum(),2).ToString());
                        item.SubItems.Add(Math.Round(solvings[i].Maximum(),2).ToString());
                        item.SubItems.Add((Math.Round(solvings[i].Sum()/ solvings[i].Count,2)).ToString());

                        listView1.Invoke(new Action(() => listView1.Items.Add(item)));
                    }
                    button1.Invoke(new Action(() => button1.Enabled = true));
                });
                
               

                series2 = CreateInterpolationPoints();              
                series2.MarkerSize = 0;
                chart2.Series.Add(series2);
                //initializeImages();

                
               

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        public void initializeImages()
        {
            Bitmap bmp = new Bitmap(1, 1);
            using (Graphics g = Graphics.FromImage(bmp))
                g.Clear(Color.Transparent);
            for (int i = 0; i < series2.Points.Count; i++)
            {
                chart2.Images.Add(new NamedImage("NI" + i, bmp));
                series2.Points[i].MarkerImage = "NI" + i;
            }
        }
        public Color GetColorForValue(double val, double maxVal)
        {
            
            double valPerc = val / (maxVal+1);// value%
            double colorPerc = 1d / (ColorsOfMap.Count - 1);// % of each block of color. the last is the "100% Color"
            double blockOfColor = valPerc / colorPerc;// the integer part repersents how many block to skip
            int blockIdx = (int)Math.Truncate(blockOfColor);// Idx of 
            double valPercResidual = valPerc - (blockIdx * colorPerc);//remove the part represented of block 
            double percOfColor = valPercResidual / colorPerc;// % of color of this block that will be filled

            if (blockIdx < 0) blockIdx = 0;
            Color cTarget = ColorsOfMap[blockIdx];
            Color cNext = cNext = ColorsOfMap[blockIdx + 1];

            var deltaR = cNext.R - cTarget.R;
            var deltaG = cNext.G - cTarget.G;
            var deltaB = cNext.B - cTarget.B;

            var R = cTarget.R + (deltaR * percOfColor);
            var G = cTarget.G + (deltaG * percOfColor);
            var B = cTarget.B + (deltaB * percOfColor);

            Color c = ColorsOfMap[0];
            try
            {
                 c = Color.FromArgb(Alpha, (byte)R, (byte)G, (byte)B);
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message);
            }
            return c;
        }
        void createMarkers( int count, List<Color> colors,int markerwidth, int markerheight)
        {

            // clean up previous images:
            foreach (NamedImage ni in chart2.Images) ni.Dispose();
            chart2.Images.Clear();

            // now create count images:
            for (int i = 0; i < count; i++)
            {
                Bitmap bmp = new Bitmap(markerwidth,markerheight);
                using (Graphics G = Graphics.FromImage(bmp))               
                G.Clear(colors[i]);
                    
                chart2.Images.Add(new NamedImage("NI" + i, bmp));
                series2.Points[i].MarkerImage = "NI" + i;                                                                         
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            series.Enabled = false;
            //progressBar1.Value = 0;
            AutoResetEvent aevent = new AutoResetEvent(false);
            animationTimer = new System.Threading.Timer(AnimateHeatFlow, aevent, 0, Timeout.Infinite);
           
        }

        public double interpolateTemperature(double T1, double T2, double T3, double T4, int pointID)
        {
            // wartosci funkcji kształtu w punktach interpolacji {-0,5;-0,5} {0,5;-0,5} {0,5;0,5} {-0,5;0,5}
            double factor1 = 0.5625;
            double factor2 = 0.1875;
            double factor3 = 0.0625;
            double factor4 = 0.1875;
            switch(pointID)
            {
                case 1:
                    return factor1 * T1 + factor2 * T2 + factor3 * T3 + factor4 * T4;
                case 2:
                    return factor2 * T1 + factor1 * T2 + factor2 * T3 + factor3 * T4;
                case 3:
                    return factor3 * T1 + factor2 * T2 + factor1 * T3 + factor2 * T4;
                case 4:
                    return factor2 * T1 + factor3 * T2 + factor2 * T3 + factor1 * T4;
                default:
                    return -1;
            }
            
        }
        public double findMax()
        {
            double max = 0.0;
            foreach (Vector<double> v in this.solvings)
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
            List<Color> colors = new List<Color>(count);
            int sw = Convert.ToInt32((chart2.ChartAreas[0].AxisX.ValueToPixelPosition(mesh.Nodes[mesh.Elements[0].IDS[1]].X) - chart2.ChartAreas[0].AxisX.ValueToPixelPosition(mesh.Nodes[mesh.Elements[0].IDS[0]].X)) / 2.0);
           int sh = Convert.ToInt32((chart2.ChartAreas[0].AxisX.ValueToPixelPosition(mesh.Nodes[mesh.Elements[0].IDS[3]].Y) - chart2.ChartAreas[0].AxisX.ValueToPixelPosition(mesh.Nodes[mesh.Elements[0].IDS[0]].Y)) / 2.0);
           
            foreach (Vector<double> vec in solvings)
            {


                colors.Clear();
                foreach (Element el in mesh.Elements)
                {
                    for (int i = 1; i <= 4; i++)
                    {
                        double d = interpolateTemperature(vec[el.IDS[0]], vec[el.IDS[1]], vec[el.IDS[2]], vec[el.IDS[3]], i);
                        colors.Add(GetColorForValue(d, maxT));
                    }
                }

               
               
               
                chart2.Invoke(new Action(() =>
                {
                    Monitor.Enter(lck);
                    createMarkers(series2.Points.Count, colors, sw + 1, sh + 1);                   
                   // progressBar1.PerformStep();
                    Monitor.Exit(lck);
                   
                }
                ));
                while (!Monitor.TryEnter(lck))
                { }
                Monitor.Exit(lck);

                


                Thread.Sleep(400);
            }
            button3.Invoke(new Action(() => button3.Enabled = true));
            button1.Invoke(new Action(() => button1.Enabled = true));
        }

        Series CreateInterpolationPoints()
        {
            Series series = new Series();
            series.ChartType = SeriesChartType.Point;
            foreach (Element el in mesh.Elements)
            {
                double x = (mesh.Nodes[el.IDS[0]].X + mesh.Nodes[el.IDS[2]].X) / 2.0;
                double y = (mesh.Nodes[el.IDS[0]].Y + mesh.Nodes[el.IDS[2]].Y) / 2.0;
                PointF middle = new PointF((float)x, (float)y);


                x = (middle.X + mesh.Nodes[el.IDS[0]].X) / 2.0;
                y = (middle.Y + mesh.Nodes[el.IDS[0]].Y) / 2.0;

                series.Points.Add(new DataPoint(x, y));
                series.Points.Add(new DataPoint(x + mesh.Dx / 2.0, y));
                series.Points.Add(new DataPoint(x + mesh.Dx / 2.0, y + mesh.Dy / 2.0));
                series.Points.Add(new DataPoint(x, y + mesh.Dy / 2.0));
            }
            return series;
        }

        
    }
}
