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
            Memberdetail detail = new Memberdetail();
            detail.Show();
            
        }

        private void frmDashWorkspace_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }

        private void faq_button_Click(object sender, RoutedEventArgs e)
        {
            FAQ_window mem = new FAQ_window();
            mem.ShowDialog();
        }

        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
