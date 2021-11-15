using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WpfAppTEST
{
    public partial class MainWindow : Window
    {
        private CancellationTokenSource _cancellation;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_RandomStart_OnClick(object sender, RoutedEventArgs e)
        {
            if (_cancellation is not null) return;
            try
            {
                using (_cancellation = new CancellationTokenSource())
                {
                    await RandomAsync(Random, _cancellation.Token);
                }
            }
            catch (Exception exception)
            {
                //MessageBox.Show(exception.Message);
            }
            finally
            {
                _cancellation = null;
            }
        }
        private async void Button_RandomStop_OnClick(object sender, RoutedEventArgs e)
        {
            _cancellation?.Cancel();
        }
        private async Task RandomAsync(Label label, CancellationToken token)
        {
            await label.Dispatcher.Invoke(async () =>
            {
                var random = new Random();
                //await Task.Delay(5000, token);
                label.Content = random.Next();
            }, DispatcherPriority.Normal, token);
        }
    }
}