using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FITSystem.Models
{
    public class Employee
    {
        [Key][ForeignKey("Person")]
        public string NIC { get; set; }
        public string EpfNo { get; set; }
        public int BankAccount { get; set; }
        public string OtherFacts { get; set; } //Qualifications or Describe Employee
        //Salary
        public int BaseSalary { get; set; }
        public int Bonus { get; set; }
        public int Deduction { get; set; }
        public int NetSalary { get; set; }


        #region NavigationProps
        public Person Person { get; set; }
        #endregion
    }
}
