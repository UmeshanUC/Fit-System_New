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
    /// Interaction logic for memberdashboard.xaml
    /// </summary>
    public partial class memberdashboard : Window
    {
        public memberdashboard()
        {
            InitializeComponent();
        }

        private void SetNavBtnClickedStyle(object sender, RoutedEventArgs e)
        {
            Memberdetail mem = new Memberdetail();
            mem.ShowDialog();
        }

        private void frmDashWorkspace_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }
    }
}
