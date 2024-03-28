using ATMApplication.Domain.Enteties;
using ATMApplication.UI;
using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ATMApplication.Domain.Interfaces
{
    public interface IUserAccountActions
    {
        void CheckBalance();
        void MakeWithDrawal();
        void ProcessInternalTransfer(Transfer internalTransfer, List<UserAccount> users);
        public void ViewTransaction();
        
    }
}
