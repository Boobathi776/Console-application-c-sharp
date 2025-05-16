using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using iText.Forms.Fields;
using Tasks.ConsoleApplication.Inventory_Management_System.Interfaces;
using Tasks.ConsoleApplication.Inventory_Management_System.Models;
using Tasks.ConsoleApplication.Inventory_Management_System.Validators;
using Tasks.ConsoleApplication.Library_Book_management_system.Models;

namespace Tasks.ConsoleApplication.Inventory_Management_System.Services
{
    public enum productCategory
    {
        Electronics=1,
        Groceries=2,
        Clothing=3,
        HomeAppliances=4,
        Furniture=5,
        Stationery=6,
        HealthCare=7,
        Toys=8,
        Automotive=9,
        Accessories=10,
        Others=11
    }
    internal class InventoryServices : IInventoryService
    {
        public List<Products> products = new List<Products>();
        string InventoryManagementDirecotry = ConfigurationManager.AppSettings["inventoryPath"];

        public void InventoryService()
        {
            int choice;
            ReadFromTextFile();
            do
            {
                Console.WriteLine("1. Add product\n2. Update product\n3. Remove product\n4. Display products\n5. SearchProduct\n6. Category search\n7. Exit");
                choice = InputValidation.GetOption(7);
                switch (choice)
                {
                    case 1:
                        AddProduct();
                        break;
                    case 2:
                        UpdateProductDetails();
                        WriteInTextFile();
                        break;
                    case 3:
                        RemoveProduct();
                        WriteInTextFile();
                        break;
                    case 4:
                        DisplayProducts(products);
                        //WriteInTextFile();
                        break;
                    case 5:
                        SearchProduct();
                        break;
                    case 6:
                        SearchByCategory();
                        break;
                    case 7:
                        Console.WriteLine("Exiting.....");
                        break;
                    default:
                        Console.WriteLine("Invalid choice....");
                        break;
                }
            } while (choice != 7);
        }

        public void AddProduct()
        {
            try
            {
                int productId = InputValidation.GetProductId();
                while(products.Any(product=>product.ProductID == productId))
                {
                    Console.Write("Entered product id is already exist...so enter unique id for the product...");
                    productId = InputValidation.GetProductId();
                }
                string productName = InputValidation.GetProductName();
                productCategory category = InputValidation.GetCategory();
                decimal unitPrice = InputValidation.GetUnitPrice(productName);
                int quantity = InputValidation.GetQuantity();
                if (productId != 0 && productName != null && unitPrice != 0 && quantity != 0)
                {
                    products.Add(new Products(productId,productName,category,unitPrice,quantity));
                    using(StreamWriter writer = new StreamWriter(Path.Combine(InventoryManagementDirecotry,"Product details.txt"),true))
                    {
                        writer.WriteLine($"{productId,-12},{productName,-20},{category,-15},{unitPrice,-12},{quantity,-8}");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid details are entered....");
                    throw new Exception("The data are not okay to create a new object");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Error : unable to add a  product to the list..");
                InputValidation.LogError(e);
            }
        }

        public void UpdateProductDetails()
        {
            try
            {
                Console.Write("Enter product Id :");
                int productId = InputValidation.GetProductId();

                var matchedProduct = InputValidation.GetMatchedProduct(productId, products);
                if (matchedProduct != null)
                {
                    int choice;
                    do
                    {
                        Console.WriteLine("1.Update Name\n2.Update Category\n3.update unit price\n4.Update quantity\n5.Exit");
                        choice = InputValidation.GetOption(5);
                        switch (choice)
                        {
                            case 1:
                                Console.WriteLine("Enter a new product name...");
                                matchedProduct.ProductName = InputValidation.GetProductName();
                                break;
                            case 2:
                                Console.WriteLine("Enter a new product category...");
                                matchedProduct.Category = InputValidation.GetCategory();
                                break;
                            case 3:
                                Console.WriteLine("Enter a new product unit price...");
                                matchedProduct.UnitPrice = InputValidation.GetUnitPrice(matchedProduct.ProductName);
                                break;
                            case 4:
                                Console.WriteLine("Enter a new product quantity...");
                                matchedProduct.Quantity = InputValidation.GetQuantity();
                                break;
                            case 5:
                                Console.WriteLine("Exiting........");
                                break;
                            default:
                                Console.WriteLine("Invalid choice....");
                                break;
                        }
                    } while (choice != 5);

                }
            }
            catch(Exception e)
            {
                InputValidation.LogError(e);
                Console.WriteLine("Error : unable to update the product details...");
            }
          
        }

        public void RemoveProduct()
        {
            try
            {
                Console.WriteLine("Enter a product id that you want to remove...");
                int productId = InputValidation.GetProductId();
                Products matchedProduct = InputValidation.GetMatchedProduct(productId,products);
                if (matchedProduct != null)
                {
                    Console.WriteLine($"Do you want to remove the product with ID :{matchedProduct.ProductID}  and TITLE : {matchedProduct.ProductName} from the products?");
                    string proceed = InputValidation.GetYesOrNoForProceed();
                    if(proceed!=null && proceed=="y")
                    {
                        Console.WriteLine("The product is removed from the list of productss successfully...");
                        products.Remove(matchedProduct);
                    }
                    else
                    {
                        Console.WriteLine("your product is not removed from the list of products...");
                    }
                }
                else
                {
                    Console.WriteLine($"There is no product available with the given id-{productId}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : unable to add a  product to the list..");
                InputValidation.LogError(e);
            }

        }

        public void DisplayProducts(List<Products> products)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n----------------------------------------------------------------------------------------------------------\n");
            Console.WriteLine($"\t{"Product ID",-12}{"Product name",-20}{"Category",-15}{"Unit price",-12}{"Quantity",-8}");
            Console.WriteLine("\n----------------------------------------------------------------------------------------------------------\n");
            Console.ResetColor();
            foreach(Products product in products)
            {
                if (product.Quantity == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    product.DisplayProductDetails();
                    Console.ResetColor();
                }
                else if(product.Quantity < 5)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    product.DisplayProductDetails();
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    product.DisplayProductDetails();
                    Console.ResetColor();
                }
            }
            Console.WriteLine("\n----------------------------------------------------------------------------------------------------------\n");
        }

        public void SearchProduct()
        {
            try
            {
                Console.Write("Enter a product Id or name : ");
                string productIdOrName = Console.ReadLine().ToLower();
                if (int.TryParse(productIdOrName, out int productId))
                {
                    Products product = InputValidation.GetMatchedProduct(productId, products);
                    Console.WriteLine("Display product details..");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
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
                }
                else
                {
                    productId = 0;
                    while (!products.Any(product => product.ProductName.ToLower().Contains(productIdOrName.ToLower())))
                    {
                        Console.Write("Invalid product name.. enter a valid product name :");
                        productIdOrName = Console.ReadLine();
                    }
                    List<Products> matchedProducts = products.Where(product => product.ProductName.ToLower().Contains(productIdOrName.ToLower())).ToList();
                    Console.WriteLine("\nThese are the products that matched with the given word\n");
                    DisplayProducts(matchedProducts);

                    //for billing
                    Console.WriteLine("Choose the product that you want from the above list of products by enter ID...");
                    productId = InputValidation.GetProductId();
                    Products matchedProduct = InputValidation.GetMatchedProduct(productId, products);
                    while (matchedProduct == null)
                    {
                        Console.WriteLine("Invalid product Id... so Enter a valid one..!");
                        productId = InputValidation.GetProductId();
                        matchedProduct = InputValidation.GetMatchedProduct(productId, products);
                    }
                    Console.WriteLine($"\t{"Product ID",-12}{"Product name",-20}{"Category",-15}{"Unit price",-12}{"Quantity",-8}");
                    matchedProduct.DisplayProductDetails();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: unable to display a particular product details");
                InputValidation.LogError(e);
            }
        }

        public void SearchByCategory()
        {
            try
            {
                List<Products> categorizedProducts = null;
                Console.WriteLine("AVAILABLE CATEGORIES........");
                productCategory inputProductCategory = InputValidation.GetCategory();
                var productCategories = Enum.GetValues(typeof(productCategory));

                foreach (productCategory category in productCategories)
                {
                    categorizedProducts = products.Where(product => product.Category == inputProductCategory).ToList();
                }
                if (categorizedProducts != null)
                { 
                   DisplayProducts(categorizedProducts);
                }
                else
                {
                    Console.WriteLine("No values found in this category...");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : unable to get a products based on their category..");
            }
        }


        public void WriteInTextFile()
        {
            try
            {
                string filePath = Path.Combine(InventoryManagementDirecotry, "Product details.txt");
                using(StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine($"{"Product ID",-12},{"Product name",-20},{"Category",-15},{"Unit price",-12},{"Quantity",-8}");
                    foreach (Products product in products)
                    {
                        writer.WriteLine($"{product.ProductID,-12},{product.ProductName,-20},{product.Category,-15},{product.UnitPrice,-12},{product.Quantity,-8}");
                    }
                }
            }
            catch(FileNotFoundException e)
            {
                Console.WriteLine("Error : unable to find a file..");
                InputValidation.LogError(e);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error : unable to write into a file..");
                InputValidation.LogError(e);
            }
        }

        public void ReadFromTextFile()
        {
            try
            {
                Console.WriteLine("started reading..");
                string filePath = Path.Combine(InventoryManagementDirecotry, "Product Details.txt");
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    int lineCount = 0;
                    while((line = reader.ReadLine()) != null)
                    {
                        if (lineCount == 0)
                        {
                            lineCount++;
                            continue;
                        }
                        string[] productDetails = line.Split(",",StringSplitOptions.RemoveEmptyEntries);
                        if (productDetails.Length > 0)
                        {
                            bool isIdConverted = int.TryParse(productDetails[0], out int productId);
                            bool isCategoryAvailable = false;

                            if (Enum.TryParse<productCategory>(productDetails[2], true, out productCategory category))  //**********************************//
                            {
                                isCategoryAvailable = true;
                            }
                            else
                            {
                                Console.WriteLine("There is no such category available");
                            }
                            bool isPriceConverted = decimal.TryParse(productDetails[3], out decimal price);
                            bool isQuanityConverted = int.TryParse((productDetails[4]), out int quantity);
                            if (isIdConverted && isCategoryAvailable && isPriceConverted && isQuanityConverted)
                            {
                                products.Add(new Products(productId, productDetails[1], category, price, quantity));
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
                Console.WriteLine("Reading ended..");
            }
            catch(DirectoryNotFoundException e)
            {
                Console.WriteLine("Error : Direcotry not exist... when i try to find a file for Read");
                InputValidation.LogError(e);
            }
            catch(FileNotFoundException e)
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
