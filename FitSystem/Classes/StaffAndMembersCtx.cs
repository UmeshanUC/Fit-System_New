using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitSystem.Classes
{
    public class StaffAndMembersCtx
    {
        public DetailCardCtx MedicalOfficersCard { get; set; }
        public DetailCardCtx TrainersCard { get; set; }
        public DetailCardCtx MembersCard { get; set; }
        public DetailCardCtx OtherStaffCard { get; set; }

    }
}
