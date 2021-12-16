using FitSystem.Classes;
using FitSystem.Classes.Models;
using FitSystem.Classes.Models.FilterModels;
using FitSystem.Database;
using FitSystem.Models;
using Microsoft.Win32;
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
    /// Interaction logic for AddPerson.xaml
    /// </summary>
    public partial class AddPerson : Window
    {
        private readonly bool isEditMode = false;
        private readonly string nIC;
        private readonly Person person;

        public delegate void refreshCallback();

        #region Constructors
        public AddPerson()
        {
            InitializeComponent();
            person = new Person();
            DataContext = person;
            ucEmpSection.DataContext = new Employee();
            ucMemberSection.DataContext = new Member();
        }

        public AddPerson(string NIC)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(NIC))
            {
                ErrorService.ShowError("Error in NIC before editing data");
                Close();
            }
            else
            {
                person = new Person();
                DataContext = person;
                ucEmpSection.DataContext = CreateEmployeeCtx(NIC);
                ucMemberSection.DataContext = CreateMemberCtx(NIC);

                isEditMode = true;
                nIC = NIC;
                Person loadedPerson = LoadEditForm(NIC);
                if (loadedPerson is null)
                {
                    loadedPerson = new Person();
                    MessageBox.Show("Error in Loading Data");
                }
                loadedPerson.PicImage = Global.ByteArrayToBitmapImage(loadedPerson.Pic);
                DataContext = loadedPerson;
                imgPic.Source = loadedPerson.PicImage;
            }
        }
        #endregion

        #region Methods
        private Person LoadEditForm(string nic)
        {
            using (FitDb db = new FitDb())
            {
                var selectedPersonList = db.PersonSet.Find(nic);
                return selectedPersonList;
            }
        }

        private Employee CreateEmployeeCtx(string NIC)
        {
            using (FitDb db = new FitDb())
            {
                var employeeRecord = db.EmployeeSet.Find(NIC);
                return employeeRecord;
            }

        }

        private Member CreateMemberCtx(string NIC)
        {
            using (FitDb db = new FitDb())
            {
                var memberRecord = db.MemberSet.Find(NIC);
                return memberRecord;
            }

        }

        private void AddNewPerson()
        {
            using (FitDb db = new FitDb())
            {
                try
                {
                    db.PersonSet.Add(DataContext as Person);
                    db.SaveChanges();

                    AddMemberAndEmpRecords((DataContext as Person).NIC);

                    Person person = DataContext as Person;
                    CreateLogin(person.NIC, person.WorkRoleID, txtUsename.Text, txtPassword.Text);

                    Global.RefreshDataGlobally();
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occured during updating record\nDetail View:\n" + ErrorService.MineErrorMsg(ex));
                }
            }
        }

        private void CreateLogin(string NIC, int workRoleID, string username, string pword)
        {
            Login newLogin = new Login
            {
                NIC = NIC,
                Username = username,
                Passwd = pword,
                PermissionLevel = workRoleID == 2 ? 2 : 1,

            };
            using (FitDb db = new FitDb())
            {
                try
                {
                    db.LoginSet.Add(newLogin);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    ErrorService.ShowError("Error in creating logins");
                }
            }
        }

        private bool AddMemberAndEmpRecords(string NIC)
        {
            if (string.IsNullOrEmpty(NIC))
            {
                ErrorService.ShowError("Valid NIC should be provided");
                return false;
            }

            using (FitDb db = new FitDb())
            {
                switch ((DataContext as Person).WorkRoleID)
                {
                    case 0:
                    case 1:
                        Employee empRecord = ucEmpSection.DataContext as Employee;
                        empRecord.NIC = NIC;
                        db.EmployeeSet.Add(empRecord);
                        break;

                    case 2:
                        Member memberRecord = ucMemberSection.DataContext as Member;
                        memberRecord.NIC = NIC;
                        db.MemberSet.Add(memberRecord);
                        break;
                    default:
                        break;
                }
                try
                {
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    ErrorService.ShowError("Error in saving Employee or Member records\n" + ErrorService.MineErrorMsg(ex));
                    return false;
                }

            }


        }
        private void UpdatePerson()
        {
            using (FitDb db = new FitDb())
            {
                Person editedPerson = (DataContext as Person);
                if (editedPerson is null)
                {
                    ErrorService.ShowError("Error in loading edited data.");
                    return;
                }
                Person record = db.PersonSet.Find(nIC);
                if (record is null)
                {
                    MessageBox.Show("Some error occured in loading the record for edit");
                }
                db.PersonSet.Attach(record);

                record.NIC = (DataContext as Person).NIC;
                record.Name = (DataContext as Person).Name;
                record.Email = (DataContext as Person).Email;
                record.Address = (DataContext as Person).Address;
                record.JoinedDate = (DataContext as Person).JoinedDate;
                record.Telephone = (DataContext as Person).Telephone;
                record.Gender = (DataContext as Person).Gender;
                record.Pic = (DataContext as Person).Pic;

                #region NotImplemented(No Time)
                // If the workrole was changed, the record should be moved to corresponding table(Employee/Member)
                //if (record.WorkRoleID != editedPerson.WorkRoleID)
                //{
                //    if (record.WorkRoleID == 2)
                //    {
                //        try
                //        {
                //            var temp = db.MemberSet.Find(nIC);
                //            db.MemberSet.Attach(temp);
                //            db.MemberSet.Remove(temp);
                //            db.SaveChanges();
                //        }
                //        catch (Exception ex)
                //        {
                //            ErrorService.ShowError("Error while removed before inserting in new table\n" + ErrorService.MineErrorMsg(ex));
                //            return;
                //        }
                //    }
                //}
                #endregion

                try
                {
                    db.SaveChanges();

                    if (editedPerson.WorkRoleID == 2)
                    {
                        UpdateMemberDetails(nIC);
                    }
                    else
                    {
                        UpdateEmpDetails(nIC);
                    }

                    Global.RefreshDataGlobally();
                    Close();

                }
                catch (Exception ex)
                {
                    ErrorService.ShowError("Error occured during updating record\nDetail View:\n" + ErrorService.MineErrorMsg(ex));
                }
            }
        }

        private void UpdateEmpDetails(string nIC)
        {
            using (FitDb db = new FitDb())
            {
                Employee employeeRecord = db.EmployeeSet.Find(nIC);
                Employee editedEmp = (ucEmpSection.DataContext as Employee);

                if (editedEmp is null || employeeRecord is null)
                {
                    ErrorService.ShowError("Error while loading data");
                    return;
                }

                try
                {
                    db.EmployeeSet.Attach(employeeRecord);

                    employeeRecord.BankAccount = editedEmp.BankAccount;
                    employeeRecord.BaseSalary = editedEmp.BaseSalary;
                    employeeRecord.Bonus = editedEmp.Bonus;
                    employeeRecord.EpfNo = editedEmp.EpfNo;
                    employeeRecord.Deduction = editedEmp.Deduction;
                    employeeRecord.NetSalary = editedEmp.NetSalary;
                    employeeRecord.OtherFacts = editedEmp.OtherFacts;

                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    ErrorService.ShowError("Error in saving Employee Data\n" + ErrorService.MineErrorMsg(ex));
                }

            }
        }

        private void UpdateMemberDetails(string nIC)
        {
            using (FitDb db = new FitDb())
            {
                Member memberRecord = db.MemberSet.Find(nIC);
                Member editedMember = ucMemberSection.DataContext as Member;

                if (editedMember is null || memberRecord is null)
                {
                    ErrorService.ShowError("Error while loading data");
                    return;
                }


                try
                {
                    db.MemberSet.Attach(memberRecord);
                    memberRecord.Height = editedMember.Height;
                    memberRecord.Weight = editedMember.Weight;
                    memberRecord.Package = editedMember.Package;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    ErrorService.ShowError("Error in saving Member Data\n" + ErrorService.MineErrorMsg(ex));
                }
            }
        }

        private void UnsetPackegeBtns()
        {
            var ButtonsAsQuay = gridPackage.Children.OfType<Button>();
            if (ButtonsAsQuay == null)
            {
                ErrorService.ShowError("Error occured in UnSetting Styles of Package Buttons");
                return;
            }

            ButtonsAsQuay.ToList().ForEach(btn => btn.ClearValue(FrameworkElement.StyleProperty)); ;
        }

        private void SetPackage(object sender)
        {
            Button button = sender as Button;
            UnsetPackegeBtns();
            button.Style = Global.GetAppStyle("DarkButtonClicked");
            (ucMemberSection.DataContext as Member).Package = button.Content as string;
        }
        #endregion


        private void FitTitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (person is null) return;

            Email email = new Email(person.Email);
            email.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (isEditMode) UpdatePerson(); else AddNewPerson();
        }



        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Person dataContextAsPerson = DataContext as Person;
            string imagePath = Global.OpenImageFile();
            if (imagePath is null)
            {
                ErrorService.ShowError("File path error.\nPath was null");
                return;
            }
            byte[] ImageBArray = Global.FileToByteArray(imagePath);
            if (ImageBArray is null)
            {
                ErrorService.ShowError("Error in serialization");
            }

            dataContextAsPerson.Pic = ImageBArray;

            dataContextAsPerson.PicImage = Global.ByteArrayToBitmapImage(ImageBArray);
            imgPic.Source = dataContextAsPerson.PicImage;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SetPackage(sender);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            SetPackage(sender);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            SetPackage(sender);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
