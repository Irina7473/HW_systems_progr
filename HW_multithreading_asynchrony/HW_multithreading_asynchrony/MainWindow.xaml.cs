using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace HW_multithreading_asynchrony
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Task taskSimple;
        public Task taskFibonacci;
        private CancellationTokenSource cancelTokenS;
        private CancellationTokenSource cancelTokenF;

        //List<int> SimpleList;
        //List<int> FibonacciList;
        private int lowerS, upperS;
        private bool simple;
        
        public MainWindow()
        {
            InitializeComponent();                 
            //SimpleList = new List<int>();
            //FibonacciList = new List<int>();
            lowerS = 2;
            upperS = 1000;
            TextBox_simpleLowerBound.Text = "";
            TextBox_simpleUpperBound.Text = "";
        }

        // Prime numbers

        private void TextBox_simpleLowerBound_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBox_simpleLowerBound.Text != "") lowerS = borderCheck(TextBox_simpleLowerBound.Text);
            else lowerS = 2;
        }

        private void TextBox_simpleUpperBound_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBox_simpleUpperBound.Text != "") upperS = borderCheck(TextBox_simpleUpperBound.Text);
            else upperS = 1000;
        }

        private async void Button_simpleStart_Click(object sender, RoutedEventArgs e)               
        {            
            if (cancelTokenS != null) return;
            try
            {
                using (cancelTokenS = new CancellationTokenSource())
                {
                    //RichTextBox_simpleOutput.Document.Blocks.Clear();
                    Output.AppendText("Начинаю генерацию простых чисел: ");
                    await SimpleGenerationAsync(Output, cancelTokenS.Token);
                }
            }
            catch (Exception exception)
            {
                //MessageBox.Show(exception.Message);
            }
            finally
            {
                cancelTokenS = null;
            }       
        }


        private void Button_simpleStop_Click(object sender, RoutedEventArgs e)
        {
            cancelTokenS?.Cancel();
            lowerS = 2;
            upperS = 1000;
            TextBox_simpleLowerBound.Text = "";
            TextBox_simpleUpperBound.Text = "";
            Output.AppendText("Генерация простых чисел закончена. ");
        }

        private void Button_simpleRestart_Click(object sender, RoutedEventArgs e)
        {
            
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
       
        private async Task SimpleGenerationAsync(RichTextBox output, CancellationToken token)
        {    
            await output.Dispatcher.Invoke(async () =>
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
                        if (simple) Output.AppendText($"{n} ");
                    }

                }
                else
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
                        if (simple) Output.AppendText($"{n} ");
                    await Task.Delay(10, token);
                    }
                Output.AppendText("Достигнута верхняя граница для простых чисел. ");

            }, DispatcherPriority.Normal, token); 
        }
       
        // Fibonacci numbers 
        private async void Button_FibonacciStart_Click(object sender, RoutedEventArgs e)
        {
            if (cancelTokenF != null) return;
            try
            {
                using (cancelTokenF = new CancellationTokenSource())
                {
                    /*
                    RichTextBox_FibonacciOutput.Document.Blocks.Clear();
                    RichTextBox_FibonacciOutput.AppendText("Начинаю генерацию чисел Фибоначчи: ");
                    RichTextBox_FibonacciOutput.AppendText("0 1 1 ");
                    await FibonacciGeneration(RichTextBox_FibonacciOutput, cancelTokenF.Token, 1, 1);*/
                    
                    Output.AppendText("Начинаю генерацию чисел Фибоначчи: ");
                    Output.AppendText("0 1 1 ");
                    await FibonacciGeneration(Output, cancelTokenF.Token, 1 ,1);
                    Output.AppendText("Генерация чисел Фибоначчи закончена. ");
                }
            }
            catch (Exception exception)
            {
                //MessageBox.Show(exception.Message);
            }
            finally
            {
                cancelTokenF = null;
            }
        }

        private void Button_FibonacciStop_Click(object sender, RoutedEventArgs e)
        {
            cancelTokenF?.Cancel();
            Output.AppendText("Генерация чисел Фибоначчи закончена. ");
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
               
        private async Task FibonacciGeneration (RichTextBox output, CancellationToken token, long number1, long number2)
        {
            await output.Dispatcher.Invoke(async () =>
            {
                if ((number1 + number2) < 1000000000000000000)
                {
                    output.AppendText($"{number1 + number2} ");
                    await FibonacciGeneration(output, token, number2, number1 + number2);                    
                }
            }, DispatcherPriority.Normal, token);            
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