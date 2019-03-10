using System.Windows.Forms;
using MES.Model;

namespace MES.View
{
    public partial class DataInput : UserControl
    {
        public DataInput()
        {
            InitializeComponent();
        }

        public SimulationData getSimulationData()
        {
            return new SimulationData()
            {
                Convection_Factor = double.Parse(AlfaFactorBox.Text),
                Conductivity = double.Parse(ConductivityBox.Text),
                Initial_Temperature = double.Parse(InittempBox.Text),
                Ambient_Temperature = double.Parse(AmbienttempBox.Text),
                Density = double.Parse(DensityBox.Text),
                Specific_Heat = double.Parse(SpecificheatBox.Text),
                Simulation_Step_Time = double.Parse(SteptimeBox.Text),
                Simulation_Time = double.Parse(SimulationtimeBox.Text),
                XaxisNodesCount = int.Parse(NodesXBox.Text),
                YaxisNodesCount = int.Parse(NodesYBox.Text),
                SampleWidth = double.Parse(WidthtextBox.Text),
                SampleHeight = double.Parse(HeighttextBox.Text)
            };

            //if (checkBox1.Checked)
            //{
            //    double asr = conductivity / (density * specificheat);
            //    double ellength = width / nodesXaxis;
            //    SteptimeBox.Text = Convert.ToInt32(Math.Ceiling(Math.Pow(ellength, 2) / (0.5 * asr))).ToString();
            //}



        }
    }
}
