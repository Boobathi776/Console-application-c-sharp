using System.Globalization;

namespace Tasks.Helpers;

static  class Helper
{
    //check the given input is a string
    public static bool IsString(string str)
    {
        bool isSuccess = int.TryParse(str, out int result);
        if(!isSuccess) 
        {
            return true;
        }
        return false;
    }

    //check is the string is digit
    public static bool IsDigit(string str)
    {
        bool isDigit = int.TryParse(str, out int result);
        if (isDigit)
        {
            return true;
        }
        return false;
    }


    //for get a valid string 
    public static string GetString(string str)
    {
        bool isSuccess = int.TryParse(str, out int result);
        if (!isSuccess)
        {
            return str;
        }
        else
        {
            Console.Write("Enter a valid string : ");
            return GetString(Console.ReadLine());
        }
    }

    //For get a integer from the user
    public static int GetInteger(string str)
    {
        int result = 0;
        while(!int.TryParse(str,out result))
        {
            Console.WriteLine("Enter a valid number : ");
        }
        return result;
    }

    //For get the valid finance related values like decimal
    public static decimal GetDecimal(string str)
    {
        decimal result = 0;
        bool isDecimal = decimal.TryParse(str, out result);
        if (isDecimal)
        {
            return result;
        }
        else
        {
            Console.Write("Enter a valid Amount : ");
            return GetDecimal(Console.ReadLine());
        }
    }
    //for get a double value from the user 
    public static double GetDouble(string str)
    {
        double result = 0;
        bool isDouble = double.TryParse(str, out result);
        if (isDouble)
        {
            return result;
        }
        else
        {
            Console.Write("Enter a valid Number : ");
            return GetInteger(str);
        }
    }

    public static string GetYesOrNo()
    {
        string choice;
        do
        {
            Console.Write("Enter yes or no : ");
            choice = Console.ReadLine();
        } while (choice != "y" && choice != "n");
        return choice;
    }
}
