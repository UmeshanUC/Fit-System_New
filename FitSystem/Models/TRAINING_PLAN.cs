using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitSystem.Models
{
    public class Training_Plan
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string PlanCode { get; set; }
        public string Shedule { get; set; }
        public string Diet { get; set; }


        ////////////////REFERENCES////////////////////
    }
}
