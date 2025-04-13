using Xunit;
using MatrixCalc;
using System;

namespace MatrixTests.UnitTests
{
    public class MatrixCalculatorTests
    {
        private readonly IMatrixCalc _calculator;

        public MatrixCalculatorTests()
        {
            _calculator = new MyMatrixCalculator();
        }

        [Fact]
        public void Add_ValidMatrices_ReturnsCorrectSum()
        {
            // Arrange
            var matrixA = new double[,] { { 1, 2 }, { 3, 4 } };
            var matrixB = new double[,] { { 5, 6 }, { 7, 8 } };
            var expected = new double[,] { { 6, 8 }, { 10, 12 } };

            // Act
            var result = _calculator.Add(matrixA, matrixB);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Add_DifferentDimensions_ThrowsArgumentException()
        {
            // Arrange
            var matrixA = new double[,] { { 1, 2 } };
            var matrixB = new double[,] { { 1, 2, 3 } };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _calculator.Add(matrixA, matrixB));
        }

        [Fact]
        public void Multiply_ValidMatrices_ReturnsCorrectProduct()
        {
            // Arrange
            var matrixA = new double[,] { { 1, 2 }, { 3, 4 } };
            var matrixB = new double[,] { { 2, 0 }, { 1, 2 } };
            var expected = new double[,] { { 4, 4 }, { 10, 8 } };

            // Act
            var result = _calculator.Multiply(matrixA, matrixB);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Multiply_IncompatibleDimensions_ThrowsArgumentException()
        {
            // Arrange
            var matrixA = new double[,] { { 1, 2, 3 } };
            var matrixB = new double[,] { { 1, 2 } };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _calculator.Multiply(matrixA, matrixB));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2.5)]
        [InlineData(-3)]
        public void MultiplyByScalar_ValidInput_ReturnsCorrectResult(double scalar)
        {
            // Arrange
            var matrix = new double[,] { { 1, 2 }, { 3, 4 } };
            var expected = new double[,]
            {
            { 1 * scalar, 2 * scalar },
            { 3 * scalar, 4 * scalar }
            };

            // Act
            var result = _calculator.MultiplyByScalar(matrix, scalar);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Transpose_SquareMatrix_ReturnsCorrectResult()
        {
            // Arrange
            var matrix = new double[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            var expected = new double[,] { { 1, 4 }, { 2, 5 }, { 3, 6 } };

            // Act
            var result = _calculator.Transpose(matrix);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}

