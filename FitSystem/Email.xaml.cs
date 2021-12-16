using FitSystem.Classes;
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

namespace FitSystem
{
    /// <summary>
    /// Interaction logic for Email.xaml
    /// </summary>
    public partial class Email : Window
    {
        private readonly string recipient;

        public Email()
        {
            InitializeComponent();
        }

        public Email(string recipient)
        {
            InitializeComponent();
            this.recipient = recipient;
            EmailCtx emailCtx = new EmailCtx()
            {
                Recipient = recipient
            };
            DataContext = emailCtx;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EmailCtx emailCtx = DataContext as EmailCtx;
            EmailService emailService = new EmailService(emailCtx.Body, emailCtx.Recipient, emailCtx.Subject);
            emailService.Send();
        }

        private void FitTitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
