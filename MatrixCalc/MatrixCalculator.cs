using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCalc
{
    public class MyMatrixCalculator : IMatrixCalc
    {
        private readonly IMatrixValidator _validator;

        public MyMatrixCalculator(IMatrixValidator validator)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            Console.WriteLine("Инициализация калькулятора матриц");
        }

        public double[,] Add(double[,] matrixA, double[,] matrixB)
        {
            _validator.ValidateMatrices(matrixA, matrixB);

            int rows = matrixA.GetLength(0);
            int cols = matrixA.GetLength(1);
            var result = new double[rows, cols];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    result[i, j] = matrixA[i, j] + matrixB[i, j];

            return result;
        }

        public double[,] Subtract(double[,] matrixA, double[,] matrixB)
        {
            _validator.ValidateMatrices(matrixA, matrixB);

            int rows = matrixA.GetLength(0);
            int cols = matrixA.GetLength(1);
            var result = new double[rows, cols];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    result[i, j] = matrixA[i, j] - matrixB[i, j];

            return result;
        }

        public double[,] Multiply(double[,] matrixA, double[,] matrixB)
        {
            _validator.ValidateMultiplication(matrixA, matrixB);

            int rows = matrixA.GetLength(0);
            int cols = matrixB.GetLength(1);
            int common = matrixA.GetLength(1);
            var result = new double[rows, cols];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    for (int k = 0; k < common; k++)
                        result[i, j] += matrixA[i, k] * matrixB[k, j];

            return result;
        }

        public double[,] MultiplyByScalar(double[,] matrix, double scalar)
        {
            _validator.ValidateMatrix(matrix);

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            var result = new double[rows, cols];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    result[i, j] = matrix[i, j] * scalar;

            return result;
        }

        public double[,] Transpose(double[,] matrix)
        {
            _validator.ValidateMatrix(matrix);

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            var result = new double[cols, rows];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    result[j, i] = matrix[i, j];

            return result;
        }
    }
}
