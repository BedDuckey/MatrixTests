using MatrixCalc;
using System;
using System.Text;


internal class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("Матричный калькулятор \n");

        IMatrixCalc calculator = new MyMatrixCalculator();

        try
        {
            // Создаем тестовые матрицы
            double[,] matrixA = Matrix.CreateRandom(2, 2);
            double[,] matrixB = Matrix.CreateRandom(2, 2);

            // Демонстрация операций
            DisplayMatrixOperation("Матрица A", matrixA);
            DisplayMatrixOperation("Матрица B", matrixB);

            PerformOperation("Сложение (A + B)", () => calculator.Add(matrixA, matrixB));
            PerformOperation("Вычитание (A - B)", () => calculator.Subtract(matrixA, matrixB));
            PerformOperation("Умножение (A * B)", () => calculator.Multiply(matrixA, matrixB));
            PerformOperation("Умножение на скаляр (A * 2.5)", () => calculator.MultiplyByScalar(matrixA, 2.5));
            PerformOperation("Транспонирование A", () => calculator.Transpose(matrixA));
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n Ошибка: {ex.Message}");
            Console.ResetColor();
        }

        Console.WriteLine("\n Нажмите любую клавишу для выхода...");
        Console.ReadKey();
    }

    static void DisplayMatrixOperation(string title, double[,] matrix)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\n{title}:");
        Console.ResetColor();
        Matrix.Print(matrix);
    }

    static void PerformOperation(string title, Func<double[,]> operation)
    {
        try
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n{title}:");
            Console.ResetColor();

            double[,] result = operation();
            Matrix.Print(result);
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Ошибка при выполнении операции: {ex.Message}");
            Console.ResetColor();
        }
    }
}

