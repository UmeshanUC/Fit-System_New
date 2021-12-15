using System.Linq;
using System.Windows;
using FitSystem;
using FitSystem.Classes;
using FitSystem.Database;

namespace FITSystem
{
    /// <summary>
    /// Interaction logic for Memberdetail.xaml
    /// </summary>
    public partial class Memberdetail : Window
    {
        public Memberdetail()
        {
            InitializeComponent();
            getmember();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void getmember()
        {
            FitDb db = new FitDb();
            var record = db.PersonSet.Where(a=>a.NIC==Global.logusernic).FirstOrDefault();
            if(record is null)
            {
                ErrorService.ShowError("No record found");
                return;
            }
            text_address.Text = record.Address;
            text_name.Text = record.Name;
            text_mail.Text = record.Email;
            text_nic.Text = record.NIC;
            text_gender.Text = record.Gender;
            text_tp.Text = record.Telephone;



        }

        private void text_nic_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}
