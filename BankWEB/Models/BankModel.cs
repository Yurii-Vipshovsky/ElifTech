using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankWEB.Models
{
    public class BankModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double InterestRate {get;set;}
        public double MaximumLoan { get; set; }
        public double MinimumDownPayment { get; set; }
        public uint LoanTerm { get; set; }
        public string UserId { get; set; }
    }
}
