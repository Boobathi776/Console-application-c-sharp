using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tasks.ConsoleApplication.Inventory_Management_System.Validators
{
    internal class UserDetailsValidation
    {
        public static int GetUserId()
        {
            try { 
            Console.Write("Enter unique user ID : ");
            string inputId = Console.ReadLine();
            int userId;
            while (!int.TryParse(inputId, out userId) || userId < 0 )
            {
                Console.Write("Enter a valid user ID : ");
                inputId = Console.ReadLine();
            }
            return userId;
        }
            catch(Exception e)
            {
                Console.WriteLine("Error : unable to get a user id..");
                return -1;
            }
        }

        public static string GetUserName()
        {
            try
            {
                string productNamePattern = @"^[A-Z][a-z]+([- ][A-Z][a-z]*)*$";
                Console.Write("Enter a User name (Ex : Boobathi A [first and initial letter capital]): ");
                string productName = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(productName) || productName.Length == 2 || !Regex.IsMatch(productName, productNamePattern))
                {
                    Console.Write("Enter a valid user name : ");
                    productName = Console.ReadLine();
                }
                return productName;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: unable to get the proper user name ");
                return null;
            }
        }

        public static string GetMobileNumber()
        {
            try
            {
                Console.Write("Enter a 10 digit mobile number : ");
                string mobileNumber = Console.ReadLine();
                string mobileNumberPattern = @"^(\+91|0)?[6-9][0-9]{9}$";
                while(!Regex.IsMatch((string)mobileNumber, mobileNumberPattern))
                {
                    Console.Write("Enter a valid mobile number(10-digit) : ");
                    mobileNumber = Console.ReadLine();
                }
                return mobileNumber;
            }
            catch(Exception e)
            {
                Console.WriteLine("Error : unable to get a valid mobile number from the user..");
                return "0000000000";
            }
        }

        public static string GetEmail()
        {
            try
            {
                Console.Write("Enter you mail ID : ");
                string email = Console.ReadLine();
                string mailPattern = @"^[0-9a-zA-Z.+-_%]+@gmail\.com$";
                while(!Regex.IsMatch(email, mailPattern))
                {
                    Console.Write("Enter a valid gmail id : ");
                    email = Console.ReadLine();
                }
                return email;
            }
            catch(Exception e)
            {
                Console.WriteLine("Error : unable to get a mail id from the user");
                return "someone@gmail.com";
            }

        }
    }
}
