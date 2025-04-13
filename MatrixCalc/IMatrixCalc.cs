using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCalc
{
    public interface IMatrixCalc
    {
        double[,] Add(double[,] matrixA, double[,] matrixB);
        double[,] Subtract(double[,] matrixA, double[,] matrixB);
        double[,] Multiply(double[,] matrixA, double[,] matrixB);
        double[,] MultiplyByScalar(double[,] matrix, double scalar);
        double[,] Transpose(double[,] matrix);
    }


}
