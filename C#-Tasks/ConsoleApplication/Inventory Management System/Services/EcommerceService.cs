using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.ConsoleApplication.Inventory_Management_System.Validators;

namespace Tasks.ConsoleApplication.Inventory_Management_System.Services
{
    internal class EcommerceService
    {
        public void EcommerceSystem()
        {
            Console.WriteLine("\n*************************************************************************************************\n");
            Console.WriteLine("***************************         E-commerce Management System        *************************");
            Console.WriteLine("\n*************************************************************************************************\n");
            
            Console.WriteLine("Choose the User type : ");
            Console.WriteLine("> 1. Admin");
            Console.WriteLine("> 2. User");
            int option = InputValidation.GetOption(2);
            if(option == 1)
            {
                Console.Write("Enter a admin password :");
                string password = Console.ReadLine();
                if (password == "admin")
                {
                    InventoryServices inventoryServices = new InventoryServices();
                    inventoryServices.InventoryService();
                }
                else
                {
                    Console.WriteLine("Error : Invalid password");
                }
            }
            else if (option == 2)
            {
                UserServices userServices = new UserServices();
                userServices.UserService();
            }
            else
            {
                Console.WriteLine("Error : unable to choose the user type..");
            }
        }

    }
}
