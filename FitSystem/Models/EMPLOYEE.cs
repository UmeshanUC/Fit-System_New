using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitSystem.Models
{
    public class Employee
    {
        [Key]
        public string NIC { get; set; }
        public string EpfNo { get; set; }
        public long BankAccount { get; set; }
        public string OtherFacts { get; set; } //Qualifications or Describe Employee
        //Salary
        public decimal BaseSalary { get; set; }
        public decimal Bonus { get; set; }
        public decimal Deduction { get; set; }
        public decimal NetSalary { get; set; }


        #region NavigationProps
        public Person Person { get; set; }
        #endregion
    }
}
