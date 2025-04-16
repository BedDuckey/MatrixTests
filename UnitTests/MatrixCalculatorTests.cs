using Xunit;
using Moq;
using MatrixCalc;
using System;

namespace MatrixTests.UnitTests
{
    public class MatrixCalculatorTests
    {
        private readonly Mock<IMatrixCalc> _calculatorMock;
        private readonly Mock<IMatrixValidator> _validatorMock;
        private readonly IMatrixGenerator _matrixGenerator;
        private readonly IMatrixPrinter _matrixPrinter;
        private readonly MyMatrixCalculator _realCalculator;

        public MatrixCalculatorTests()
        {
            _calculatorMock = new Mock<IMatrixCalc>();
            _validatorMock = new Mock<IMatrixValidator>();
            _matrixGenerator = new MatrixGenerator();
            _matrixPrinter = new MatrixPrinter();
            _realCalculator = new MyMatrixCalculator(_validatorMock.Object);
        }

        // Тесты для моков
        [Fact]
        public void Mock_Add_ReturnsPredefinedResult()
        {
            // Arrange
            var expected = new double[,] { { 1, 1 }, { 1, 1 } };
            var matrixA = new double[2, 2];
            var matrixB = new double[2, 2];

            _calculatorMock.Setup(x => x.Add(It.IsAny<double[,]>(), It.IsAny<double[,]>()))
                         .Returns(expected);

            // Act
            var result = _calculatorMock.Object.Add(matrixA, matrixB);

            // Assert
            Assert.Equal(expected, result);
            _calculatorMock.Verify(x => x.Add(It.IsAny<double[,]>(), It.IsAny<double[,]>()), Times.Once);
        }

        [Fact]
        public void Mock_MultipleOperations_SequenceVerification()
        {
            // Arrange
            var matrix = _matrixGenerator.CreateRandom(2, 2);

            _calculatorMock.SetupSequence(x => x.Add(matrix, matrix))
                         .Returns(matrix)
                         .Throws<InvalidOperationException>();

            _calculatorMock.Setup(x => x.MultiplyByScalar(matrix, 2.0))
                         .Returns(matrix);

            // Act
            var firstResult = _calculatorMock.Object.Add(matrix, matrix);
            var secondResult = _calculatorMock.Object.MultiplyByScalar(matrix, 2.0);

            // Assert
            Assert.Equal(matrix, firstResult);
            Assert.Equal(matrix, secondResult);
            Assert.Throws<InvalidOperationException>(() => _calculatorMock.Object.Add(matrix, matrix));
        }

        [Fact]
        public void Mock_Subtract_ReturnsExpectedResult()
        {
            // Arrange
            var expected = new double[,] { { 1, 1 }, { 1, 1 } };
            var matrixA = new double[2, 2];
            var matrixB = new double[2, 2];

            _calculatorMock.Setup(x => x.Subtract(It.IsAny<double[,]>(), It.IsAny<double[,]>()))
                         .Returns(expected);

            // Act
            var result = _calculatorMock.Object.Subtract(matrixA, matrixB);

            // Assert
            Assert.Equal(expected, result);
            _calculatorMock.Verify(x => x.Subtract(It.IsAny<double[,]>(), It.IsAny<double[,]>()), Times.Once);
        }

        [Fact]
        public void Mock_Multiply_VerifiesParameters()
        {
            // Arrange
            var testMatrix = new double[,] { { 1, 2 } };
            _calculatorMock.Setup(x => x.Multiply(It.IsAny<double[,]>(), It.IsAny<double[,]>()))
                         .Returns(new double[1, 1]);

            // Act
            _calculatorMock.Object.Multiply(testMatrix, testMatrix);

            // Assert
            _calculatorMock.Verify(x => x.Multiply(
                It.Is<double[,]>(m => m.GetLength(0) == 1 && m.GetLength(1) == 2),
                It.IsAny<double[,]>()),
                Times.Once);
        }

        [Fact]
        public void Mock_Transpose_ReturnsTransposedMatrix()
        {
            // Arrange
            var matrix = new double[,] { { 1, 2 } };
            var expected = new double[,] { { 1 }, { 2 } };

            _calculatorMock.Setup(x => x.Transpose(It.IsAny<double[,]>()))
                         .Returns(expected);

            // Act
            var result = _calculatorMock.Object.Transpose(matrix);

            // Assert
            Assert.Equal(expected, result);
            _calculatorMock.Verify(x => x.Transpose(It.IsAny<double[,]>()), Times.Once);
        }

        [Fact]
        public void Mock_Add_ThrowsExceptionWhenValidatorFails()
        {
            // Arrange
            var matrixA = new double[1, 2];
            var matrixB = new double[2, 2];

            _calculatorMock.Setup(x => x.Add(It.IsAny<double[,]>(), It.IsAny<double[,]>()))
                         .Throws<ArgumentException>();

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                _calculatorMock.Object.Add(matrixA, matrixB));
        }

        [Fact]
        public void Mock_MultiplyByScalar_WithZero_ReturnsZeroMatrix()
        {
            // Arrange
            var matrix = new double[,] { { 1, 2 }, { 3, 4 } };
            var expected = new double[,] { { 0, 0 }, { 0, 0 } };

            _calculatorMock.Setup(x => x.MultiplyByScalar(It.IsAny<double[,]>(), 0))
                         .Returns(expected);

            // Act
            var result = _calculatorMock.Object.MultiplyByScalar(matrix, 0);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Mock_VerifyMethodNotCalled()
        {
            // Arrange
            var matrix = new double[2, 2];

            // Act - не вызываем никаких методов

            // Assert
            _calculatorMock.Verify(x => x.Add(It.IsAny<double[,]>(), It.IsAny<double[,]>()), Times.Never);
        }

        [Fact]
        public void Mock_SequenceOfOperations()
        {
            // Arrange
            var matrix = new double[2, 2];
            var results = new double[][,]
            {
        new double[,] { { 1, 1 }, { 1, 1 } },
        new double[,] { { 2, 2 }, { 2, 2 } }
            };

            _calculatorMock.SetupSequence(x => x.MultiplyByScalar(matrix, It.IsAny<double>()))
                         .Returns(results[0])
                         .Returns(results[1]);

            // Act
            var first = _calculatorMock.Object.MultiplyByScalar(matrix, 1);
            var second = _calculatorMock.Object.MultiplyByScalar(matrix, 2);

            // Assert
            Assert.Equal(results[0], first);
            Assert.Equal(results[1], second);
        }

        // Тесты для реального калькулятора
        [Fact]
        public void Real_Add_ValidMatrices_ReturnsCorrectSum()
        {
            // Arrange
            var matrixA = new double[,] { { 1, 2 }, { 3, 4 } };
            var matrixB = new double[,] { { 5, 6 }, { 7, 8 } };
            var expected = new double[,] { { 6, 8 }, { 10, 12 } };

            _validatorMock.Setup(v => v.ValidateMatrices(matrixA, matrixB));

            // Act
            var result = _realCalculator.Add(matrixA, matrixB);

            // Debug output
            _matrixPrinter.Print(matrixA);
            _matrixPrinter.Print(matrixB);
            _matrixPrinter.Print(result);

            // Assert
            Assert.Equal(expected, result);
            _validatorMock.Verify(v => v.ValidateMatrices(matrixA, matrixB), Times.Once);
        }

        [Fact]
        public void Real_Subtract_ValidMatrices_ReturnsCorrectResult()
        {
            // Arrange
            var matrixA = new double[,] { { 5, 6 }, { 7, 8 } };
            var matrixB = new double[,] { { 1, 2 }, { 3, 4 } };
            var expected = new double[,] { { 4, 4 }, { 4, 4 } };

            _validatorMock.Setup(v => v.ValidateMatrices(matrixA, matrixB));

            // Act
            var result = _realCalculator.Subtract(matrixA, matrixB);

            // Assert
            Assert.Equal(expected, result);
            _validatorMock.Verify(v => v.ValidateMatrices(matrixA, matrixB), Times.Once);
        }

        [Fact]
        public void Real_Multiply_ValidMatrices_ReturnsCorrectProduct()
        {
            // Arrange
            var matrixA = new double[,] { { 1, 2 }, { 3, 4 } };
            var matrixB = new double[,] { { 2, 0 }, { 1, 2 } };
            var expected = new double[,] { { 4, 4 }, { 10, 8 } };

            _validatorMock.Setup(v => v.ValidateMultiplication(matrixA, matrixB));

            // Act
            var result = _realCalculator.Multiply(matrixA, matrixB);

            // Assert
            Assert.Equal(expected, result);
            _validatorMock.Verify(v => v.ValidateMultiplication(matrixA, matrixB), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2.5)]
        [InlineData(-3)]
        public void Real_MultiplyByScalar_ValidInput_ReturnsCorrectResult(double scalar)
        {
            // Arrange
            var matrix = new double[,] { { 1, 2 }, { 3, 4 } };
            var expected = new double[,]
            {
                { 1 * scalar, 2 * scalar },
                { 3 * scalar, 4 * scalar }
            };

            _validatorMock.Setup(v => v.ValidateMatrix(matrix));

            // Act
            var result = _realCalculator.MultiplyByScalar(matrix, scalar);

            // Assert
            Assert.Equal(expected, result);
            _validatorMock.Verify(v => v.ValidateMatrix(matrix), Times.Once);
        }

        [Fact]
        public void Real_Transpose_ValidMatrix_ReturnsCorrectResult()
        {
            // Arrange
            var matrix = new double[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            var expected = new double[,] { { 1, 4 }, { 2, 5 }, { 3, 6 } };

            _validatorMock.Setup(v => v.ValidateMatrix(matrix));

            // Act
            var result = _realCalculator.Transpose(matrix);

            // Assert
            Assert.Equal(expected, result);
            _validatorMock.Verify(v => v.ValidateMatrix(matrix), Times.Once);
        }

        // Тесты на ошибки
        [Fact]
        public void Real_Add_InvalidMatrices_ThrowsException()
        {
            // Arrange
            var matrixA = new double[,] { { 1, 2 } };
            var matrixB = new double[,] { { 1, 2, 3 } };

            _validatorMock.Setup(v => v.ValidateMatrices(matrixA, matrixB))
                         .Throws<ArgumentException>();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _realCalculator.Add(matrixA, matrixB));
            _validatorMock.Verify(v => v.ValidateMatrices(matrixA, matrixB), Times.Once);
        }

        [Fact]
        public void Real_Transpose_NullMatrix_ThrowsException()
        {
            // Arrange
            double[,] matrix = null;

            _validatorMock.Setup(v => v.ValidateMatrix(matrix))
                         .Throws<ArgumentNullException>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _realCalculator.Transpose(matrix));
            _validatorMock.Verify(v => v.ValidateMatrix(matrix), Times.Once);
        }
    }
}