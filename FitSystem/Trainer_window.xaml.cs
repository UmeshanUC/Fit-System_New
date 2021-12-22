using System.Linq;
using System.Windows;
using FitSystem.Classes;
using FitSystem.Database;

namespace FITSystem
{
    /// <summary>
    /// Interaction logic for Trainer_window.xaml
    /// </summary>
    public partial class Trainer_window : Window
    {
        public Trainer_window()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            getmember();

        }

        private void getmember()
        {
            FitDb db = new FitDb();
            var record = db.PersonSet.Where(a => a.WorkRoleID == 2).Where(a=>a.NIC==text_nic.Text).FirstOrDefault();
            if (record is null)
            {
                ErrorService.ShowError("No record found");
                return;
            }
            text_address.Text = record.Address;
            text_name.Text = record.Name;
            text_email.Text = record.Email;
            text_nic.Text = record.NIC;
            text_gender.Text = record.Gender;
            text_tp.Text = record.Telephone;


            
        }

        private void FitTitleBar_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
