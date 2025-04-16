using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCalc
{
    public interface IMatrixPrinter
    {
        void Print(double[,] matrix);
    }
}