using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FitSystem.Models
{
    /// <summary>
    /// Only for contanct Details. No Relationship to the other Models
    /// </summary>
    public class Branches
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int BranchCode { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public int Telephone { get; set; }
        public string Location { get; set; }

    }
}
