using ATMApplication.Domain.Enums;
using ATMApplication.Domain.Interfaces;
using ATMApplication.UI;
using ATMApplication.App;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;
using System.Transactions;

namespace ATMApplication.Domain.Enteties
{
    public class UserAccount : Entity, IUserAccountActions
    {
        public long CardNumber { get; set; }
        public int CardPin { get; set; }
        public long AccountNumber { get; set; }
        public string FullName { get; set; }
        public decimal AccountBalance { get; set; }
        public int TotalLogin { get; set; }
        public bool IsLocked { get; set; }

        Transaction transaction;

        public int minimumKeptAmount = 500;

        public List<Transaction> _transactions = new();

        public void CheckBalance()
        {
            Console.WriteLine($"Account balance: {AccountBalance}");
        }
        public void MakeWithDrawal()
        {
            var transaction_amt = 0;
            int selectedAmount = AppScreen.SelectAmount();
            if (selectedAmount == -1)
            {
                MakeWithDrawal();
                return;
            }
            else if (selectedAmount != 0)
            {
                transaction_amt = selectedAmount;
            }
            else
            {
                Console.WriteLine($"amount {AppScreen.currency}");
                transaction_amt = Convert.ToInt32(Console.ReadLine());
            }

            if (transaction_amt <= 0)
            {
                Console.WriteLine("Amount needs to be greater than zero. Try agin");
                return;
            }
            if (transaction_amt % 500 != 0)
            {
                Console.WriteLine("You can only withdraw amount in multiples of 500 or 1000 $. Try again.");
                return;
            }

            if (transaction_amt > AccountBalance)
            {
                Console.WriteLine($"Withdrawal failed. Your balance is too low to withdraw");
                return;
            }
            if ((AccountBalance - transaction_amt) < minimumKeptAmount)
            {
               Console.WriteLine($"Withdrawal failed. Your account needs to have " +
                    $"minimum {minimumKeptAmount}");
                return;
            }

            transaction = new Transaction(new TransactionInfo(TransactionType.Withdrawal, -transaction_amt, "Withdrawn" + $"{transaction_amt}"));
            transaction.InsertTransaction(AccountNumber);
            _transactions.Add(transaction);

            AccountBalance -= transaction_amt;

            Console.WriteLine($"You have successfully withdrawn " +
                $"{transaction_amt}");
        }

        public void ProcessInternalTransfer(Transfer internalTransfer, List<UserAccount> users) 
        {
            if (internalTransfer.TransferAmount <= 0)
            {
                Console.WriteLine("Amount needs to be more than zero. Try again.");
                return;
            }

            if (internalTransfer.TransferAmount > AccountBalance)
            {
                Console.WriteLine($"Transfer failed. You do not have enough balance" +
                    $" to transfer {internalTransfer.TransferAmount}");
                return;
            }
        
            if ((AccountBalance - internalTransfer.TransferAmount) < minimumKeptAmount)
            {
                Console.WriteLine($"Transfer failed. Your account needs to have minimum" +
                    $" {minimumKeptAmount}");
                return;
            }

            var selectedBankAccountReciever = (from userAcc in users
                                               where userAcc.AccountNumber == internalTransfer.BankAccountNumber
                                               select userAcc).FirstOrDefault();
            if (selectedBankAccountReciever == null)
            {
                Console.WriteLine("Transfer failed. Recieber bank account number is invalid.");
                return;
            }

            if (selectedBankAccountReciever.FullName != internalTransfer.BankAccountName)
            {
                Console.WriteLine("Transfer Failed. Recipient's bank account name does not match.");
                return;
            }

            transaction = new Transaction(new TransactionInfo(TransactionType.Transfer, -internalTransfer.TransferAmount, "Transfered " +
              $"to {selectedBankAccountReciever.AccountNumber} ({selectedBankAccountReciever.FullName})"));
            transaction.InsertTransaction(AccountNumber);
            _transactions.Add(transaction);

            AccountBalance -= internalTransfer.TransferAmount;
           
            var receievertransaction = new Transaction(new TransactionInfo(TransactionType.Transfer, internalTransfer.TransferAmount, "Transfered from" +
              $"{AccountNumber}({FullName})"));
            receievertransaction.InsertTransaction(selectedBankAccountReciever.AccountNumber);
            selectedBankAccountReciever._transactions.Add(receievertransaction);

            selectedBankAccountReciever.AccountBalance += internalTransfer.TransferAmount;

            Console.WriteLine($"You have successfully transfered" +
                $" {internalTransfer.TransferAmount} to " +
                $"{internalTransfer.BankAccountName}", true);
        }

        public void ViewTransaction()
        {
            var filteredTransactionList = _transactions.Where(t => t.UserBankAccountId == this.AccountNumber).ToList();

            if (filteredTransactionList.Count <= 0)
            {
                Console.WriteLine("You have no transaction yet.");
            }
            else
            {
                var table = new ConsoleTable("Transaction Date", "Type Descriptions Amount " + AppScreen.currency);
                foreach (var tran in filteredTransactionList)
                {
                    table.AddRow(tran.TransactionDate, tran.TransactionInformation);
                }
                table.Options.EnableCount = false;
                table.Write();
                Console.WriteLine($"You have {filteredTransactionList.Count} transaction(s)");
            }
        }
    }
}
