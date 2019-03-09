using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MathNet.Numerics.LinearAlgebra;
using System.Threading;

namespace MES.View
{
    public partial class MeshVisualiser : UserControl
    {

        public MeshVisualiser()
        {
            InitializeComponent();
            chart1.Legends.Clear();
            chart1.ChartAreas[0].AxisX.IsMarginVisible = false;
            chart1.ChartAreas[0].AxisY.IsMarginVisible = false;
            chart1.ChartAreas[0].AxisX.IsInterlaced = false;

        }

        private Series createSeriesFromMesh(Mesh mesh)
        {
            Series series = new Series
            {
                ChartType = SeriesChartType.Point,
                Name = "Nodes",
                Color = Color.Red,
                MarkerSize = 3,
                Enabled = true
            };

            foreach (Node el in mesh.Nodes)
                series.Points.Add(new DataPoint(el.X, el.Y));

            return series;
        }

        public void visualiseMesh(Mesh mesh)
        {
            this.chart1.Series.Clear();
            var series = createSeriesFromMesh(mesh);
            this.chart1.Series.Add(series);
        }

        public void visualiseSimulation(Mesh mesh, List<Color[]> snapShoots, int interval)
        {
            chart1.Series.Clear();
            Series interpolationSeries = CreateInterpolationPoints(mesh);
            chart1.Series.Add(interpolationSeries);
            var axis = chart1.ChartAreas[0].AxisX;
            int sw = Convert.ToInt32((axis.ValueToPixelPosition(mesh.Nodes[mesh.Elements[0].IDS[1]].X) - axis.ValueToPixelPosition(mesh.Nodes[mesh.Elements[0].IDS[0]].X)) / 2.0);
            int sh = Convert.ToInt32((axis.ValueToPixelPosition(mesh.Nodes[mesh.Elements[0].IDS[3]].Y) - axis.ValueToPixelPosition(mesh.Nodes[mesh.Elements[0].IDS[0]].Y)) / 2.0);

            Task.Run(() =>
            {
                foreach (Color[] snapShoot in snapShoots)
                {
                   chart1.Invoke(new Action(()=> visualiseSnapshoot(interpolationSeries, snapShoot.ToList(), sw + 1, sh + 1)));
                   Thread.Sleep(interval);
                }
            }
            );
        }

        void visualiseSnapshoot(Series series, List<Color> colors, int markerwidth, int markerheight)
        {

            // clean up previous images:
            foreach (NamedImage ni in chart1.Images) ni.Dispose();
            chart1.Images.Clear();

            // now create count images:
            for (int i = 0; i < series.Points.Count; i++)
            {
                Bitmap bmp = new Bitmap(markerwidth, markerheight);
                using (Graphics G = Graphics.FromImage(bmp))
                    G.Clear(colors[i]);

                chart1.Images.Add(new NamedImage("NI" + i, bmp));
                series.Points[i].MarkerImage = "NI" + i;
            }
        }

        private Series CreateInterpolationPoints(Mesh mesh)
        {
            Series series = new Series
            {
                ChartType = SeriesChartType.Point,
                Name = "Nodes",
                Color = Color.Red,
                MarkerSize = 3,
                Enabled = true
            };
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
