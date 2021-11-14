using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HW_multithreading_asynchrony
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Task taskSimple;
        public Task taskFibonacci;
        // CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

        //List<int> SimpleList;
        //List<int> FibonacciList;
        private int lowerS, upperS, lowerF, upperF;
        private bool simple;
        
        public MainWindow()
        {
            InitializeComponent();

            //CancellationToken token = cancelTokenSource.Token;     
            //SimpleList = new List<int>();
            //FibonacciList = new List<int>();

            lowerS = 2;
            upperS = 0;
            lowerF = 0;
            upperF = 0;
        }

        // Prime numbers

        private void TextBox_simpleLowerBound_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBox_simpleLowerBound.Text != "") lowerS = borderCheck(TextBox_simpleLowerBound.Text);
        }

        private void TextBox_simpleUpperBound_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBox_simpleUpperBound.Text != "") upperS = borderCheck(TextBox_simpleUpperBound.Text);
        }

        private void Button_simpleStart_Click(object sender, RoutedEventArgs e)
        {
            taskSimple = Task.Run(() =>
            {
                RichTextBox_simpleOutput.Document.Blocks.Clear();
                SimpleGeneration();
            });
            //RichTextBox_simpleOutput.Document.Blocks.Clear();            
            //taskSimple = Task.Run(() => SimpleGeneration());
            //taskSimple = new Task(SimpleGeneration);
            //taskSimple.Start();
        }

        private void Button_simpleStop_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_simpleRestart_Click(object sender, RoutedEventArgs e)
        {
            RichTextBox_simpleOutput.Document.Blocks.Clear();
        }
       
        private void Button_simplePause_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_simpleContinue_Click(object sender, RoutedEventArgs e)
        {

        }

        private int borderCheck(string text)
        {
            int number;
            try
            {
                number = Int32.Parse(text);
                if (number < 0)
                {
                    MessageBox.Show("Введите границу в виде целого числа > или = 0");
                }
                return number;
            }
            catch { MessageBox.Show("Введите границу в виде целого числа > или = 0"); return -100; }
            
        }
        private void SimpleGeneration()
        {
            if (upperS == 0)
            {
                for (int n = lowerS; ; n++)
                {
                    simple = true;
                    for (int i = 2; i < n; i++)
                    {
                        if (n % i == 0)
                        {
                            simple = false;
                            break;
                        }
                    }
                    if (simple) RichTextBox_simpleOutput.AppendText($"{n} ");
                }

            }
            else 
            { 
            for (int n = lowerS; n < upperS; n++)
            {
                   simple = true;
                    for (int i = 2; i < n; i++)
                {
                        if (n % i == 0)
                        {
                            simple = false;
                            break;
                        }
                }
                if (simple) RichTextBox_simpleOutput.AppendText($"{n} ");
            }
            }
        }


        // Fibonacci numbers 
        private void Button_FibonacciStart_Click(object sender, RoutedEventArgs e)
        {
            taskFibonacci = Task.Run(() =>
            {
                RichTextBox_FibonacciOutput.Document.Blocks.Clear();
                RichTextBox_FibonacciOutput.AppendText("0 1 1 ");
                FibonacciGeneration(1, 1);
            });        
        }

        private void Button_FibonacciStop_Click(object sender, RoutedEventArgs e)
        {
            //cancelTokenSource.Cancel();
        }

        private void Button_FibonacciRestart_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void Button_FibonacciPause_Click(object sender, RoutedEventArgs e)
        {

        }
               
        private void Button_FibonaccieContinue_Click(object sender, RoutedEventArgs e)
        {
            
        }
               
        private void FibonacciGeneration (long number1, long number2)
        {
            if ((number1 + number2) < 1000000000000000000)
            {
                RichTextBox_FibonacciOutput.AppendText($"{number1 + number2} ");
                FibonacciGeneration(number2, number1 + number2);
            }
        }
    }
}


/*  
 * Задание 1:
Создайте оконное приложение, генерирующее набор
простых чисел в диапазоне, указанном пользователем.
Если не указана нижняя граница, поток с стартует с 2.
Если не указана верхняя граница, генерирование про-
исходит до завершения приложения. Используйте меха-
низм потоков. Числа должны отображаться в оконном
интерфейсе.
Задание 2
Добавьте к первому заданию поток, генерирующий
набор чисел Фибоначчи. Числа должны отображаться
в оконном интерфейсе.
Задание 3
Добавьте ко второму заданию кнопки для полной
остановки каждого из потоков. Одна кнопка на один
поток. Если пользователь нажал на кнопку остановки,
поток полностью прекращает свою работу.
Задание 4
Добавьте к третьему заданию кнопки для приоста-
новления и возобновления каждого из потоков. Например,
пользователь может приостановить генерацию чисел Фи-
боначчи по нажатию на кнопку. Продолжение генерации
возможно по нажатию на другую кнопку
Задание 5
Добавьте к четвертому заданию возможность полного
рестарта потоков с новыми границами

*/
