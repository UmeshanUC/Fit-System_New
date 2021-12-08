using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitSystem.Models
{
    public class Member
    {
        [Key][ForeignKey("Person")]
        public string NIC { get; set; }
        public float Weight { get; set; }
        public float Height { get; set; }
        public string Package { set; get; }

        #region NavigationProps
        public Person Person { get; set; }
        #endregion
    }
}
