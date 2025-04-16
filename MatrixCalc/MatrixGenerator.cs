using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCalc
{
    public class MatrixGenerator : IMatrixGenerator
    {
        private readonly Random _random = new Random();

        public double[,] CreateRandom(int rows, int cols)
        {
            var matrix = new double[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    matrix[i, j] = _random.NextDouble() * 10;
            return matrix;
        }
    }
}