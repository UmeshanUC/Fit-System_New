using FitSystem.Classes;
using FitSystem.Classes.Models.FilterModels;
using FitSystem.Database;
using FitSystem.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for ManageStaffAndMembers.xaml
    /// </summary>
    public partial class ManageStaffAndMembers : Window
    {

        public ManageStaffAndMembers()
        {
            InitializeComponent();
            LoadGrid(Global.ManagingWorkRoleID);
            Global.ManageStaffAndMembersRefreshCallBack = RefreshData;
        }

        #region Methods
        private void LoadGrid(int workRoleID)
        {
            using (FitDb db = new FitDb())
            {
                var selectedPersons = db.PersonSet.Include("WorkRoles").Where(p => p.WorkRoleID == workRoleID);
                List<FilteredPerson> filteredPersons = FilterPersonDetails(selectedPersons);
                DGridPerson.ItemsSource = filteredPersons;
            }
        }

        private void RemovePerson(string nic)
        {
            using (FitDb db = new FitDb())
            {
                var record = db.PersonSet.Find(nic);
                if (record is null)
                {
                    ErrorService.ShowError("Error Occured in deleting", "Warning !");
                }
                try
                {
                    db.PersonSet.Remove(record);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    ErrorService.ShowError("Error occured. Not deleted ! \n" + ErrorService.MineErrorMsg(ex));
                }
            }

        }

        public List<FilteredPerson> FilterPersonDetails(IQueryable<Person> List)
        {
            var Filtrate = List.Select(a => new FilteredPerson()
            {
                NIC = a.NIC,
                Name = a.Name,
                Role = a.WorkRoles.RoleName,
                Address = a.Address,
                Email = a.Email,
                Gender = a.Gender,
                JoinedDate = a.JoinedDate,
                Telephone = a.Telephone,
                TodayPresence = a.TodayPresence,
            });
            return Filtrate.ToList();
        }

        private FilteredPerson GetSelectedGridItem(object sender)
        {
            DataGrid grid = sender as DataGrid;
            FilteredPerson person = grid?.SelectedItem as FilteredPerson;
            return person;
        }

        private void RefreshData()
        {
            LoadGrid(Global.ManagingWorkRoleID);
        }
        #endregion

        private void FitTitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddPerson addPerson = new AddPerson();
            addPerson.ShowDialog();
        }

        private void DGridPerson_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            FilteredPerson person = GetSelectedGridItem(sender);
            if (person is null) return;

            Profile profile = new Profile(person.NIC);
            profile.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FilteredPerson person = GetSelectedGridItem(DGridPerson as object);
            if (person is null) return;

            AddPerson addPerson = new AddPerson(person.NIC);
            addPerson.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            FilteredPerson person = GetSelectedGridItem(DGridPerson as object);
            if (person is null) return;

            Email email = new Email(person.Email);
            email.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            FilteredPerson person = GetSelectedGridItem(DGridPerson as object);
            if (person?.NIC is null)
            {
                ErrorService.ShowError("Error occured when deleting. Not deleted !");
            }
            RemovePerson(person.NIC);
            Global.RefreshDataGlobally();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Global.ManageStaffAndMembersRefreshCallBack = null;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
