using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FITSystem.Classes.Models
{
    interface IPerson
    {
        int Id { get; set; }
        string Name { get; set; }
        string Role { get; set; }
        string Email { get; set; }
        string Telephone { get; set; }
        string Gender { get; set; }
        string Address { get; set; }
        string ProfPic { get; set; }
        string BankAccount { get; set; }
        DateTime JoinedDate { get; set; }
    }
}
