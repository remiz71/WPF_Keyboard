using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WPF_Keyboard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string baceString = "QWERTYUIOPASDFGHJKLZXCVBNM~!@#$%^&*()_+{}|:\"<>?1234567890[],./\\`-=;'qwertyuiopasdfghjklzxcvbnm";
        bool sensetive = false;
        Random rand = new Random();
        KeyConverter keyconv = new KeyConverter();
        DispatcherTimer timer = null;
        int temptimer = 0;
        bool flagBackspace = true;
        int fails = 0;
        public MainWindow()
        {

            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Tick += timer_Tick;
            timer.Interval = new TimeSpan(0, 0,0,0,1000);
            foreach (Button item in this.keys.Children)
            {
                item.Content = item.Content.ToString().ToLower();
            }
        }
        
        private void timer_Tick( object sender, EventArgs e)
        {
            temptimer++;
            Speed();
        }

        private void Speed()
        {
            _speed.Content = Math.Round(((double)tb2.Text.Length / temptimer) * 60).ToString();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            
            foreach (Button item in this.keys.Children)
            {
                if (e.Key == Key.LeftShift || e.Key == Key.RightShift|| e.Key == Key.Capital)
                {
                    if (item.Content.ToString().Length == 1)
                    item.Content = item.Content.ToString().ToUpper();
                }
                if (keyconv.ConvertToString(e.Key) == item.Content.ToString().ToUpper())
                {
                    item.Opacity=0.5;
                }
                if (e.Key == Key.Back)
                {
                    flagBackspace = true;
                }
                else flagBackspace = false;

            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            foreach (Button item in this.keys.Children)
            {
                
                if (e.Key == Key.LeftShift || e.Key == Key.RightShift || e.Key == Key.Capital)
                    item.Content = item.Content.ToString().ToLower();
                if (keyconv.ConvertToString(e.Key) == item.Content.ToString().ToUpper())
                {
                    item.Opacity = 100;
                }
                
            }
        }
        private void Start() 
        {
            tb1.Clear();
            _startBt.Content = "Stop";
            tb1.IsEnabled = true;
            tb2.Focus();
            GenerateString(Convert.ToInt32(dif.Content));
            temptimer = 0;
            timer.Start();

        }
        private void Stop() 
        {
            _startBt.Content = "Start";
            tb2.Clear();
            tb2.IsEnabled = false;
            tb1.IsEnabled = false;
        }
        

        private void GenerateString(int difficulty)
        {
            string tmp = "";
            int _sense = (sensetive) ? 0 : 47;
            for (int i = 0; i < difficulty*100; i++)
            {
                tmp += baceString[rand.Next(_sense, baceString.Length)];
            }
            tmp += " ";
            int countString = difficulty*10;
            for (int i = 0; i < countString; i++)
            {
                tb1.Text += tmp[rand.Next(0, tmp.Length)];
                if (i % difficulty == 0) tb1.Text += " ";
            }
        }
        private void _startBt_Click(object sender, RoutedEventArgs e)
        {
            if (_startBt.Content.ToString() == "Start")
                Start();
            else Stop();
      
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            sensetive = false;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            sensetive = true;
        }

        private void tb2_TextChanged(object sender, TextChangedEventArgs e)
        {
            string tmp;
            tmp = tb1.Text.Substring(0, tb2.Text.Length);
            if (tb2.Text.Equals(tmp))
            {
                tb2.Foreground = new SolidColorBrush(Colors.DarkGreen);
            }
            else
            {
                fails++;
                tb2.Foreground = new SolidColorBrush(Colors.Red);
                _fail.Content = fails;
            }
            if (tb1.Text.Length == tb2.Text.Length)
            {
                timer.Stop();
                MessageBox.Show($"Задание завершено!\n Количество символов {tb1.Text.Length}.\n " +
                    $"Количество ошибок {_fail.Content}.\nСкорость ввода {_speed.Content} символов в минуту", "Сообщение", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                Stop();
            }
        }
    }
}
