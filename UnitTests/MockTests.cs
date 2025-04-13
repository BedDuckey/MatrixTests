using Xunit;
using Moq;
using MatrixCalc;
using System;

namespace MatrixTests.UnitTests
{

    public class MockMatrixCalculatorTests
    {
        private readonly Mock<IMatrixCalc> _mockCalculator;

        public MockMatrixCalculatorTests()
        {
            _mockCalculator = new Mock<IMatrixCalc>();
        }

        [Fact]
        public void Mock_Add_ReturnsPredefinedResult()
        {
            // Arrange
            var expected = new double[,] { { 1, 1 }, { 1, 1 } };
            _mockCalculator.Setup(x => x.Add(It.IsAny<double[,]>(), It.IsAny<double[,]>()))
                          .Returns(expected);

            // Act
            var result = _mockCalculator.Object.Add(new double[2, 2], new double[2, 2]);

            // Assert
            Assert.Equal(expected, result);
            _mockCalculator.Verify(x => x.Add(It.IsAny<double[,]>(), It.IsAny<double[,]>()), Times.Once);
        }

        [Fact]
        public void Mock_Multiply_VerifiesParameters()
        {
            // Arrange
            var testMatrix = new double[,] { { 1, 2 } };
            _mockCalculator.Setup(x => x.Multiply(It.IsAny<double[,]>(), It.IsAny<double[,]>()))
                          .Returns(new double[1, 1]);

            // Act
            _mockCalculator.Object.Multiply(testMatrix, testMatrix);

            // Assert
            _mockCalculator.Verify(x => x.Multiply(
                It.Is<double[,]>(m => m.GetLength(0) == 1 && m.GetLength(1) == 2),
                It.IsAny<double[,]>()),
                Times.Once);
        }

        [Fact]
        public void Mock_Add_ThrowsException()
        {
            // Arrange
            _mockCalculator.Setup(x => x.Add(It.IsAny<double[,]>(), It.IsAny<double[,]>()))
                          .Throws<ArgumentException>();

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                _mockCalculator.Object.Add(new double[1, 2], new double[2, 2]));
        }

        [Fact]
        public void Mock_MultipleOperations_VerifySequence()
        {
            // Arrange
            var matrix = new double[,] { { 1 } };
            _mockCalculator.Setup(x => x.Add(It.IsAny<double[,]>(), It.IsAny<double[,]>()))
                          .Returns(matrix);
            _mockCalculator.Setup(x => x.MultiplyByScalar(It.IsAny<double[,]>(), It.IsAny<double>()))
                          .Returns(matrix);

            // Act
            var addResult = _mockCalculator.Object.Add(matrix, matrix);
            var scalarResult = _mockCalculator.Object.MultiplyByScalar(matrix, 2);

            // Assert
            _mockCalculator.Verify(x => x.Add(matrix, matrix), Times.Once);
            _mockCalculator.Verify(x => x.MultiplyByScalar(matrix, 2), Times.Once);
        }
    }
}
