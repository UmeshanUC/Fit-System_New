using System;
using System.Windows;
using FitSystem.Database;
using FitSystem.Models;

namespace FITSystem
{
    /// <summary>
    /// Interaction logic for Guest_register.xaml
    /// </summary>
    public partial class Guest_register : Window
    {
        public Guest_register()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            register();
            Member_payment win = new Member_payment();
            win.Show();
            this.Close();

        }

        public void register (){

            FitDb db = new FitDb();
            Person persn = new Person()
            {
                Name = text_name.Text,
                Email = text_email.Text,
                Gender = text_gender.Text,
                NIC = text_nic.Text,
                Telephone = text_tele.Text,
                Address = text_address.Text,
                Pic = null,
            };

            db.PersonSet.Add(persn);
            Login login = new Login()
            {
                NIC = text_nic.Text,
                Username = text_username.Text,
                Passwd = text_username.Text,
                PermissionLevel = 2,
            };

            db.LoginSet.Add(login);
            

            Member member = new Member()
            {
                NIC = text_nic.Text,
                Weight = decimal.Parse(text_weight.Text),
                Height = decimal.Parse(text_height.Text),
                Package = "basic"

            };

            db.MemberSet.Add(member);
            db.SaveChanges();

        }
    }
}
