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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Keyboard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
            KeyConverter keyconv = new KeyConverter();
        public MainWindow()
        {
            InitializeComponent();
            foreach (Button item in this.keys.Children)
            {
                item.Content = item.Content.ToString().ToLower();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            foreach (Button item in this.keys.Children)
            {
                if (e.Key.ToString() == "LeftShift" && item.Content.ToString().Length == 1)
                    item.Content = item.Content.ToString().ToUpper();
                if (keyconv.ConvertToString(e.Key) == item.Content.ToString().ToUpper())
                {
                    item.Opacity=0.5;
                }

            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            foreach (Button item in this.keys.Children)
            {
                if (e.Key.ToString() == "LeftShift" && item.Content.ToString().Length == 1)
                    item.Content = item.Content.ToString().ToLower();
                if (keyconv.ConvertToString(e.Key) == item.Content.ToString().ToUpper())
                {
                    item.Opacity = 100;
                }
            }
        }
    }
}
