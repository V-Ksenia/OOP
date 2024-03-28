using ATMApplication.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMApplication.Domain.Enteties
{
    public sealed record TransactionInfo(TransactionType _tranType, decimal _tranAmount, string _desc);
}
