using ATMApplication.Domain.Enteties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ATMApplication.UI
{
    internal class AppScreen
    {
        internal const string currency = "$";
        internal static void Welcome()
        {
            Console.Clear();
            Console.Title = "ATM Application";

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.CursorLeft = 24;
            Console.CursorTop = 3;

            Console.WriteLine("WELCOME TO ATM APP\n\n");

            Console.ForegroundColor = ConsoleColor.White;
            
            Console.WriteLine("└─ Please insert your card");

        }
        internal static UserAccount UserLoginForm()
        {
            UserAccount tempUserAccount = new UserAccount();

            Console.WriteLine("└─ Enter your card number");
            tempUserAccount.CardNumber = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("└─ Enter your PIN");
            tempUserAccount.CardPin = Convert.ToInt32(Console.ReadLine());
            return tempUserAccount;
        }
        internal static void WelcomeCustomer(string fullName)
        {
            Console.WriteLine($"Welcome back, {fullName}");
        }
        internal static void LoginProgress()
        {
            Console.WriteLine("\nChecking card number and PIN...");
            //Utility.PrintDotAnimation();
        }
        internal static void PrintLockScreen()
        {
            Console.WriteLine("Your account is locked. Please go to the nearest branch" +
                " to unlock your account. Thank you.");
        }
        internal static void DisplayAppMenu()
        {
           // Console.Clear();
            Console.WriteLine("-------My ATM App Menu-------");
            Console.WriteLine(":                           :");
            Console.WriteLine("1. Account Balance          :");
            Console.WriteLine("2. Withdrawal               :");
            Console.WriteLine("3. Transfer                 :");
            Console.WriteLine("4. Transactions             :");
            Console.WriteLine("5. Logout                   :");
        }
        internal Transfer InternalTransferForm()
        {
            var Transfer = new Transfer();
            Console.WriteLine("Recipient's account number:");
            Transfer.BankAccountNumber = Convert.ToInt64(Console.ReadLine());

            Console.WriteLine($"Amount {currency}");
            Transfer.TransferAmount = Convert.ToInt64(Console.ReadLine());

            Console.WriteLine("Recipient's name:");
            Transfer.BankAccountName = Console.ReadLine();

            return Transfer;
        }

        internal static void LogoutProgress()
        {
            Console.WriteLine("Thank you for using My ATM App.");
           // Utility.PrintDotAnimation();
            //Console.Clear();
        }
        internal static int SelectAmount()
        {
            Console.WriteLine("");
            Console.WriteLine(":1.{0}500      5.{0}10,000", currency);
            Console.WriteLine(":2.{0}1000     6.{0}15,000", currency);
            Console.WriteLine(":3.{0}2000     7.{0}20,000", currency);
            Console.WriteLine(":4.{0}5000     8.{0}40,000", currency);
            Console.WriteLine(":0.Other");
            Console.WriteLine("option:");

            int selectedAmount = Convert.ToInt32(Console.ReadLine());
            switch (selectedAmount)
            {
                case 1:
                    return 500;
                    break;
                case 2:
                    return 1000;
                    break;
                case 3:
                    return 2000;
                    break;
                case 4:
                    return 5000;
                    break;
                case 5:
                    return 10000;
                    break;
                case 6:
                    return 15000;
                    break;
                case 7:
                    return 20000;
                    break;
                case 8:
                    return 40000;
                    break;
                case 0:
                    return 0;
                    break;
                default:
                    Console.WriteLine("Invalid input. Try again.");
                    return -1;
                    break;
            }
        }
    }
}
