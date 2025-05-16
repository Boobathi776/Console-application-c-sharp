using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Tasks;
class ATMmenu
{
    decimal balance;

    public void showOption()
    {
        Console.WriteLine("a.Check Balance");
        Console.WriteLine("b.Deposit");
        Console.WriteLine("c.Withdraw");
        Console.WriteLine("d.Exit");
        Console.Write("Enter your choice : ");
    }

    public char GetOption()
    {
        char choice;
        bool isSuccess = char.TryParse(Console.ReadLine(), out choice);
        if (isSuccess)
        {
            return choice;
        }
        else
        {
            return GetOption();
        }
    }


    public void Banking()
    {
        Console.WriteLine("ATM machine");
        char choice;
        decimal deposit_amount;
        decimal withdraw_amount;
        Console.Write("Enter pin number : ");
    GetPinNumber:
        string pin_number = Console.ReadLine();
        if (pin_number.Length == 6)
        {
            do
            {
                Console.WriteLine("===========================================================");
                showOption();
                choice = GetOption();
                switch (choice)
                {
                    case 'a':
                        Console.WriteLine($"The available balance in you account is {balance}");
                        break;
                    case 'b':
                        Console.Write("Enter the amount that you want to deposit : ");
                        decimal.TryParse(Console.ReadLine(), out deposit_amount);
                        balance += deposit_amount;
                        Console.WriteLine("your about is credited to your account");
                        goto case 'a';
                        break;
                    case 'c':
                        Console.WriteLine($"The available balance in you account is {balance}");
                        Console.Write("Enter the amount that you want to withdraw : ");
                        decimal.TryParse(Console.ReadLine(), out withdraw_amount);
                        if (balance > withdraw_amount)
                        {
                            balance -= withdraw_amount;
                            Console.WriteLine("The amount is debited successfully..");
                            goto case 'a';
                        }
                        else
                            Console.WriteLine("You don't have a suffiecient amount to withdraw");
                        break;
                    case 'd':
                        Console.WriteLine("Thanks for do the banking");
                        break;
                }
            } while (choice != 'd');
        }
        else
        {
            Console.Write("Enter a valid pin number : ");
            goto GetPinNumber;
        }
    }
}