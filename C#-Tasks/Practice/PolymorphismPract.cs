using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Practice;

class First
{
    public void Show()
    {
        Console.WriteLine("I am base class ");
    }
}

internal class PolymorphismPract : First 
{

    public void SplitString()
    {
        Console.Write("Enter a date (dd-MM-yyyy): ");
        string input = Console.ReadLine();

        DateOnly date;
        while (!DateOnly.TryParseExact(input, "dd-MM-yyyy", out date))
        {
            Console.Write("Invalid format. Please enter a date in dd-MM-yyyy format: ");
            input = Console.ReadLine();
        }

        Console.WriteLine(date);
    }
}
