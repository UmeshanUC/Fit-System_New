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
    /// Interaction logic for ManageStaffAndMembers.xaml
    /// </summary>
    public partial class ManageStaffAndMembers : Window
    {
        private readonly string personType;
        private readonly object recipient;

        public ManageStaffAndMembers()
        {
            InitializeComponent();
            LoadGrid(Global.ManagingWorkRoleID);
        }

        #region Methods
        private void LoadGrid(int workRoleID)
        {
            using (FitDb db = new FitDb())
            {
                var selectedPersonList = db.PersonSet.Where(p => p.WorkRoleID == workRoleID).ToList();
                DGridPerson.ItemsSource = selectedPersonList;
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

        private Person GetSelectedGridItem(object sender)
        {
            DataGrid grid = sender as DataGrid;
            Person person = grid.SelectedItem as Person;
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
            AddPerson addPerson = new AddPerson(RefreshData);
            addPerson.ShowDialog();
        }

        private void DGridPerson_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Person person = GetSelectedGridItem(sender);
            if (person is null) return;

            AddPerson addPerson = new AddPerson(person.NIC, RefreshData);
            addPerson.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DGridPerson_MouseDoubleClick(DGridPerson as object, null);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Person person = GetSelectedGridItem(DGridPerson as object);
            if (person is null) return;

            Email email = new Email(person.Email);
            email.ShowDialog();


        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Person person = GetSelectedGridItem(DGridPerson as object);
            if (person?.NIC is null)
            {
                ErrorService.ShowError("Error occured when deleting. Not deleted !");
            }
            RemovePerson(person.NIC);
            LoadGrid(Global.ManagingWorkRoleID);
        }
    }
}
