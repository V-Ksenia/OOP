using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMApplication.Domain.Enteties
{
    public class Transfer
    {
        public decimal TransferAmount { get; set; }
        public long BankAccountNumber { get; set; }
        public string BankAccountName { get; set; }
    }
}
