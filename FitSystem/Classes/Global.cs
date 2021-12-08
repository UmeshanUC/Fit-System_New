using FitSystem.Classes;
using FitSystem.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitSystem
{
    public static class Global
    {
        #region Globals
        internal static string LoggedUserNIC = null;
        internal static DateTime LoggedTs;
        internal static bool IsUserLoggedIn = false;

        /// <summary>
        /// Navigate to relevant window and Closes the current Window
        /// </summary>
        /// <typeparam name="TWin">Window to navigate</typeparam>
        /// <param name="win">Current Window object to be closed</param>
        public static void Navigate<TWin>(Window win = null) where TWin : Window, new()
        {
            //if(win is null)
            //{
            //    ErrorService.ShowError("Error occured. Navigation aborting");
            //    return;
            //}
            TWin dashB = new TWin();
            dashB.Show();
            win?.Close();
        }

        private static void LogLogout()
        {
            using (FitDb db = new FitDb())
            {
                var record = db.LoginLogSet.Where(r => r.NIC == LoggedUserNIC).Where(r => r.LoggedTs.Equals(LoggedTs)).FirstOrDefault();
                if (record is null)
                {
                    ErrorService.ShowError("LogoutLog Error");
                    return;
                }
                record.LogOutTs = DateTime.Now;
                db.LoginLogSet.Attach(record);
                db.Entry(record).Property(p => p.LogOutTs).IsModified = true;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    ErrorService.ShowError("Loggout Log Save error\n" + ErrorService.MineErrorMsg(ex));
                }
            }

        }

        public static void UserLogout()
        {
            IsUserLoggedIn = false;
            LoggedUserNIC = null;


            var openedWin = Application.Current.Windows.OfType<Window>().ToList();
            Navigate<UserLogin>(null);
            openedWin.ForEach(win => win.Close());
        }
        #endregion

        #region Managing Staff and Person

        internal static string ManagingPersonType = null;
        internal static int ManagingWorkRoleID = -1;

        /// <summary>
        /// Setting the coupled values ManagingPersonType and ManagingWorkRole together
        /// </summary>
        /// <param name="personType"></param>
        static public void setManagingPersonType(string personType)
        {
            ManagingPersonType = personType;
            ManagingWorkRoleID = PersonTypeToWorkRoleID(personType);
        }

        /// <summary>
        /// Setting the coupled values ManagingPersonType and ManagingWorkRole together
        /// </summary>
        /// <param name="workRoleID"></param>
        static public void setManagingPersonType(int workRoleID)
        {
            ManagingWorkRoleID = workRoleID;
            ManagingPersonType = WorkRoleIdtoPersonType(workRoleID);
        }

        /// <summary>
        /// Value conversion from PersonType to WorkRoleID
        /// </summary>
        /// <param name="personType"></param>
        /// <returns></returns>
        static public int PersonTypeToWorkRoleID(string personType)
        {
            int TempWorkRoleID = -1;
            switch (personType)
            {
                case "Medical Officers":
                    TempWorkRoleID = 0;
                    break;
                case "Trainers":
                    TempWorkRoleID = 1;
                    break;
                case "Members":
                    TempWorkRoleID = 2;
                    break;
                case "Other Staff":
                    TempWorkRoleID = 3;
                    break;
            }
            if (TempWorkRoleID == -1)
            {
                MessageBox.Show("Error in PersonType.");
                return -1;
            }
            else
            {
                return TempWorkRoleID;
            }
        }

        /// <summary>
        /// Value conversion from WorkRoleID to PersonType
        /// </summary>
        /// <param name="workRoleID"></param>
        /// <returns></returns>
        static public string WorkRoleIdtoPersonType(int workRoleID)
        {
            string tempPersonType = null;
            switch (workRoleID)
            {
                case 0:
                    tempPersonType = "Medical Officers";
                    break;
                case 1:
                    tempPersonType = "Trainers";
                    break;
                case 2:
                    tempPersonType = "Members";
                    break;
                case 3:
                    tempPersonType = "Other Staff";
                    break;
            }
            if (tempPersonType == null)
            {
                MessageBox.Show("Error in PersonType.");
                return null;
            }
            else
            {
                return tempPersonType;
            }
        }
        #endregion


    }
}
