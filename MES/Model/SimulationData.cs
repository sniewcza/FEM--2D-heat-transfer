namespace MES.Model
{
    public class SimulationData
    {
        public double Ambient_Temperature { get; set; }
        public double Initial_Temperature { get; set; }
        public double Simulation_Time { get; set; }
        public double Simulation_Step_Time { get; set; }
        public double Convection_Factor { get; set; }
        public double Specific_Heat { get; set; }
        public double Conductivity { get; set; }
        public double Density { get; set; }
        public int XaxisNodesCount { get; set; }
        public int YaxisNodesCount { get; set; }
        public double SampleWidth { get; set; }
        public double SampleHeight { get; set; }
    }
}
