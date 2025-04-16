using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCalc
{
    public interface IMatrixValidator
    {
        void ValidateMatrices(double[,] matrixA, double[,] matrixB);
        void ValidateMatrix(double[,] matrix);
        void ValidateMultiplication(double[,] matrixA, double[,] matrixB);
    }
}
