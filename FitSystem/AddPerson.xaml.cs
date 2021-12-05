using FITSystem.Classes;
using FITSystem.Database;
using FITSystem.Models;
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

namespace FITSystem
{
    /// <summary>
    /// Interaction logic for AddPerson.xaml
    /// </summary>
    public partial class AddPerson : Window
    {
        private readonly bool isEditMode = false;
        private readonly string nIC;
        private readonly refreshCallback refreshCallback1;
        private readonly Person person;

        public delegate void refreshCallback();

        public AddPerson(refreshCallback refreshCallback)
        {
            InitializeComponent();
            person = new Person();
            DataContext = person;
            refreshCallback1 = refreshCallback;
        }

        public AddPerson(string NIC, refreshCallback refreshCallback)
        {
            InitializeComponent();

            if (!(NIC is null))
            {
                isEditMode = true;
                nIC = NIC;
                refreshCallback1 = refreshCallback;
                Person loadedPerson = LoadEditForm(NIC);
                if (loadedPerson is null)
                {
                    MessageBox.Show("Error in Loading Data");
                }
                DataContext = loadedPerson;
            }
        }

        #region Methods
        private Person LoadEditForm(string nic)
        {
            using (FitDb db = new FitDb())
            {
                var selectedPersonList = db.PersonSet.Where(p => p.NIC == nic).SingleOrDefault();
                return selectedPersonList;
            }
        }
        #endregion

        private void FitTitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show((DataContext as Person)?.Email);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (isEditMode) UpdatePerson(); else AddNewPerson();
        }

        private void AddNewPerson()
        {
            using (FitDb db = new FitDb())
            {
                //record.NIC = (DataContext as Person).NIC;
                //record.Name = (DataContext as Person).Name;
                //record.Email = (DataContext as Person).Email;
                //record.Address = (DataContext as Person).Address;
                //record.JoinedDate = (DataContext as Person).JoinedDate;
                //record.Telephone = (DataContext as Person).Telephone;
                //record.Pic = (DataContext as Person).NIC;

                try
                {
                    db.PersonSet.Add(DataContext as Person);
                    db.SaveChanges();
                    refreshCallback1();
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occured during updating record\nDetail View:\n" + ErrorService.MineErrorMsg(ex));
                }
            }
        }

        private void UpdatePerson()
        {
            using (FitDb db = new FitDb())
            {
                Person record = db.PersonSet.Find(nIC);
                if (record is null)
                {
                    MessageBox.Show("Some error occured in loading the record for edit");
                }
                record.NIC = (DataContext as Person).NIC;
                record.Name = (DataContext as Person).Name;
                record.Email = (DataContext as Person).Email;
                record.Address = (DataContext as Person).Address;
                record.JoinedDate = (DataContext as Person).JoinedDate;
                record.Telephone = (DataContext as Person).Telephone;
                record.Pic = (DataContext as Person).NIC;
                try
                {
                    db.SaveChanges();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occured during updating record\nDetail View:\n" + ex.Message);
                }
                refreshCallback1();
                Close();
            }
        }
    }
}
