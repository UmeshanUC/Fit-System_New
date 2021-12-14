using FitSystem.Classes;
using FitSystem.Database;
using FitSystem.Models;
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
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Window
    {
        private readonly Person person;

        public Profile(string NIC)
        {
            InitializeComponent();
            using (FitDb db = new FitDb())
            {
                Person person = db.PersonSet.Find(NIC);
                if (person is null)
                {
                    ErrorService.ShowError("Error occured in loading data");
                    Close();
                }
                this.person = person;
            }

            person.PicImage = Global.ByteArrayToBitmapImage(person.Pic);
            DataContext = person;
            SetSubDetails(person.NIC);
        }

        private void SetSubDetails(string NIC)
        {
            using (FitDb db = new FitDb())
            {
                switch (person.WorkRoleID)
                {
                    case 0:
                    case 1:
                        ucEmpSection.DataContext = db.EmployeeSet.Find(NIC);
                        break;
                    case 2:
                        var member = db.MemberSet.Find(NIC);
                        ucMemberSection.DataContext = member;
                        dockPackage.DataContext = GetPackageDetails(member.Package);
                        break;
                    case 3:
                        break;
                    default:
                        ErrorService.ShowError("Error in WorkRoleID while loading SubDetails of Person");
                        break;
                }
            }
        }

        private Package GetPackageDetails(string packageName)
        {
            Package package;
            using (FitDb db = new FitDb())
            {
                package = db.PackageSet.Find(packageName);
            }
            if (package is null)
            {
                ErrorService.ShowError("Error in loading Package");
                return null;
            }
            return package;
        }

        private void FitTitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (person is null) return;

            Email email = new Email(person.Email);
            email.ShowDialog();
        }
    }
}
