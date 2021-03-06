using FitSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FitSystem.Classes.Models
{
    class Profile : IPerson
    {
        public string NIC { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public Package Package { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string BankAccount { get; set; }
        public DateTime JoinedDate { get; set; }
        public int WorkRoleID { get; set; }
        public byte[] Pic { get; set; }
        public bool TodayPresence { get; set; }
        public BitmapImage PicImage { get; set; }
    }
}
