using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.ConsoleApplication.Inventory_Management_System.Models;
using Tasks.ConsoleApplication.Inventory_Management_System.Validators;

namespace Tasks.ConsoleApplication.Inventory_Management_System.Services
{
    internal class UserServices
    {
        List<User> users = new List<User>();
        string directoryPath = @"D:\Azure-Assignment\C#-Tasks\C#-Tasks\ConsoleApplication\Inventory Management System\Data files\";
        public void UserService()
        {
            ReadFromFile();
            Console.WriteLine("Are you a new User?");
            string newUser = InputValidation.GetYesOrNoForProceed();
            if(newUser== "y")
            {
                Console.WriteLine("Please enter your details to register");
                RegisterUser();
            }
            else
            {
                Console.WriteLine("Please enter your user ID to login");
                LogInUser();
            }
        }

        public void RegisterUser()
        {
            try
            {
                int userId = UserDetailsValidation.GetUserId();
                string userName = UserDetailsValidation.GetUserName();
                string mobileNumber = UserDetailsValidation.GetMobileNumber();
                string gmail = UserDetailsValidation.GetEmail();
                if (userId != 0 && userName != null && mobileNumber != null && gmail != null)
                {
                    while(users.Any(user => user.UserID == userId))
                    {
                        Console.WriteLine("Error: user id already exist");
                        userId = UserDetailsValidation.GetUserId();
                    }
                    User user = new User(userId, userName, mobileNumber, gmail);
                    users.Add(user);
                    using(StreamWriter writer = new StreamWriter(Path.Combine(directoryPath,"UserDetails.txt"), true))
                    {
                        //writer.WriteLine($"{"User ID",-8},{"Name",-15},{"Mobile No",-14},{"Gamil",-25},{"User transaction file"}");
                        writer.WriteLine($"{user.UserID,-8},{user.UserName,-15},{user.MobileNumber,-14},{user.Gmail,-25},{userName}.txt");
                    }
                    Console.WriteLine("User added successfully");
                    LogInUser();
                }
                else
                {
                    Console.WriteLine("Error: unable to add the user");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Error : unable to get the user details from the user..");
            }
        }

        public void LogInUser()
        {
            try
            {
                int userId = UserDetailsValidation.GetUserId();
                if(users.Any(user=>user.UserID == userId))
                {
                    User user = users.FirstOrDefault(user => user.UserID == userId);
                    Console.WriteLine($"Welcome {user.UserName}!!!!!!");
                    BillServices billServices = new BillServices(user.UserID,user.UserName);
                    billServices.BillService();
                }
                else
                {
                    Console.WriteLine("Error: user id not exist");
                    Console.WriteLine("Register a new user");
                    RegisterUser();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : unable to login the user..");
            }
            
        }

        public void ReadFromFile()
        {
            try
            {
                string filePath = @"D:\Azure-Assignment\C#-Tasks\C#-Tasks\ConsoleApplication\Inventory Management System\Data files\UserDetails.txt";
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    int lineCount = 0;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (lineCount == 0)
                        {
                            lineCount++;
                            continue;
                        }
                        string[] userDetails = line.Split(",", StringSplitOptions.RemoveEmptyEntries);
                        if (userDetails.Length > 0)
                        {
                            bool isIdConverted = int.TryParse(userDetails[0], out int userId);

                            if (isIdConverted )
                            {
                                users.Add(new User(userId, userDetails[1], userDetails[2], userDetails[3]));
                            }
                            else
                            {
                                Console.WriteLine("Invalid product details...");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"The product details is not in a correct format in the line no {lineCount}");
                        }
                        lineCount++;
                    }
                }
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("Error : Direcotry not exist... when i try to find a file for Read");
                InputValidation.LogError(e);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Error : file not exist when i try to read from a file");
                InputValidation.LogError(e);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error : {e.Message}");
                InputValidation.LogError(e);
            }
        
        }
    }
}
