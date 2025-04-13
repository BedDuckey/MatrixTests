using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCalc
{
    public class MyMatrixCalculator: IMatrixCalc
    {
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public MyMatrixCalculator()
        {
            Console.WriteLine("Инициализация калькулятора матриц");
        }

        public double[,] Add(double[,] matrixA, double[,] matrixB)
        {
            ValidateMatrices(matrixA, matrixB);

            int rows = matrixA.GetLength(0);
            int cols = matrixA.GetLength(1);
            var result = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = matrixA[i, j] + matrixB[i, j];
                }
            }

            return result;
        }

        // Остальные методы остаются без изменений
        public double[,] Subtract(double[,] matrixA, double[,] matrixB)
        {
            ValidateMatrices(matrixA, matrixB);

            int rows = matrixA.GetLength(0);
            int cols = matrixA.GetLength(1);
            var result = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = matrixA[i, j] - matrixB[i, j];
                }
            }

            return result;
        }

        public double[,] Multiply(double[,] matrixA, double[,] matrixB)
        {
            if (matrixA.GetLength(1) != matrixB.GetLength(0))
            {
                throw new ArgumentException(
                    "Количество столбцов первой матрицы должно быть равно количеству строк второй матрицы");
            }

            int rows = matrixA.GetLength(0);
            int cols = matrixB.GetLength(1);
            int common = matrixA.GetLength(1);
            var result = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < common; k++)
                    {
                        sum += matrixA[i, k] * matrixB[k, j];
                    }
                    result[i, j] = sum;
                }
            }

            return result;
        }

        public double[,] MultiplyByScalar(double[,] matrix, double scalar)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException(nameof(matrix), "Матрица не может быть null");
            }

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            var result = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = matrix[i, j] * scalar;
                }
            }

            return result;
        }

        public double[,] Transpose(double[,] matrix)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException(nameof(matrix), "Матрица не может быть null");
            }

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            var result = new double[cols, rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[j, i] = matrix[i, j];
                }
            }

            return result;
        }

        private void ValidateMatrices(double[,] matrixA, double[,] matrixB)
        {
            if (matrixA == null)
            {
                throw new ArgumentNullException(nameof(matrixA), "Первая матрица не может быть null");
            }

            if (matrixB == null)
            {
                throw new ArgumentNullException(nameof(matrixB), "Вторая матрица не может быть null");
            }

            if (matrixA.GetLength(0) != matrixB.GetLength(0) ||
                matrixA.GetLength(1) != matrixB.GetLength(1))
            {
                throw new ArgumentException(
                    "Матрицы должны иметь одинаковые размеры. " +
                    $"Получено: [{matrixA.GetLength(0)}x{matrixA.GetLength(1)}] и " +
                    $"[{matrixB.GetLength(0)}x{matrixB.GetLength(1)}]");
            }
        }
    }
}
