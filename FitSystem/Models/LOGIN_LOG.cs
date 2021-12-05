using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FITSystem.Models
{
    public class LoginLog
    {
        [Key, Column(Order = 0)]
        public string NIC { get; set; }
        [Key, Column(Order = 1)]
        public DateTime LoggedTs { get; set; }
        public DateTime? LogOutTs { get; set; }
    }
}
