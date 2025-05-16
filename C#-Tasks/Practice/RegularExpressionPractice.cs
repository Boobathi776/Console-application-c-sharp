using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tasks.Practice;

//RegularExpressionPractice regularExpression = new RegularExpressionPractice();
////regularExpression.CheckRegularExpression();
//regularExpression.CheckAllPosibility();

namespace Tasks.Practice
{

    public class InvalidStringException : Exception
    {
        public InvalidStringException(string message) 
        {

        }
    }
    internal class RegularExpressionPractice
    {
        public RegularExpressionPractice() { }

        public void CheckRegularExpression()
        {
            string inputExpression = "^[a-zA-Z ]+$";
            string userInputName = Console.ReadLine();
            try
            {
                if (Regex.IsMatch(userInputName, inputExpression))
                {
                    Console.WriteLine(userInputName);
                    Console.WriteLine("It's a valid input!!!");
                }
                else
                {
                    Console.WriteLine("the given input is wrong make sure the input is correct or not ");
                    throw new InvalidStringException("The name should not contain digits or special characters");
                }
            }
            catch (InvalidStringException e)
            {
                Console.WriteLine(e.Message);
                CheckRegularExpression();
            }
        }

        public void PasswordValidation(string password)
        {
            string passwordPattern =@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]){5,}$";
            Console.Write("Enter a password : ");
            //string userEnteredPassword = Console.ReadLine();
            string userEnteredPassword = password;

            try
            {
                if (Regex.IsMatch(userEnteredPassword, passwordPattern))
                {
                    Console.WriteLine($"{password}  : is valid");
                    Console.WriteLine("all the requirements are satisfied so valid password");
                }
                else
                {
                    Console.WriteLine($"{password}  : is invalid password");
                    Console.WriteLine("Invalid password is entered");
                    throw new InvalidStringException("password is not so strong");
                }
            }
            catch(InvalidStringException e)
            {
                using (StreamWriter writer = new StreamWriter(@$"D:\Azure-Assignment\C#-Tasks\C#-Tasks\Practice\Error log files\Error_log{DateTime.Now.ToString("dd-MM-yyyy")}.txt" ,true ))
                {
                    writer.WriteLine("\n==========================================================================================================================================\n");
                    writer.WriteLine("Error : "+e.Message);
                    writer.WriteLine("Soucrce :"+e.Source);
                    writer.WriteLine("Stack Trace :"+e.StackTrace);
                    writer.WriteLine("date and time : "+DateTime.Now.ToString("dd-MM-yyyy dddd MMMM hh:mm:ss"));
                    writer.WriteLine("\n==========================================================================================================================================\n");
                }
            }
        }


        public void CheckAllPosibility()
        {
            List<string> passwords = new List<string> { "tomandjerrycnK@$#%" ,"boobathi" ,"iam naan","you nee","jackiechan76%D" , "CrackTheSystem12312@#$","chinnavan"};
            foreach(string word in passwords)
            {
                PasswordValidation(word);
            }

        }
    }

}