using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MES.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Utils
{
    class Solver
    {
        public event EventHandler<Vector<double>> onStepTimeSolve;

        public List<Vector<double>> SolveUnstationary(Matrix<double> H, Matrix<double> C, Vector<double> P, SimulationData simulationData)
        {
            double currentTime = 0;

            //List<Vector<double>> solves = new List<Vector<double>>();
            // Matrix<double> H = DenseMatrix.OfArray(Hglobal);
            // Matrix<double> C = DenseMatrix.OfArray(Cglobal).Divide(Simulation_Step_Time);
            // Vector<double> P = DenseVector.OfArray(Pglobal);
            //foreach (Node n in _nodes)
            //    n.T = Initial_Temperature;

            double[] t0 = new double[P.Count];

            for (int i = 0; i < t0.Length; i++)
                t0[i] = simulationData.Initial_Temperature;

            Vector<double> T0 = DenseVector.OfArray(t0);
            Matrix<double> newC = C.Divide(simulationData.Simulation_Step_Time);
            Matrix<double> newH = H.Add(newC);
            Vector<double> newP = P.Add(newC.Multiply(T0));

            List<Vector<double>> solutions = new List<Vector<double>>();

            for (currentTime = 0; currentTime < simulationData.Simulation_Time; currentTime += simulationData.Simulation_Step_Time)
            // while ( currentTime < Simulation_Time)
            {

                Vector<double> T1 = newH.Solve(newP);
                solutions.Add(T1);

                onStepTimeSolve?.Invoke(this, T1);

                T0 = T1;
                newP = P.Add(newC.Multiply(T0));
                // currentTime += Simulation_Step_Time;
            }

            return solutions;
            // return solves;
        }
    }
}
