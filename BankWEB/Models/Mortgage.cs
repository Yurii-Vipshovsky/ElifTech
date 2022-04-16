using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankWEB.Models
{
    public class Mortgage
    {
        public double InitialLoan { get; set; }
        public double DownPayment { get; set; }
        public uint NumberOfPayments { get; set; }
        public int BankId { get; set; }
        public double MonthlyPayment { get; set; }
    }
}
