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
using FitSystem;

namespace FITSystem
{
    /// <summary>
    /// Interaction logic for front_page.xaml
    /// </summary>
    public partial class front_page : Window
    {
        public front_page()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Guest_register win = new Guest_register();
            win.Show();
        }

        private void FitTitleBar_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UserLogin win = new UserLogin();
            win.Show();
        }
    }
}
