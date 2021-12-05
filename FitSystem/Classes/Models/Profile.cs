using FITSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FITSystem.Classes.Models
{
    class Profile : IPerson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public Package Package { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string ProfPic { get; set; }
        public string BankAccount { get; set; }
        public DateTime JoinedDate { get; set; }
    }
}
