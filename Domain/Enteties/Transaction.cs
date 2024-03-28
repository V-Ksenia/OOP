using ATMApplication.Domain.Enums;
using ATMApplication.Domain.Interfaces;
using ATMApplication.UI;
using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMApplication.Domain.Enteties
{
    public class Transaction : Entity, ITransaction
    {
        public long UserBankAccountId { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionInfo TransactionInformation { get; set; }

        public Transaction(TransactionInfo info) 
        {
            TransactionInformation = info;
            TransactionDate = DateTime.Now;        
        }
        public void InsertTransaction(long accountId)
        {
            if (accountId <= 0) return;
            UserBankAccountId = accountId;

        }
        
    }
 
}
