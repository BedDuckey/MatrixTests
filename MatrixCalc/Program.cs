using MatrixCalc;
using System;
using System.Text;


internal class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("🟢 Матричный калькулятор 🟢\n");

        // 1. Инициализация всех зависимостей
        IMatrixValidator validator = new MatrixValidator();
        IMatrixPrinter printer = new MatrixPrinter();
        IMatrixGenerator generator = new MatrixGenerator();

        // 2. Создание калькулятора с внедрением зависимостей
        IMatrixCalc calculator = new MyMatrixCalculator(validator);

        try
        {
            // 3. Генерация тестовых матриц
            double[,] matrixA = generator.CreateRandom(2, 2);
            double[,] matrixB = generator.CreateRandom(2, 2);

            // 4. Демонстрация операций
            DisplayMatrixOperation("Матрица A", matrixA, printer);
            DisplayMatrixOperation("Матрица B", matrixB, printer);

            // 5. Выполнение операций
            PerformOperation("Сложение (A + B)",
                () => calculator.Add(matrixA, matrixB),
                printer);

            PerformOperation("Вычитание (A - B)",
                () => calculator.Subtract(matrixA, matrixB),
                printer);

            PerformOperation("Умножение (A * B)",
                () => calculator.Multiply(matrixA, matrixB),
                printer);

            PerformOperation("Умножение на скаляр (A * 2.5)",
                () => calculator.MultiplyByScalar(matrixA, 2.5),
                printer);

            PerformOperation("Транспонирование A",
                () => calculator.Transpose(matrixA),
                printer);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nошибка: {ex.Message}");
        }

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }

    static void DisplayMatrixOperation(string title, double[,] matrix, IMatrixPrinter printer)
    {
        Console.WriteLine($"\n{title}:");
        printer.Print(matrix);
    }

    static void PerformOperation(string title, Func<double[,]> operation, IMatrixPrinter printer)
    {
        try
        {
            Console.WriteLine($"\n{title}:");

            double[,] result = operation();
            printer.Print(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}

