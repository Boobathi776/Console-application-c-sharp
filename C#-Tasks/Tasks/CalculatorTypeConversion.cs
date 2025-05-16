using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks;
class CalculatorTypeConversion
{
    double number;
    public double getValue()
    {
        Console.Write("Enter the number  : ");
        try
        {
            number = Convert.ToDouble(Console.ReadLine());
        }
        catch (Exception e)
        {
            Console.WriteLine("Enter a number please...");
        }
        return number;
    }


    public void Calculating()
    {
        Console.WriteLine("Simple Calculator with two numbers\n");
        string proceed;
        do
        {
            double number1 = getValue();
            double number2 = getValue();
            showResult(number1, number2);
            GetProceed:
            proceed = Console.ReadLine().ToLower().Trim();
            if (proceed == "n") break;
            else if (proceed != "y" && proceed != "n") { Console.Write("enter a valid value : "); goto GetProceed; }
        } while (proceed == "y");

    }
    public void showResult(double number1, double number2)
    {
            double result;
            result = number1 + number2;
            Console.WriteLine($"\nThe sum of given numbes is {result}\n");

            result = number1 - number2;
            Console.WriteLine($"The subtraction of given numbers is {result}\n");

            result = number1 * number2;
            Console.WriteLine($"The multiplication of given numbers is {result}\n");

            if (number2 != 0)
            {
                result = number1 / number2;
                Console.WriteLine($"The division of given numbers is {result} \n");
            }
            else
            {
                Console.WriteLine("The number is not divisible by Zero");
            }
            Console.Write("Enter the option (y/n) to do again or not : ");
       

    }


}