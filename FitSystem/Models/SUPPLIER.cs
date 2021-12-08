using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitSystem.Models
{
    public class Supplier
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Agent { get; set; }
        public int Telephone { get; set; }
        public string Adress { get; set; }
        public string DatePartnered { get; set; }



    }
}
