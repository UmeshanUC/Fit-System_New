using FitSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitSystem.Classes
{
    public class Dash_HomeCtx
    {
        //Members
        public int MembersToday { get; set; }
        public int MembersTotal { get; set; }
        public List<Person> MemberSearchDGrid { get; set; }

        //Staff
        public int StaffToday { get; set; }
        public int StaffTotal { get; set; }
        public List<Person> StaffSearchDGrid { get; set; }


    }
}
