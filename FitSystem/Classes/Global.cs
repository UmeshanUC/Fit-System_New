using FitSystem.Classes;
using FitSystem.Database;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace FitSystem
{
    public static class Global
    {
        public static string logusernic = "079876567v";

        #region Callback Funtions
        public delegate void RefreshCallBack();

        public static RefreshCallBack StaffPageRefreshCallBack;
        public static RefreshCallBack HomePageRefreshCallBack;
        public static RefreshCallBack ManageStaffAndMembersRefreshCallBack;
        #endregion

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

        public static string GetCurrentDir()
        {
            return Directory.GetCurrentDirectory();
        }

        /// <summary>
        /// Opens a image filtering OpenFileDialog Box
        /// </summary>
        /// <returns>Filepath as a string</returns>
        public static string OpenImageFile()
        {
            OpenFileDialog oFileD = new OpenFileDialog();
            oFileD.Title = "Select a picture";
            oFileD.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "Portable Network Graphic (*.png)|*.png";
            if (oFileD.ShowDialog() == true)
            {
                return oFileD.FileName;
            }
            return null;
        }
        public static byte[] FileToByteArray(string filePath)
        {
            try
            {
                byte[] bArray = File.ReadAllBytes(filePath);
                return bArray;
            }
            catch (Exception ex)
            {
                ErrorService.ShowError("Error in loading Image\n" + ErrorService.MineErrorMsg(ex));
            }
            return null;
        }

        public static BitmapImage ByteArrayToBitmapImage(byte[] bytes)
        {
            if (bytes is null || bytes.Count() == 0) return null;
            BitmapImage image = null;
            MemoryStream stream = null;
            try
            {
                stream = new MemoryStream(bytes);
                stream.Seek(0, SeekOrigin.Begin);
                Image img = Image.FromStream(stream);
                image = new BitmapImage();
                image.BeginInit();
                MemoryStream ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);
                image.StreamSource = ms;
                image.StreamSource.Seek(0, SeekOrigin.Begin);
                image.EndInit();
            }
            catch (Exception ex)
            {
                ErrorService.ShowError("Error Occurred in image loading.\n" + ErrorService.MineErrorMsg(ex));
                return null;
            }
            finally
            {
                stream.Close();
                stream.Dispose();
            }
            return image;
        }

        public static Style GetAppStyle(string styleKey)
        {
            if (string.IsNullOrEmpty(styleKey))
            {
                ErrorService.ShowError("StyleKey should Not be Empty");
                return null;
            }
            return App.Current.Resources[styleKey] as Style;
        }


        /// <summary>
        /// Refresh the data globally. Can be used after a CRUD to update the UI
        /// </summary>
        public static void RefreshDataGlobally()
        {
            StaffPageRefreshCallBack?.Invoke();
            HomePageRefreshCallBack?.Invoke();
            ManageStaffAndMembersRefreshCallBack?.Invoke();
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
