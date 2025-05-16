using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Drawing.Charts;
using iText.StyledXmlParser.Node;
using Microsoft.Extensions.Logging.Abstractions;
using Tasks.ConsoleApplication.Inventory_Management_System.Services;
using Tasks.ConsoleApplication.Inventory_Management_System.Models;
using System.Linq.Expressions;
namespace Tasks.ConsoleApplication.Inventory_Management_System.Validators
{
    internal class InputValidation
    {
        public static int GetProductId()
        {
            try
            {
                string inputProductId;
                int productId;
                Console.Write("Enter the product Id : ");
                inputProductId = Console.ReadLine();
                while(string.IsNullOrWhiteSpace(inputProductId) || !int.TryParse(inputProductId,out  productId) || productId <= 0  )
                {
                    Console.Write("Enter a valid product ID : ");
                    inputProductId = Console.ReadLine();
                }
                return productId;
            }
            catch(Exception e)
            {
                Console.WriteLine("Error : Unable to get a product id");
                throw new Exception();
            }
        }

        public static string GetProductName()
        {
            try
            {
                string productNamePattern = @"^[A-Z][a-z]+([- ]*[A-Z]*[a-z]*)*$";
                Console.WriteLine("Enter a product name (Ex : Milk): ");
                string productName=Console.ReadLine();
                while(string.IsNullOrWhiteSpace(productName) || productName.Length == 2 || !Regex.IsMatch(productName,productNamePattern))
                {
                    Console.WriteLine("Enter a valid product name : ");
                    productName = Console.ReadLine();
                }
                return productName;
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: unable to get the proper product name ");
                throw new ArgumentNullException();
            }
        }

        public static productCategory GetCategory()
        {
            try
            {
                int categoryNumber;
                var categories = Enum.GetValues(typeof(productCategory));
                int i = 1;
                foreach (var productCategory in categories)
                {
                    Console.Write($"({i}){productCategory,-15}");
                    if (i%4==0) Console.WriteLine();
                    i++;
                }
                Console.Write("\nEnter the category number : ");
                string inputCategoryNumber = Console.ReadLine();
                while(!int.TryParse(inputCategoryNumber, out  categoryNumber))
                {
                    Console.Write("Enter a valid category number : ");
                    inputCategoryNumber = Console.ReadLine();
                }
                productCategory category;
                if (Enum.IsDefined(typeof(productCategory), categoryNumber))
                {
                     category = (productCategory)categoryNumber;
                }
                else
                {
                    category = productCategory.Others;
                }
                return category;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : unable to get the category from the user");
                return productCategory.Others;
            }
        }

        public static decimal GetUnitPrice(string productName)
        {
            try
            {
                decimal productPrice;
                Console.Write($"Enter a unit price {productName}");
                string inputPrice = Console.ReadLine();
                while(!decimal.TryParse(inputPrice,out productPrice) || productPrice < 0)
                {
                    Console.Write($"Enter a valid price for {productName}");
                    inputPrice = Console.ReadLine();
                }
                return productPrice;
            }
            catch(Exception e)
            {
                Console.WriteLine("Error : unable to get the unit price of the product");
                throw new Exception();
            }
        }

        public static int GetQuantity()
        {
            try
            {
                int quantity;
                Console.Write("Enter a quantity");
                string inputQuantity = Console.ReadLine();
                while(!int.TryParse(inputQuantity,out quantity) || quantity < 0)
                {
                    Console.Write("Enter a valid quanity : ");
                    inputQuantity = Console.ReadLine();
                }
                return quantity;
            }
            catch(Exception e)
            {
                Console.WriteLine("Error : unable to get the quantity for the product");
                return 0;
            }
        }

        public static bool IsAvailable(int productId , List<Products> products)
        {
            if(products.Any(product=> product.ProductID == productId))
            {
                return true;
            }
            else
            {
                Console.WriteLine($"There is no product with this id {productId} !");
                return false;
            }
        }

        public static int GetOption(int range)
        {
            try
            {
                int option;
                Console.Write($"Enter your choice(1-{range}):");
                string inputChoice = Console.ReadLine();
                while (!int.TryParse(inputChoice, out option) || option < 0 || option > range)
                {
                    Console.Write($"Enter a valid option(1-{range}):");
                    inputChoice = Console.ReadLine();
                }
                return option;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : unable to enter a valid choice");
                return -1;
            }
        }

        public static string GetYesOrNoForProceed()
        {
            try
            {
                string inputChoice;
                do
                {
                    Console.Write("Enter \"y\" or \"n\" :");
                    inputChoice = Console.ReadLine().ToLower();
                } while (inputChoice != "y" && inputChoice != "n");
                return inputChoice;
            }
            catch(Exception error)
            {
                Console.WriteLine("Error : unable to proceed yes or no ..");
                InputValidation.LogError(error);
                return null;
            }
        }


       public static Products GetMatchedProduct(int produtId,List<Products> products)
        {
            try
            {
                Products matchedProduct;
                if (products.Any(product => product.ProductID == produtId))
                {
                    matchedProduct = products.FirstOrDefault(product => product.ProductID == produtId);
                    return matchedProduct;
                }
                else
                {
                    return null;
                    throw new ArgumentNullException();
                }
            }
            catch (Exception error)
            {
                LogError(error);
                Console.WriteLine("Error : There is no product match with the given id..");
                return null;
            }
        }

        public static void LogError(Exception e)
        {
            try
            {
                string filepath = @$"D:\Azure-Assignment\C#-Tasks\C#-Tasks\ConsoleApplication\Inventory Management System\Error log files\{DateTime.Now.ToString("dd-MM-yyyy")}.txt";
                using (StreamWriter writer = new StreamWriter(filepath, true))
                {
                    writer.WriteLine("\n==============================================================================================\n");
                    writer.WriteLine("Source :" + e.Source);
                    writer.WriteLine("Message : " + e.Message);
                    writer.WriteLine("Stack trace \n");
                    writer.WriteLine(e.StackTrace);
                    writer.WriteLine("date and time :" + DateTime.Now.ToString("dd-MM-yyyy dhh-mm-ss"));
                    writer.WriteLine("\n==============================================================================================\n");
                }
            }
            catch (Exception error)
            {
                Console.WriteLine($"Error: {error.Message}");
            }
        }

    }
}
