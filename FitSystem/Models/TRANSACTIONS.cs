using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FITSystem.Models
{
    public class Transactions
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int TransactionId { get; set; }
        public string PayMethod { get; set; }
        public int Value { get; set; }
        public string Date {get; set; }
        public string Time { get; set; }
        public string Payee { get; set; }
        public string Type { get; set; }
        public string User { get; set; }

    ////////////////REFERENCES////////////////////
}
}
