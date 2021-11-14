using System;
using System.Threading.Tasks;

namespace ConsoleAppTest
{
    class Program
    {
        static int lowerS = 2;
        static int upperS = 100;
        static void Main(string[] args)
        {
            Task task = new Task(Display);
            task.Start();
                       
            Task task5 = new Task(SimpleGeneration);
            lowerS = 20;
            upperS = 1000;
            task5.Start();

            Task task1 = new Task(() => Console.WriteLine("Task1 is executed"));
            task1.Start();
            task.Wait();

            Task task2 = Task.Factory.StartNew(() => Console.WriteLine("Task2 is executed"));

            Task task3 = Task.Run(() => Console.WriteLine("Task3 is executed"));

            Task.WaitAll();
            Console.WriteLine("Завершение метода Main");

        }

        static void Display()
        {
            Console.WriteLine("Начало работы метода Display");

            Console.WriteLine("Завершение работы метода Display");
        }

        private static void SimpleGeneration()
        {           
            if (upperS == 0)
            {
                for (int n = lowerS; ; n++)
                {
                    for (int i = 2; i < n; i++)
                    {
                        if (n % i == 0) return;
                    }
                    Console.Write ($"{n} ");
                }

            }
            else
            {
                for (int n = lowerS; n < upperS; n++)
                {
                    bool simple = true;
                    for (int i = 2; i < n; i++)
                    {
                        if (n % i == 0)
                        {
                            simple = false;
                            break;
                        }
                    }
                    if (simple) Console.Write($"{n} ");
                }
            }
        }


    }
}
