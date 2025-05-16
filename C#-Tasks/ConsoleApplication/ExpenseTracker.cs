
using Tasks.Helpers;
namespace Tasks.ConsoleApplication;

internal class ExpenseTracker
{
    List<string> expenses = new List<string>(); // For expense names
    List<decimal> amounts = new List<decimal>(); // For the amount that spend for purchase something
    public void Tracker()
    {
        Console.WriteLine("********EXPENSE TRACKER********");
        Console.Write("Enter the number of expenses : ");
        int numberOfExepenses = 0;
    GetAgain:
        int.TryParse(Console.ReadLine(), out numberOfExepenses);
        string expenseName = " ";

        if (numberOfExepenses > 0)
        {
            for (int i = 0; i < numberOfExepenses; i++)
            {
                Console.WriteLine("\n-----------------------------------------------------------------------------------------------------");
                Console.Write("Enter the Expense name or enter \"exit\" to stop : ");

                expenseName = Helper.GetString(Console.ReadLine().Trim());  // Helper.GetString() is my own namespace static method for get a valid string value

                if (expenseName == "exit")
                    break;
                expenses.Add(expenseName.ToUpper()); // adding the expense name to the list
                Console.Write($"Enter the amount that you spent for {expenseName} : ");
                decimal expense = Helper.GetDecimal(Console.ReadLine().Trim());
                amounts.Add(expense);
                Console.WriteLine("\n-----------------------------------------------------------------------------------------------------");
            }
        }
        else
        {
            Console.Write("Enter a valid number to continue : ");
            goto GetAgain;
        }
        Console.WriteLine("you reached your limit !......");

        Console.Write("\nIf you want to add more then enter (y/n) : ");
    GetOption:
        string yes = Console.ReadLine().ToLower().Trim();
        if (yes == "y")
        {
            Console.WriteLine("\n-----------------------------------------------------------------------------------------------------");
            Console.Write("Enter the Expense name or enter \"exit\" to stop : ");
            expenseName = Helper.GetString(Console.ReadLine().ToLower().Trim());

            while (expenseName != "exit")
            {
                if (expenseName == "exit") break;
                expenses.Add(expenseName); // adding the expense name to the list
                Console.Write($"Enter the amount that you spent for {expenseName} : ");
                decimal expense = Helper.GetDecimal(Console.ReadLine().Trim());
                amounts.Add(expense);
                Console.WriteLine("\n-----------------------------------------------------------------------------------------------------");
                Console.Write("Enter the Expense name or enter \"exit\" to stop : ");
                expenseName = Helper.GetString(Console.ReadLine().Trim());
            }
        }
        else if (yes == "n") { }
        else
        {
            Console.Write("Enter a valid option : ");
            goto GetOption;
        }

        int expenseAmount = 0;
        Console.WriteLine("\n-----------------------------------------------------------------------------------------------------");
        Console.WriteLine("Expense Name\t\tAmount");
        Console.WriteLine("\n-----------------------------------------------------------------------------------------------------");
        foreach (var expense in expenses) 
        {
            Console.WriteLine($"{expense}\t\t\t{amounts[expenseAmount]}");
            expenseAmount++;
        }

        Console.WriteLine("\n-----------------------------------------------------------------------------------------------------");
        int maximumExpenseIndex = amounts.IndexOf(amounts.Max());
        string maxExpenseName = expenses[maximumExpenseIndex];
        Console.WriteLine($"\nYou spend so much money on {maxExpenseName}");
        Console.WriteLine("\n-----------------------------------------------------------------------------------------------------");

        int minimumExpenseIndex = amounts.IndexOf(amounts.Min());
        string minExprenseName = expenses[minimumExpenseIndex];
        Console.WriteLine($"You spend minimum money on {minExprenseName}");
        Console.WriteLine("\n-----------------------------------------------------------------------------------------------------");
        decimal totalAmount = 0;
        foreach (decimal itemAmount in amounts)
        {
            totalAmount += itemAmount;
        }
        Console.WriteLine($"Total amount spend today is : {totalAmount}");
        Console.WriteLine("\n-----------------------------------------------------------------------------------------------------");
        decimal averageAmount = 0;
        averageAmount = totalAmount / (amounts.Count);
        Console.WriteLine($"The average amount that you spend per day is : {averageAmount}");
        Console.WriteLine("\n-----------------------------------------------------------------------------------------------------");

    }

}
        
 