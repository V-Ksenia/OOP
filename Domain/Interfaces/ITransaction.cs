using ATMApplication.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMApplication.Domain.Interfaces
{
    public interface ITransaction
    {
        void InsertTransaction(long accountId);
    }
}
