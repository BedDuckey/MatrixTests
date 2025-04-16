using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCalc
{
    public class MatrixValidator : IMatrixValidator
    {
        public void ValidateMatrices(double[,] matrixA, double[,] matrixB)
        {
            ValidateMatrix(matrixA);
            ValidateMatrix(matrixB);

            if (matrixA.GetLength(0) != matrixB.GetLength(0) ||
                matrixA.GetLength(1) != matrixB.GetLength(1))
                throw new ArgumentException(
                    $"Размеры матриц не совпадают: [{matrixA.GetLength(0)}x{matrixA.GetLength(1)}] " +
                    $"и [{matrixB.GetLength(0)}x{matrixB.GetLength(1)}]");
        }

        public void ValidateMatrix(double[,] matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix), "Матрица не может быть null");
        }

        public void ValidateMultiplication(double[,] matrixA, double[,] matrixB)
        {
            ValidateMatrix(matrixA);
            ValidateMatrix(matrixB);

            if (matrixA.GetLength(1) != matrixB.GetLength(0))
                throw new ArgumentException(
                    $"Невозможно умножить: [{matrixA.GetLength(0)}x{matrixA.GetLength(1)}] " +
                    $"на [{matrixB.GetLength(0)}x{matrixB.GetLength(1)}]");
        }
    }
}
