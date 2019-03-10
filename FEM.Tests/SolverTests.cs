using MathNet.Numerics.LinearAlgebra;
using MES.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
namespace FEM.Tests
{
    class SolverTests
    {
        Solver _solver;
        TestData _testData;
        MatrixBuilder _matrixBuilder;
        CancellationToken token;
        [SetUp]
        public void Setup()
        {
            _testData = new TestData();
            _matrixBuilder = new MatrixBuilder(_testData.Mesh, _testData.SimulationData);
            _solver = new Solver();
        }

        [Test]
        public  void test1()
        {
            //Arrange
            List<double> validMinTemp = new List<double>() { 110.0, 168.8, 242.8, 318.6, 391.3, 459.0, 521.6, 579.0, 631.7, 679.9 };
            List<double> validMaxTemp = new List<double>() { 365.8, 502.6, 587.4, 649.4, 700.1, 744.1, 783.4, 819, 851.4, 881.1 };
            //Act
            var H = _matrixBuilder.buildHglobalMatrix();
            var P = _matrixBuilder.buildPglobalVector();
            var C = _matrixBuilder.buildCglobalMatrix();
            List<Vector<double>> solutions = _solver.SolveUnstationaryAsync(H, C, P, _testData.SimulationData, token).Result;

            List<double> minTemp = solutions.Select(vec => Math.Round(vec.Min(), 1)).ToList();
            List<double> maxTemp = solutions.Select(vec => Math.Round(vec.Max(), 1)).ToList();
            //Assert
            Assert.IsTrue(validMaxTemp.SequenceEqual(maxTemp));
            Assert.IsTrue(validMinTemp.SequenceEqual(minTemp));
        }
    }

}
