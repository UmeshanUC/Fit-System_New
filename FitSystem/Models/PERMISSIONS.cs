using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FITSystem.Models
{
    public class Permissions
    {
        [Key][DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PermID { get; set; }
        public string PermName { get; set; }
        public string Description { get; set; }
    }
}
