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
using System.Windows.Shapes;

namespace FITSystem
{
    /// <summary>
    /// Interaction logic for Member_payment.xaml
    /// </summary>
    public partial class Member_payment : Window
    {
        public Member_payment()
        {
            InitializeComponent();
        }

        private void buy_2week_Click(object sender, RoutedEventArgs e)
        {
            Buywindow win = new Buywindow();
            win.Show();
        }

        private void buy_1month_Click(object sender, RoutedEventArgs e)
        {
            Buywindow win = new Buywindow();
            win.Show();
        }

        private void but_6month_Click(object sender, RoutedEventArgs e)
        {
            Buywindow win = new Buywindow();
            win.Show();
        }

        private void FitTitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
