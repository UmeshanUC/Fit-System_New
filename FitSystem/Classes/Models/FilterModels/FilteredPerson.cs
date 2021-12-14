using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FitSystem.Classes.Models.FilterModels
{
    public class FilteredPerson
    {
        public string NIC { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public DateTime JoinedDate { get; set; }
        public string Telephone { get; set; }
        public bool TodayPresence { get; set; }
    }
}
