using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitSystem.Models
{
    public class Login
    {
        [Key][DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string NIC { get; set; }
        [Required]
        public string Username{ get; set; } 
        [Required]
        public string Passwd { get; set; }
        [Required][ForeignKey("Permissions")]
        public int PermissionLevel { get; set; }

        #region NavigationProps
        public virtual Person Person { get; set; }
        public virtual Permissions Permissions{ get; set; }
        #endregion
    }
}
