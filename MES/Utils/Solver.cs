using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MES.Model;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MES.Utils
{

    class Solver
    {
        public event EventHandler<CustomEventArgs> onStepTimeSolve;

        public Task<List<Vector<double>>> SolveUnstationaryAsync(Matrix<double> H, Matrix<double> C, Vector<double> P, SimulationData simulationData, CancellationToken token)
        {
            return Task.Run(() =>
            {
                token.ThrowIfCancellationRequested();
                double currentTime = 0;
                double[] t0 = new double[P.Count];

                for (int i = 0; i < t0.Length; i++)
                    t0[i] = simulationData.Initial_Temperature;

                Vector<double> T0 = DenseVector.OfArray(t0);
                Matrix<double> newC = C.Divide(simulationData.Simulation_Step_Time);
                Matrix<double> newH = H.Add(newC);
                Vector<double> newP = P.Add(newC.Multiply(T0));

                List<Vector<double>> solutions = new List<Vector<double>>();

                //for (currentTime = 0; currentTime < simulationData.Simulation_Time; currentTime += simulationData.Simulation_Step_Time)
                while (currentTime < simulationData.Simulation_Time)
                {
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }
                    Vector<double> T1 = newH.Solve(newP);
                    solutions.Add(T1);

                    T0 = T1;
                    newP = P.Add(newC.Multiply(T0));
                    currentTime += simulationData.Simulation_Step_Time;
                    onStepTimeSolve?.Invoke(this, new CustomEventArgs(T1, currentTime));
                }

                return solutions;
            });
            // return solves;
        }
    }
}
