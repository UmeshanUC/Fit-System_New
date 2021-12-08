using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitSystem.Models
{
    public class Section
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string SectionCode { get; set; }
        public string SectionName { get; set; }
        public string Description { get; set; }
        public string SectionManager { get; set; }


        
    }
}
