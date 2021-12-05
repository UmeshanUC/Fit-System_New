using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FITSystem.Classes
{
    public class StaffAndMembersContext
    {
        public DetailCardCtx MedicalOfficersCard { get; set; }
        public DetailCardCtx TrainersCard { get; set; }
        public DetailCardCtx MembersCard { get; set; }
        public DetailCardCtx OtherStaffCard { get; set; }

    }
}
