using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Bibliography;
using iText.StyledXmlParser.Node;
using Tasks.ConsoleApplication.Inventory_Management_System.Models;
using Tasks.ConsoleApplication.Inventory_Management_System.Validators;
using Tasks.ConsoleApplication.Inventory_Management_System.Validators;
using Tasks.Practice;

namespace Tasks.ConsoleApplication.Inventory_Management_System.Services
{

    internal class BillServices : InventoryServices
    {
        Dictionary<string, decimal[]> selectedProducts = new Dictionary<string, decimal[]>();
        string InventoryManagementDirecotry = ConfigurationManager.AppSettings["inventoryPath"];
        public int UserId { get; set; }
        public string UserName { get; set; }
        public BillServices(int userId,string userName)
        {
            UserId = userId;
            UserName = userName;
        }
        public void BillService()
        {
            SelectRequiredProducts();
            GenerateBill();
        }


        public void SelectRequiredProducts()
        {
           
            try
            {
                string wantExtraProduct = null;

                do
                {
                    BillGeneratorMenu();
                    int productId = InputValidation.GetProductId();
                    Products product = InputValidation.GetMatchedProduct(productId, products);
                    Console.WriteLine("\n----------------------------------------------------------------------------------------------------------\n");
                    Console.WriteLine($"\t{"Product ID",-12}{"Product name",-20}{"Category",-15}{"Unit price",-12}{"Quantity",-8}");
                    Console.ResetColor();
                    if (product != null)
                    {
                        Console.WriteLine("\n----------------------------------------------------------------------------------------------------------\n");
                        Console.ForegroundColor = ConsoleColor.Green;
                        product.DisplayProductDetails();
                        Console.ResetColor();
                        Console.WriteLine("\n----------------------------------------------------------------------------------------------------------\n");
                    }
                    else
                    {
                        Console.WriteLine("No product with this ID...");
                    }
                    int quantity = InputValidation.GetQuantity();
                    while(quantity > product.Quantity)
                    {
                        Console.WriteLine($"There is no enough quantity of this product... only {product.Quantity} available");
                        quantity = InputValidation.GetQuantity();
                    }
                    selectedProducts.Add(product.ProductName, new decimal[] { productId, product.UnitPrice, quantity });
                    Console.WriteLine("Do you want to add more products to the bill (y/n)?");
                    wantExtraProduct = InputValidation.GetYesOrNoForProceed();
                } while (wantExtraProduct == "y" && wantExtraProduct != "n");
               
            }
            catch(Exception e)
            {
                Console.WriteLine("Error : unable to add a product to the bill..");
            }
            }

        public void GenerateBill()
        {
            Console.WriteLine("\n\n");
            Console.WriteLine("------------------------------------------------------------------------------------------------");
            Console.WriteLine("\t\t\tBill : ");
            Console.WriteLine("------------------------------------------------------------------------------------------------");

            Console.WriteLine($"{"\tProduct ID",-12}{"Product name",-20}{"price",-12}{"X",3}{"quantity",-8}{"=",-3}{"Total price",-10}");
            Console.WriteLine("------------------------------------------------------------------------------------------------");

            decimal total = 0;
            foreach (var product in selectedProducts)
            {
                decimal productId = product.Value[0];
                decimal price = product.Value[1];
                decimal quantity = product.Value[2];
                decimal totalPrice = price * quantity;
                total += totalPrice;
                Console.WriteLine($"\t{productId,-12}{product.Key,-20}{price,-12}{"X",3}{quantity,-8}{"=",-3}{totalPrice,-10}");
            }
            Console.WriteLine("------------------------------------------------------------------------------------------------");
            Console.WriteLine($"\tTotal Amount : {total}");
            Console.WriteLine("------------------------------------------------------------------------------------------------");
            Console.WriteLine("\n\n");
            Console.Write("if you want to modify a bill then enter (y/n) : ");
            string wantToChange = InputValidation.GetYesOrNoForProceed();
            if( wantToChange == "y")
            {
                UpdateBill();
            }
            else
            {
                foreach(var product in selectedProducts)
                {
                    int productId = (int)product.Value[0];
                    int quantity = (int)product.Value[2];
                    Products matchedProduct = InputValidation.GetMatchedProduct(productId, products);
                    if (matchedProduct != null)
                    {
                        matchedProduct.Quantity -= quantity;
                    }
                }
                WriteInTextFile(); // rewrite the file to update the quantity of the products
                WriteInBillFile(total);
            }
        }

        public void UpdateBill()
        {
            int choice;
            do
            {
                Console.WriteLine("1. Add product\n2. Remove product\nn3. Update quantity\n4. Exit");
                choice = InputValidation.GetOption(4);
                switch (choice)
                {
                    case 1:
                        //AddProduct();
                        int productId = InputValidation.GetProductId();
                        decimal quantity;
                        Products product = InputValidation.GetMatchedProduct(productId, products);
                        product.DisplayProductDetails();
                        if (selectedProducts.ContainsKey(product.ProductName))
                        {
                            Console.WriteLine("This product is already in the bill");
                            string wantMore = InputValidation.GetYesOrNoForProceed();
                            if(wantMore == "y")
                            {
                                Console.WriteLine("Enter a new quantity......");
                                quantity = InputValidation.GetQuantity();
                                selectedProducts[product.ProductName][2] += quantity;
                            }
                            else
                            {
                                Console.WriteLine("Extra quantity is not added...");
                            }
                        }
                        else
                        {
                            quantity= InputValidation.GetQuantity();
                            while (quantity > product.Quantity)
                            {
                                Console.WriteLine($"There is no enough quantity of this product... only {product.Quantity} available");
                                quantity = InputValidation.GetQuantity();
                            }
                            selectedProducts.Add(product.ProductName, new decimal[] { productId, product.UnitPrice, quantity });
                        }
                            break;
                    case 2:
                         productId = InputValidation.GetProductId();
                        string keyValue = selectedProducts.FirstOrDefault(data => data.Value[0] == productId).Key;
                        while (keyValue == null)
                        {
                            Console.Write("Enter a valid product Id : ");
                            productId = InputValidation.GetProductId();
                        }
                        selectedProducts.Remove(keyValue);
                        break;
                    case 3:
                         productId = InputValidation.GetProductId();
                         keyValue = selectedProducts.FirstOrDefault(data => data.Value[0] == productId).Key;
                        decimal price = selectedProducts[keyValue][1];
                         quantity = selectedProducts[keyValue][2];
                        decimal totalPrice = price * quantity;
                        Console.WriteLine($"{productId,-15}{keyValue,-20}{price,-15}X{quantity,-8}={totalPrice,-10}");
                        Console.Write("Enter a new quanitity : ");
                        int quanitity = InputValidation.GetQuantity();
                        selectedProducts[keyValue][2] = quanitity;
                        break;
                    case 4:
                        Console.WriteLine("Exiting.......");
                        GenerateBill();
                        break;
                    default:
                        Console.WriteLine("Invalid choice....");
                        break;
                }
            } while (choice != 4);
        }


        public void WriteInBillFile(decimal total)
        {
            try
            {
                string filePath = Path.Combine(InventoryManagementDirecotry, "Sales details.txt");
                int transactionNumber = new Random().Next(1, 101);
                using (StreamWriter writer = new StreamWriter(filePath,true))
                {
                    writer.WriteLine($"Transaction no:{transactionNumber}| User name:{UserName}  | User ID:{UserId}");
                    writer.WriteLine($"{"Product ID",-12}|{"Product name",-20}|{"price",-15}|{"quantity",-12}");
                    foreach (var product in selectedProducts)
                    {
                        writer.WriteLine($"{product.Value[0],-12}|{product.Key,-20}|{product.Value[1],-15}|{product.Value[2],-12}");
                    }
                    writer.WriteLine($"Total amount : {total}"); 
                    writer.WriteLine("Date and time :"+DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
                }

                string userFilePath = $@"D:\Azure-Assignment\C#-Tasks\C#-Tasks\ConsoleApplication\Inventory Management System\Data files\User transaction file\{UserName.Trim()}.txt";
                using (StreamWriter writer = new StreamWriter(userFilePath, true))
                {
                    writer.WriteLine($"Transaction no : {transactionNumber}");
                    writer.WriteLine($"{"Product ID",-12}|{"Product name",-20}|{"price",-15}|{"quantity",-12}");
                    foreach (var product in selectedProducts)
                    {
                        writer.WriteLine($"{product.Value[0],-12}|{product.Key,-20}|{product.Value[1],-15}|{product.Value[2],-12}");
                    }
                    writer.WriteLine($"Total amount : {total}");
                    writer.WriteLine("Date and time :" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Error : unable to find a file..");
                InputValidation.LogError(e);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : unable to write into a file..");
                InputValidation.LogError(e);
            }
        }

    }
}

