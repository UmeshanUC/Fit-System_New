using FITSystem.Classes;
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

namespace FITSystem.FITControls
{
    /// <summary>
    /// Interaction logic for DetailCard.xaml
    /// </summary>
    public partial class DetailCard : UserControl
    {
        public DetailCard()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string personType = (DataContext as DetailCardCtx)?.ManageType;
            if (personType == null)
            {
                MessageBox.Show("Error in Setting Managing PersonType.");
                return;
            }
            Global.setManagingPersonType(personType);
            ManageStaffAndMembers manageStaffAndMembers = new ManageStaffAndMembers();
            manageStaffAndMembers.ShowDialog();
            //Window.GetWindow(this)?.Close();
        }
    }
}
