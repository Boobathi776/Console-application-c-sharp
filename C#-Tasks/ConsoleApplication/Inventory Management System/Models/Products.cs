using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.ConsoleApplication.Inventory_Management_System.Services;

namespace Tasks.ConsoleApplication.Inventory_Management_System.Models
{
    internal class Products
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public productCategory Category { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        public Products(int productId, string productName, productCategory category, decimal unitPrice, int quantity)
        {
            ProductID = productId;
            ProductName = productName;
            Category = category;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }
        public void DisplayProductDetails()
        {
            Console.WriteLine($"\t{ProductID,-12}{ProductName,-20}{Category,-15}{UnitPrice,-12}{Quantity,-8}");
        }

    }
}
