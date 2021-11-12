using System;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace CHP2
{
    class Program
    {
        static void Main(string[] args)
        {            
            {
                CountWordInFile counter = new CountWordInFile(@"G:\STEP\systems-progr\qq.txt", "ura");

                Thread myThread = new Thread(counter.Count);
                myThread.Start();

                for (int i = 2; i < 9; i++)
                {
                    Console.WriteLine("Главный поток:");
                    Console.WriteLine(i * i);
                    Thread.Sleep(300);
                }

            }
        }
    }
    public class CountWordInFile
    {
        private string _path;
        private string _word;

        public CountWordInFile(string path, string word)
        {            
            _path = path;
            _word = word;
        }

        public void Count()
        {
            Console.WriteLine("Дочерний процесс:");
            int count = 0;
            Process.Start("notepad",_path);

            using (StreamReader reader = new StreamReader(_path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] words = line.Split(' ');
                    foreach (var word in words) if (word ==_word) count++;                    
                }
            }
            Console.WriteLine($"Слово {_word} встречается в файле {_path} {count} раз");
            Thread.Sleep(400);
        }
    }
}
