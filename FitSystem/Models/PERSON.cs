using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

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
        [ForeignKey("WorkRoles")]
        public int WorkRoleID { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public DateTime JoinedDate { get; set; }
        public string Telephone { get; set; }
        public byte[] Pic { get; set; }
        public bool TodayPresence { get; set; }

        #region NotMapped
        [NotMapped]
        public BitmapImage PicImage { get; set; }
        #endregion



        #region NavigationProps
        public virtual WorkRole WorkRoles{ get; set; }
        public virtual Login Login { get; set; }
        public virtual Member Member{ get; set; }
        public virtual Employee Employee{ get; set; }
        #endregion

    }
}
