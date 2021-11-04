using System;
using System.Data;
using System.Diagnostics;
using System.Threading;

namespace CHP1
{
    class Program
    {
        static void Main(string[] args)
        {
            Counter counter = new Counter(7, 3, "+");

            Thread myThread = new Thread(new ThreadStart(counter.Count));
            myThread.Start();

            for (int i = 1; i < 4; i++)
            {
                Console.WriteLine("Главный поток:");
                Console.WriteLine(i * i);
                Thread.Sleep(300);                
            }
            
        }
                
    }

    public class Counter
    {
        private int _x;
        private int _y;
        private string _oper;

        public Counter(int x, int y, string oper)
        {
            _x = x;
            _y = y;
            _oper = oper;
        }

        public void Count()
        {
            if (_oper == "+")
            {
                Console.WriteLine("Второй поток:");
                Console.WriteLine($"{_x} + {_y} = {_x + _y}");
                Thread.Sleep(400);
            }
        }
    }
}
