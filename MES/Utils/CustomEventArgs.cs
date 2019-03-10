using MathNet.Numerics.LinearAlgebra;
using System;

namespace MES.Utils
{
    public class CustomEventArgs : EventArgs
    {
        public Vector<double> Solution { get; set; }
        public double TimeElapsed { get; set; }

        public CustomEventArgs(Vector<double> solution, double timeElapsed)
        {
            Solution = solution;
            TimeElapsed = timeElapsed;
        }
    }
}
