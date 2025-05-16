using Tasks.Helpers;
namespace Tasks.ConsoleApplication;
//This class is the template for the product 
internal class Product
{
    private string _productName;
    private decimal _productPrice;
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public Product(string name, decimal price)
    {
        ProductName = name;
        ProductPrice = price;
    }
}

internal class SuperMarketBillingSystem
{
    // details get from the user
    SortedDictionary<decimal, Product> products = new SortedDictionary<decimal, Product>();
    // prducts selected by the user
    Dictionary<string, decimal[]> selectedProduct = new Dictionary<string, decimal[]>();
    //predefined products
    SortedDictionary<decimal, Product> definedProductDetails = new SortedDictionary<decimal, Product>() {
                {1,new Product("Milk",40) },
                {2,new Product("bread",25) },
                {3,new Product("curd",10) },
                {4,new Product("Eggs",40) },
                {5,new Product("Apple",80) },
                {6,new Product("Orange",90) },
                {7,new Product("Rice",55) },
                {8,new Product("Tomato",30) },
                {9,new Product("Chicken",250) },
                {10,new Product("Fish",200) },
                {11,new Product("Dal",90) },
                {12,new Product("Chips",20) }
            };


   

    /*This is the starting method of the program*/
    /// <summary>
    /// 
    /// </summary>
    public void Purchasing()
    {
        Console.WriteLine("**************************************************************");
        Console.WriteLine("**********      Welcome to the Super Market      *************");
        Console.WriteLine("**************************************************************");

        Console.Write("Enter user name : ");
        string password = Console.ReadLine() ?? "user";
        Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
        if (password == "admin")
        {
            var productDetails = GetProduct();
            do
            {
                ShowProduct(productDetails);
                UserSelectingProducts(productDetails);
                Console.Write("Do you want to Generate bill for another client  ? (y/n) : ");
            } while (Console.ReadLine() == "y");
        }
        else
        {
            Console.WriteLine($"Welcome {password}");
            string need;
            do
            {
                ShowProduct(definedProductDetails);
                UserSelectingProducts(definedProductDetails);
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
                Console.Write("Do you want to Generate bill for another client  ? (y/n) : ");
            GetNeed:
                need = Console.ReadLine();
                if (need == "n") break;
                else if (need == "y") { }
                else { Console.Write("Enter a valid option (y/n) : "); goto GetNeed; }
            } while (need == "y");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");

        }

        Console.WriteLine("Thank you for visiting the Super Market");
        Console.WriteLine("------------------------------------------------------------------------------------------------------\n");

        Console.Write("If you want to switch to another user or admin press 1 or 0 to exit : ");

        if (Console.ReadLine() == "1")
        {
            Console.Clear();
            Purchasing();
        }
        else
        {
            Console.WriteLine("Thank you for visiting the Super Market");
        }
    }


    /// <summary>
    /// Show the products that admin entered or the predefined set of products based on the selection
    /// For a unique key  use the  product id as a key
    /// </summary>
    /// <param name="products"> sorted dictionary collection </param>
    public void ShowProduct(SortedDictionary<decimal, Product> products)
    {
        Console.WriteLine("--------------------------------------------------------------");
        Console.WriteLine("\t\tProduct List : ");
        Console.WriteLine("--------------------------------------------------------------");
        Console.WriteLine("\tProduct Id\tProduct Name\tProduct Price");
        Console.WriteLine("--------------------------------------------------------------");
        foreach (var product in products)
        {
            Console.WriteLine($"\t{product.Key}\t\t{product.Value.ProductName}\t\t{product.Value.ProductPrice}");
        }
        Console.WriteLine("--------------------------------------------------------------");
    }


    /// <summary>
    /// This is method is used to get the product details from the admin panel . so we can make our own list to 
    /// meke bills
    /// </summary>
    /// <returns> sorted dictioanry of products collection </returns>
    public SortedDictionary<decimal, Product> GetProduct()
    {
        string product;
        decimal price;
        decimal productId;
        do
        {
            Console.Write("Enter the product Id or Enter 0 to stop Adding: ");
            productId = GetInputForProduct();
            if (productId != 0)
            {
                Console.Write("Enter the product name : ");
                product = Console.ReadLine() ?? "invalid";

                while (product == "invalid")
                {
                    Console.WriteLine("Enter a valid a value : ");
                    product = Console.ReadLine() ?? "invalid"; // Again get input from the user to get a valid value
                }

                if (product != "invalid" && product != null)
                {
                    Console.Write("Enter product price : ");
                    price = GetInputForProduct();
                    try
                    {
                        products.Add(productId, new Product(product, price));
                        Console.WriteLine("the product is added to the list....");
                        Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine($"Sorry .. the product id is already present Enter a unique id for this product \"{product}\"");
                        GetProduct();
                    }
                }
            }
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
        } while (productId != 0);
        return products;
    }

    /// <summary>
    /// Get the input for the "Product ID" and "Product price"
    /// </summary>
    /// <returns>product id or product price </returns>
    public decimal GetInputForProduct()
    {
        decimal number;
        bool isSuccess;
        isSuccess = decimal.TryParse(Console.ReadLine(), out number);
        if (isSuccess == true)
            return number;
        else
        {
            Console.Write("Enter a valid value : ");
            return GetInputForProduct();
        }
    }


    /// <summary>
    /// The user select the product by giving product id and quantity for that particular product.
    /// This product is added in the separate dicitonary to generate bill.
    /// </summary>
    /// <param name="productDetails">sortedDicitionary collection for selected products </param>
    public void UserSelectingProducts(SortedDictionary<decimal, Product> productDetails)
    {
        decimal productId = 0;

        do
        {
            Console.Write("Enter the \"product ID\" that you want to buy or 0 to \"exit\": ");
        GetID:
            bool c = decimal.TryParse(Console.ReadLine(), out productId);
            if (c && productId > 0 && productId <= productDetails.Count && productDetails.ContainsKey(productId))
            {
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine($"\tThe product you selected is :\t{productDetails[productId].ProductName} \n\tThe price per Unit is :\t{productDetails[productId].ProductPrice}");
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
                int quantity; decimal price;
                Console.Write("Enter the quantity that you want to buy : ");

            GetQuantity:
                bool isSuccess = int.TryParse(Console.ReadLine(), out quantity);
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
                if (isSuccess && quantity > 0)
                {
                    try
                    {
                        // Adding the user selected products to the Selected product dectionary
                        selectedProduct.Add(productDetails[productId].ProductName, new decimal[] { quantity, productDetails[productId].ProductPrice, productId });
                    }
                    catch (Exception e) // what if the product is already in the dictionary
                    {

                        Console.WriteLine($"You Add the extra {productDetails[productId].ProductName} in your Cart......");
                        Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
                        selectedProduct[productDetails[productId].ProductName][0] = selectedProduct[productDetails[productId].ProductName][0] + quantity;
                    }
                }
                else
                {
                    Console.Write("Please enter a valid quantity : ");
                    goto GetQuantity;
                }
            }
            else if (productId == 0) break;  // if user enter 0 it will break the loop 
            else
            {
                Console.Write("Enter the valid product ID  or enter 0 to Generate \"Bill\" : ");
                goto GetID;
            }
        } while (productId != 0);
        GenerateBill(selectedProduct);
        selectedProduct.Clear();
    }

    //Get the product Id from the user for select a particular product
    public int GetProductId()
    {
        //Console.Write("Enter the product ID : ");
        int productId;
        bool c = int.TryParse(Console.ReadLine(), out productId);
        if (c) return productId;
        else
        {
            Console.WriteLine("Please enter a valid product id...... ");
            return GetProductId();
        }
    }

    /// <summary>
    /// Generate a bill based on the selected product dictionary 
    /// </summary>
    /// <param name="selectedProduct"></param>
    public void GenerateBill(Dictionary<string, decimal[]> selectedProduct)

    {
        Console.WriteLine("\n\n");
        Console.WriteLine("--------------------------------------------------------------");
        Console.WriteLine("\t\t\tBill : ");
        Console.WriteLine("--------------------------------------------------------------");
        Console.WriteLine("Product ID\tProduct Name\tPrice\tQuantity\tTotal");
        Console.WriteLine("--------------------------------------------------------------");
        decimal total = 0;
        foreach (var product in selectedProduct)
        {
            decimal quantity = product.Value[0];
            decimal price = product.Value[1];
            decimal totalPrice = price * quantity;
            total += totalPrice;
            Console.WriteLine($"{product.Value[2]}\t\t{product.Key}\t \t {price}   X    {quantity}  \t = {totalPrice}");
        }
        Console.WriteLine("--------------------------------------------------------------");
        Console.WriteLine($"Total Amount : {total}");
        Console.WriteLine("--------------------------------------------------------------");
        Console.WriteLine("\n\n");
        Console.Write("if you want to modify a bill then enter (y/n) : ");
        char proceed;
    ModifyBill:
        bool isSuccess = char.TryParse(Console.ReadLine(), out proceed);
        if (proceed == 'y')
        {
            ModifyBill(selectedProduct);
        }
        else if (proceed == 'n')
        {
            selectedProduct.Clear();
        }
        else
        {
            Console.Write("Enter a valid option \"y\" or \"n\" : ");
            goto ModifyBill;
        }
    }

    /// <summary>
    /// If the user want to edit the bill 
    /// </summary>
    /// <param name="selectedProduct"></param>
    public void ModifyBill(Dictionary<string, decimal[]> selectedProduct)
    {
        int option;
        do
        {
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Choose the option:\n1.Remove product\n2.Add product\n3.Modify quantity\n4.Proceed to Bill\n0.Exit");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
            Console.Write("Enter your choice : ");
        GetChoice:
            int.TryParse(Console.ReadLine(), out option);
            if (option == 1)
            {
                Console.Write("Enter the product ID that you want to remove : ");
                int productId = GetProductId();
                string productName;
                productName = selectedProduct.FirstOrDefault(item => item.Value[2] == productId).Key;
                if (productName != null)
                {
                    selectedProduct.Remove(productName);
                    Console.WriteLine($"The product with ID {productId}-{productName} is removed from the bill.");
                }
                else
                {
                    Console.WriteLine($"The product with ID {productId} is not found in the bill.");
                }
            }
            else if (option == 2)
            {
                ShowProduct(definedProductDetails);
                UserSelectingProducts(definedProductDetails);
                break;
            }
            else if (option == 3)
            {
                Console.Write("Enter the product ID that you want to Modify : ");
                int productId = GetProductId();
                string productName;
                productName = selectedProduct.FirstOrDefault(item => item.Value[2] == productId).Key;
                if (productName != null)
                {
                    Console.Write("Enter the new quantity : ");
                    decimal quantity = GetInputForProduct();
                    selectedProduct[productName][0] = quantity;
                    Console.WriteLine($"The product with ID {productId}-{productName} is quantity changesd.");
                }
                else
                {
                    Console.WriteLine($"The product with ID {productId} is not found in the bill.");
                }
            }
            else if (option == 4)
            {
                Console.WriteLine("Proceeding to Bill......");
                GenerateBill(selectedProduct);
                break;
            }
            else if (option == 0) break;
            else
            {
                Console.Write("Enter a valid choice : ");
                goto GetChoice;
                //GenerateBill(selectedProduct);
            }
        } while (option != 0);
    }
}
