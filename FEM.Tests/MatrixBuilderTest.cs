using NUnit.Framework;
using MES.Utils;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Linq;
namespace FEM.Tests
{
    public class MatrixBuilderTest
    {
        MatrixBuilder _matrixBuilder;
        TestData _testData;

        [SetUp]
        public void Setup()
        {
            _testData = new TestData();
            _matrixBuilder = new MatrixBuilder(_testData.Mesh, _testData.SimulationData);
        }

        [Test]
        public void Cmatrix_should_be_equal_to_synthetic_row()
        {
            //Arrange
            DenseVector CmatrixFirstRow = DenseVector.OfArray(new double[] { 674.0, 337.0, 0, 0, 337.0, 168.5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
            //Act
            var Cmatrix = _matrixBuilder.buildCglobalMatrix();
            var rounded = Cmatrix.Row(0).Map(el => Math.Round(el, 1));
            //Assert
            Assert.IsTrue(rounded.ToArray().SequenceEqual(rounded));
        }
    }
}