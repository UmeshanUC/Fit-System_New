using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitSystem.Classes
{
    static class ErrorService
    {
        public static void ShowError(string msg, string caption = "Warning !")
        {
            MessageBox.Show(msg, caption == null ? "FIT | Message" : caption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static string MineErrorMsg(Exception ex)
        {
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            if (ex is null) return "Null";
            return ex.Message;
        }
    }
}
