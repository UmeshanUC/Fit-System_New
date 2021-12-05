using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FITSystem.Models
{
    public class Package
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public string Code { get; set; } // datatype
        public string Name { get; set; }
        public int Value { get; set; }
        public string Benefits { get; set; }
        public string Duration { get; set; }
        public string Details { get; set; }

        ////////////////REFERENCES////////////////////
    }
}
