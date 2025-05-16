using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.StyledXmlParser.Node;
using Tasks.ConsoleApplication.Inventory_Management_System.Models;
using Tasks.ConsoleApplication.Inventory_Management_System.Validators;
using Tasks.ConsoleApplication.Inventory_Management_System.Validators;

namespace Tasks.ConsoleApplication.Inventory_Management_System.Services
{

    internal class BillServices : InventoryServices
    {
        List<User> users = new List<User>();
        Dictionary<string, decimal[]> selectedProducts = new Dictionary<string, decimal[]>();

        public void BillService()
        {
            int choice;
            //ReadFromTextFile();
            do
            {
                Console.WriteLine("1. Select product\nn2. Update product\n3. Remove product\n4. Display products in Bill\n5. SearchProduct\n6. Category search\n7. Exit");
                choice = InputValidation.GetOption(7);
                switch (choice)
                {
                    case 1:
                        //SelectRequiredProducts();
                        break;
                    case 2:
                        //UpdateBill();
                        //WriteInTextFile();
                        break;
                    case 3:
                        //RemoveProductInBill();
                        //WriteInTextFile();
                        break;
                    case 4:
                        //DisplayProductsInBill(products);
                        //WriteInTextFile();
                        break;
                    case 5:
                        //SearchProduct();
                        break;
                    case 6:
                        //SearchByCategory();
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


        public void SelectRequiredProducts()
        {
            try
            {
                string wantExtraProduct = null;

                do
                {
                    int productId = InputValidation.GetProductId();
                    Products product = InputValidation.GetMatchedProduct(productId, products);
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
                    selectedProducts.Add(product.ProductName, new decimal[] { productId, product.UnitPrice, quantity });
                    string wantExtra = InputValidation.GetYesOrNoForProceed();
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
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("\t\t\tBill : ");
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Product ID\tProduct Name\tPrice\tQuantity\tTotal");
            Console.WriteLine("--------------------------------------------------------------");
            decimal total = 0;
            foreach (var product in selectedProducts)
            {
                decimal productId = product.Value[0];
                decimal price = product.Value[1];
                decimal quantity = product.Value[2];
                decimal totalPrice = price * quantity;
                total += totalPrice;
                Console.WriteLine($"{productId}\t\t{product.Key}\t \t {price}   X    {quantity}  \t = {totalPrice}");
            }
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine($"Total Amount : {total}");
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("\n\n");
            Console.Write("if you want to modify a bill then enter (y/n) : ");
            string wantToChange = InputValidation.GetYesOrNoForProceed();
            if( wantToChange == "y")
            {
                //UpdateBill();
            }
        }

        public void UpdateBill()
        {

        }
    }
}

