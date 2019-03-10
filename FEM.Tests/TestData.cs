using MES.Model;
using MES.Utils;

namespace FEM.Tests
{
    class TestData
    {
        public Mesh Mesh { get; private set; }
        public SimulationData SimulationData { get; private set; } = new SimulationData()
        {
            Initial_Temperature = 100,
            Simulation_Time = 500,
            Simulation_Step_Time = 50,
            Ambient_Temperature = 1200,
            Convection_Factor = 300,
            SampleWidth = 0.1,
            SampleHeight = 0.1,
            XaxisNodesCount = 4,
            YaxisNodesCount = 4,
            Specific_Heat = 700,
            Conductivity = 25,
            Density = 7800
        };

        private MeshCreator _meshCreator = new MeshCreator();

        public TestData()
        {
            Mesh = _meshCreator.createMesh(
                SimulationData.SampleWidth,
                SimulationData.SampleHeight,
                SimulationData.XaxisNodesCount,
                SimulationData.YaxisNodesCount);
        }
    }
}
