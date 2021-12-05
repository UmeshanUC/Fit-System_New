using FITSystem.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FITSystem.ViewModels
{
    public class Person_VM
    {
        private string nIC;
        private string name;
        private int workRoleID;
        private string email;
        private string gender;
        private string address;
        private int telephone;
        private DateTime joinedDate;

        public Person_VM()
        {
            FitDb db = new FitDb();
        }
        public string NIC { get => nIC; set => nIC = value; }
        public string Name { get => name; set => name = value; }
        public int WorkRoleID { get => workRoleID; set => workRoleID = value; }
        public string Email { get => email; set => email = value; }
        public string Gender { get => gender; set => gender = value; }
        public string Address { get => address; set => address = value; }
        public DateTime JoinedDate { get => joinedDate; set => joinedDate = value; }
        public int Telephone { get => telephone; set => telephone = value; }
    }
}
