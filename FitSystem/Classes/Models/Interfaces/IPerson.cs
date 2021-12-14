using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FitSystem.Classes.Models
{
    interface IPerson
    {
        string NIC { get; set; }
        string Name { get; set; }
        int WorkRoleID { get; set; }
        string Email { get; set; }
        string Gender { get; set; }
        string Address { get; set; }
        DateTime JoinedDate { get; set; }
        string Telephone { get; set; }
        byte[] Pic { get; set; }
        bool TodayPresence { get; set; }
        BitmapImage PicImage { get; set; }

    }
}
