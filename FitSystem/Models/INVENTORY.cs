using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FITSystem.Models
{
    public class Inventory
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ItemCode{ get; set; }
        public string Name { get; set; }
        public string PurchasedDate { get; set; }
        public int UnitValue { get; set; }
        public int Quantity { get; set; }

        ////////////////REFERENCES////////////////////

    }
}
