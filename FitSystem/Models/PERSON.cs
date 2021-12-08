using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitSystem.Models
{
    public class Person
    {
        public Person()
        {
            JoinedDate = DateTime.Today;
        }
        [Key]
        public string NIC { get; set; }
        public string Name { get; set; }
        [ForeignKey("Permissions")]
        public int WorkRoleID { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public DateTime JoinedDate { get; set; }
        public int Telephone { get; set; }
        public string Pic { get; set; } //datatype ?
        public bool TodayPresence { get; set; }


        #region NavigationProps
        public virtual Permissions Permissions{ get; set; }
        public virtual Login Login { get; set; }
        public virtual Member Member{ get; set; }
        public virtual Employee Employee{ get; set; }
        #endregion

    }
}
