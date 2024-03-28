using ATMApplication.Domain.Enteties;
using ATMApplication.Domain.Enums;
using ATMApplication.Persistense.Repository;
using ATMApplication.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMApplication.App
{
    public class ATMApp
    {
        private List<UserAccount> userAccountList;
        private UserAccount selectedAccount;

        private AppScreen screen;
        private UserRepository userRepo;
        public ATMApp()
        {
            screen = new AppScreen();
            userRepo = new UserRepository();
        }
        public void Run()
        {
            AppScreen.Welcome(); 
            CheckUserCardNumAndPassword(); 
            AppScreen.WelcomeCustomer(selectedAccount.FullName);
            while (true)
            {
                AppScreen.DisplayAppMenu();
                ProcessMenuoption();
            }
        }
        public void InitializeData()
        {
            userAccountList = userRepo.ListAllAsync();
        }

        public void CheckUserCardNumAndPassword()
        {
            bool isCorrectLogin = false;
            while (isCorrectLogin == false)
            {
                UserAccount inputAccount = AppScreen.UserLoginForm();
                AppScreen.LoginProgress();
                foreach (UserAccount account in userAccountList)
                {
                    selectedAccount = account;
                    if (inputAccount.CardNumber.Equals(selectedAccount.CardNumber))
                    {
                        selectedAccount.TotalLogin++;

                        if (inputAccount.CardPin.Equals(selectedAccount.CardPin))
                        {
                            selectedAccount = account;

                            if (selectedAccount.IsLocked || selectedAccount.TotalLogin > 3)
                            {
                                AppScreen.PrintLockScreen();
                            }
                            else
                            {
                                selectedAccount.TotalLogin = 0;
                                isCorrectLogin = true;
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid card PIN.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid card number.");
                    }
                    if (isCorrectLogin == false)
                    {

                        selectedAccount.IsLocked = selectedAccount.TotalLogin == 3;
                        if (selectedAccount.IsLocked)
                        {
                            AppScreen.PrintLockScreen();
                            Run();
                        }
                    }
                    //Console.Clear();
                }
            }
        }
        private void ProcessMenuoption()
        {
            Console.WriteLine("an option:");
            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case (int)AppMenu.CheckBalance:
                    selectedAccount.CheckBalance();
                    break;
                case (int)AppMenu.MakeWithdrawal:
                    selectedAccount.MakeWithDrawal();
                    break;
                case (int)AppMenu.InternalTransfer:
                    var internalTransfer = screen.InternalTransferForm();
                    selectedAccount.ProcessInternalTransfer(internalTransfer, userAccountList);
                    break;
                case (int)AppMenu.ViewTransaction:
                    selectedAccount.ViewTransaction();
                    break;
                case (int)AppMenu.Logout:
                    AppScreen.LogoutProgress();
                   Console.WriteLine("You have successfully logged out. Please collect " +
                        "your ATM card.");
                    Run();
                    break;
                default:
                    Console.WriteLine("Invalid Option.", false);
                    break;
            }
        }
    }
}
