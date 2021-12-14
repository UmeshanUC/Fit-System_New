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

        private bool UpdatePerson(object affectedDGridObject)
        {
            var selectedRecord = (affectedDGridObject as DataGrid)?.SelectedItem as PersonForDashHome;
            string recordNIC = selectedRecord?.NIC;
            if (recordNIC is null)
            {
                ErrorService.ShowError("No NIC error");
                return false;
            }
            using (FitDb db = new FitDb())
            {
                var record = db.PersonSet.Find(recordNIC);

                try
                {
                    db.PersonSet.Attach(record);
                    record.TodayPresence = selectedRecord.TodayPresence;
                    db.SaveChanges();
                    return true;

                }
                catch (Exception ex)
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
                    if (col.DisplayIndex != 2) col.IsReadOnly = true;
                }
            }
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            dGridMembers.ItemsSource = null;
            var result = SearchStaff(((TextBox)sender).Text);
            dGridMembers.ItemsSource = result;
            if (!(result is null))
            {
                foreach (var col in dGridStaff.Columns)
                {
                    if (col.DisplayIndex != 2) col.IsReadOnly = true;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void dGridMembers_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            UpdatePerson(sender);

        }

        private void dGridStaff_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            UpdatePerson(sender);
        }
    }
}
