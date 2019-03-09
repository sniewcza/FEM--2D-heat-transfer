using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Model
{
    class SimulationData
    {
        public double Ambient_Temperature { get; set; }
        public double Initial_Temperature { get; set; }
        public double Simulation_Time { get; set; }
        public double Simulation_Step_Time { get; set; }
        public double Convection_Factor { get; set; }
        public double Specific_Heat { get; set; }
        public double Conductivity { get; set; }
        public double Density { get; set; }
    }
}
