using FitSystem.Classes;
using FitSystem.Classes.Models.FilterModels;
using FitSystem.Database;
using FitSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace FitSystem.FITPages
{
    /// <summary>
    /// Interaction logic for Dash_Home.xaml
    /// </summary>
    public partial class Dash_Home : Page
    {
        public Dash_Home()
        {
            InitializeComponent();
            DataContext = CreateDash_HomeCtx();
            Global.HomePageRefreshCallBack = RefreshData;
        }

        private void RefreshData()
        {
            DataContext = CreateDash_HomeCtx();
        }

        private Dash_HomeCtx CreateDash_HomeCtx()
        {
            Dash_HomeCtx dash_HomeCtx;
            using (FitDb db = new FitDb())
            {
                var allPersonList = db.PersonSet.ToList();
                try
                {
                    dash_HomeCtx = new Dash_HomeCtx()
                    {
                        MembersToday = allPersonList.Where(p => p.WorkRoleID == 2).Count(p => p.TodayPresence),
                        MembersTotal = allPersonList.Where(p => p.WorkRoleID == 2).Count(),
                        StaffToday = allPersonList.Where(p => p.WorkRoleID != 2).Count(p => p.TodayPresence),
                        StaffTotal = allPersonList.Where(p => p.WorkRoleID != 2).Count()
                    };
                    return dash_HomeCtx;
                }
                catch
                {
                    return null;
                }

            }
        }

        private List<PersonForDashHome> SearchMembers(string searchKey)
        {
            if (string.IsNullOrEmpty(searchKey)) return null;

            using (FitDb db = new FitDb())
            {
                List<PersonForDashHome> personList = db.PersonSet.Where(p => p.WorkRoleID == 2).Where(p => p.NIC.Contains(searchKey) || p.Name.Contains(searchKey))
                    .Select(p => new PersonForDashHome() { Name = p.Name, NIC = p.NIC, TodayPresence = p.TodayPresence }).ToList();

                if (personList is null || personList.Count() == 0) return null;

                return personList;
            }
        }

        private List<PersonForDashHome> SearchStaff(string searchKey)
        {
            if (string.IsNullOrEmpty(searchKey)) return null;

            using (FitDb db = new FitDb())
            {
                List<PersonForDashHome> personList = db.PersonSet.Where(p => p.WorkRoleID != 2).Where(p => p.NIC.Contains(searchKey) || p.Name.Contains(searchKey))
                    .Select(p => new PersonForDashHome() { Name = p.Name, NIC = p.NIC, TodayPresence = p.TodayPresence }).ToList();

                if (personList is null || personList.Count() == 0) return null;

                return personList;
            }
        }

        private bool UpdatePerson(PersonForDashHome personForDashHome)
        {
            if (personForDashHome is null)
            {
                ErrorService.ShowError("No record to update. Error");
                return false;
            }

            using (FitDb db = new FitDb())
            {
                var record = db.PersonSet.Find(personForDashHome.NIC);

                try
                {
                    db.PersonSet.Attach(record);
                    record.TodayPresence = personForDashHome.TodayPresence;
                    db.SaveChanges();
                    Global.RefreshDataGlobally();
                    return true;

                }
                catch
                {
                    ErrorService.ShowError("Error in updating attendance");
                    return false;
                }

            }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            Global.HomePageRefreshCallBack = null;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            dGridMembers.ItemsSource = null;
            var result = SearchMembers(((TextBox)sender).Text);
            dGridMembers.ItemsSource = result;
            if (!(result is null))
            {
                foreach (var col in dGridMembers.Columns)
                {
                    col.IsReadOnly = true;
                }
            }
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            dGridStaff.ItemsSource = null;
            var result = SearchStaff(((TextBox)sender).Text);
            dGridStaff.ItemsSource = result;
            if (!(result is null))
            {
                foreach (var col in dGridStaff.Columns)
                {
                    col.IsReadOnly = true;
                }
            }
        }
        private void MarkAttendance(object sender)
        {
            PersonForDashHome personForDashHome = (sender as DataGrid).SelectedItem as PersonForDashHome;

            if (personForDashHome is null)
            {
                ErrorService.ShowError("Not Person Selected");
                return;
            }

            //Toggling Attendance
            personForDashHome.TodayPresence = !personForDashHome.TodayPresence;

            if (!UpdateDGrid(sender, personForDashHome))
            {
                ErrorService.ShowError("Error updating DataGrid");
                return;
            }

            UpdatePerson(personForDashHome);
        }

        private bool UpdateDGrid(object DGridAsObject, PersonForDashHome newRecord)
        {
            DataGrid dataGrid = DGridAsObject as DataGrid;

            List<PersonForDashHome> personForDashHomeList = dataGrid?.ItemsSource as List<PersonForDashHome>;

            // Replacing New record
            personForDashHomeList?.Where(old => old.NIC != newRecord.NIC)?.ToList()?.Add(newRecord);

            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = personForDashHomeList;

            return !(personForDashHomeList is null);
        }

        private void dGridMembers_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MarkAttendance(sender);

        }


        private void dGridStaff_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MarkAttendance(sender);
        }
    }
}
