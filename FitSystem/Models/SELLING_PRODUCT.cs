using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitSystem.Models
{
    public class Product
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductId { get; set; }
        public string Details { get; set; }
        public string Supplier { get; set; }
        public int UnitPrice { get; set; }
        public int InStockQty { get; set; }


    }
}
