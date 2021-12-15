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
    /// Interaction logic for FAQ_window.xaml
    /// </summary>
    public partial class FAQ_window : Window
    {
        public FAQ_window()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Contact_foarm win = new Contact_foarm();
            win.Show();
        }

        public static implicit operator FAQ_window(Memberdetail v)
        {
            throw new NotImplementedException();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FAQAnswer_window answer = new FAQAnswer_window();
            answer.Show();
            this.Close();
        }
    }
}
