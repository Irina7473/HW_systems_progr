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
            Task taskSimple = new Task(SimpleGeneration);
            lowerS = 20;
            upperS = 1000;
            taskSimple.Start();            

            Task taskFibonacci = Task.Run(() =>
            {
                Console.WriteLine("Начало taskFibonacci");
                Console.Write ("0 1 1 ");
                FibonacciGeneration(1, 1);
                Console.WriteLine("Завершение taskFibonacci");
            });
            
            taskSimple.Wait();
            taskFibonacci.Wait();
            //Task.WaitAll();
            Console.WriteLine("Завершение метода Main");

        }

        private static void SimpleGeneration()
        {
            Console.WriteLine("Начало taskSimple");
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
            Console.WriteLine("Завершение taskSimple");
        }

        private static void FibonacciGeneration(long number1, long number2)
        {
            if ((number1 + number2) < 1000000000000000000)
            {                
                Console.Write($"!{number1 + number2} ");
                FibonacciGeneration(number2, number1 + number2);
            }
        }

    }
}
