using FitSystem;
using FitSystem.Database;
using FitSystem.Models;
using FITSystem;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Interaction logic for UserLogin.xaml
    /// </summary>
    public partial class UserLogin : Window
    {
        public UserLogin()
        {
            InitializeComponent();
        }


        #region Methods
        public void LogUserLogin()
        {
            using (FitDb loginLogCtx = new FitDb())
            {
                LoginLog log = new LoginLog()
                {
                    NIC = txtUser.Text,
                    LoggedTs = DateTime.Now,
                };
                Global.LoggedUserNIC = txtUser.Text;
                Global.LoggedTs = log.LoggedTs;
                Global.IsUserLoggedIn = true;

                loginLogCtx.LoginLogSet.Add(log);
                loginLogCtx.SaveChanges();
            }
        }

        public void Navigate<TWin>() where TWin : Window, new()
        {
            TWin dashB = new TWin();
            dashB.Show();
            this.Close();
        }

        public async Task<(bool, int)> ValidateAsync(string user, string passWd)
        {
            //Instantiating the DbConctext
            using (FitDb loginQuery = new FitDb())
            {
                var Tdata = await loginQuery.LoginSet.SingleOrDefaultAsync
                    (e => e.Username == user && e.Passwd == passWd);


                if (Tdata == null)
                {
                    MessageBox.Show("Invalid Username or Password");
                    //NotifyOverlay.Visibility = Visibility.Hidden;
                    return (false, -1);
                }

                return (true, Tdata.PermissionLevel);
            }
        }
        #endregion

        #region EventHandlers

        private void LblLogin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private async void BtnLogin_Click_1(object sender, RoutedEventArgs e)
        {
            var validationResult = await ValidateAsync(txtUser.Text, txtPassWd.Password);
            if (validationResult.Item1)
            {
                LogUserLogin();


                switch (validationResult.Item2)
                {
                    case 0:
                        Navigate<DashB>();
                        break;
                    case 1:
                        Navigate<Trainer_window>();
                        break;
                    case 2:
                        Navigate<memberdashboard>();
                        break;

                    default:
                        MessageBox.Show("Error in Access Level");
                        //NotifyOverlay.Visibility = Visibility.Hidden;
                        return;
                }

                this.Close();
            }

            ///Slowing down happens at the click to establish the connection. For Better speed the connction establishment could be done in the form load. Hence the lag become invisible to user
        }

        private void BtnExit_OnClick(object sender, RoutedEventArgs e)
        {
            //App.Current.Shutdown();
            Close();
        }

        #endregion
    }
}