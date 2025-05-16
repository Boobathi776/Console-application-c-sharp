
namespace Tasks;

 class SuperMarketDynamic
{
    //User selected products
    Dictionary<string, decimal[]> selectedProducts= new Dictionary<string, decimal[]>();

    //The user defined product details
    SortedDictionary<int, string[]> productDetails = new SortedDictionary<int, string[]>();

    //pre defined set of products 
    SortedDictionary<int, string[]> productDetailsForUser = new SortedDictionary<int, string[]>();

    public void GetProducts()
    {
        string productName;
        int price;
        int productId;
        string[] products;
        do
        {
            Console.Write("Enter the product name or enter \"exit\" to Close  : ");
            productName = Console.ReadLine().Trim();
            if(productName == "exit") break;
            price = GetPrice();
            productId = GetProductId();
            products = new string[] { productName ?? "Product", price.ToString() };
            productDetails.Add(productId, products);
        } while (productName != "exit");
        Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
        ShowProducts(productDetails);
        Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
        UserSelectingProducts(productDetails); // calling the purchasing method to switch to User mode to purchase
    }

    public int GetPrice()
    {
        Console.Write("Enter the product price : ");
        int price;
        bool c = int.TryParse(Console.ReadLine(), out price);
        if (c) return price;
        else 
        { 
            Console.WriteLine("Please enter a valid price...... ");
            return GetPrice();
        }
    }

    public int GetProductId()
    {
        Console.Write("Enter the product ID : ");
        int productId;
        bool c = int.TryParse(Console.ReadLine(), out productId);
        if (c) return productId;
        else
        {
            Console.WriteLine("Please enter a valid price...... ");
            return GetProductId();
        }
    }

    //This method is used to show the products that we have in the store 
    public void ShowProducts(SortedDictionary<int, string[]> products)
    {
        Console.WriteLine("These are the Products that we have : ");
        Console.WriteLine("\n-------------------------------------------------------------------------------\n");
        Console.WriteLine("Product ID\tProduct Name\tProduct Price");
        Console.WriteLine("\n-------------------------------------------------------------------------------\n");
        foreach (var item in products)
        {
            Console.WriteLine($"{item.Key}\t\t{item.Value[0]}\t\t\t{item.Value[1]}");
        }
        Console.WriteLine("Please select the product ID that you want to buy : ");
    }

    public void Purchasing()
    {
      
        Console.Write("Enter you are Admin or User :");
        string value = Console.ReadLine();
        if (value == "admin")
        {
            //Console.ReadLine();
            GetProducts();
        }
        else
        {
            productDetailsForUser = new SortedDictionary<int, string[]>
            {
               {1,new string[]{ "Milk", "30" } },
               {2, new string[] { "Egg", "40" } },
               {3, new string[] { "Bread", "20" } },
               {4,new string[] {"Butter", "50" } },
               {5,new string[] {"Lemon","10"} },
               {6,new string[] {"Sugar","20"} },
               {7,new string[] {"Salt","10"} },
               {8,new string[] {"Rice","50"} },
               {9,new string[] {"Pulses","40"} },
               {10,new string[] {"Vegetables","30"} }
        };
            }
        Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
        ShowProducts(productDetailsForUser);
        Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
        UserSelectingProducts(productDetailsForUser);

    }

    private int GetProductIdInList()
    {
        int productId;
        bool isSuccess = int.TryParse(Console.ReadLine(), out productId);
        if (isSuccess && productId == 0) return 0;
        else if (isSuccess && productDetailsForUser.ContainsKey(productId)) return productId;
        else
        {
            Console.WriteLine("Please enter a valid Product Id...... ");
            return GetProductIdInList();
        }
    }

    public void UserSelectingProducts(SortedDictionary<int,string[]> productDetails)
    {
        int productId = 0;
        
        Console.Write("Enter the product ID that you want to buy : ");
        productId = GetProductIdInList();
        do
        {
            if ( productId > 0 && productId <= productDetails.Count)
            {
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine($"The product you selected is :{productDetails[productId][0]} \nThe price per Unit is : {productDetails[productId][1]}");
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");

                int quantity; decimal price;
            Again3:
                Console.Write("Enter the quantity that you want to buy : ");
                int.TryParse(Console.ReadLine(), out quantity);
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");

                if (quantity > 0)
                {
                    price = Convert.ToDecimal(productDetails[productId][1]);
                    BillStore(productDetails[productId][0], quantity, price);
                }
                else
                {
                    Console.WriteLine("Please enter a valid quantity : ");
                    goto Again3;
                }
            }
            if (productId != 0)
            {
                Console.Write("Enter the product ID that you want to buy or enter 0 to Generate \"Bill\" : ");
                productId = GetProductIdInList();
            }
        } while (productId != 0);
        if (selectedProducts.Count == 0) 
            Console.WriteLine("You didn't select anything....");
        else
            GenerateBill();
    }


    public void BillStore(string product, int quantity, decimal price)
    {
        try
        {
            decimal total = quantity * price;
            selectedProducts.Add(product, new decimal[] { quantity, total });
        }
        catch (Exception e) // what if the product is already in the dictionary
        {
            Console.WriteLine($"You Add the extra {product} in your Cart......");
           
            selectedProducts[product][0] = selectedProducts[product][0] + quantity;
      
            selectedProducts[product][1] = selectedProducts[product][1] + (quantity * price);
        }

    }

    public void GenerateBill()
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        decimal totalCost = 0;
        foreach (var item in selectedProducts)
        {
            totalCost += item.Value[1];
        }
        //Console.WriteLine($"Total cost is {totalCost}");
        Console.WriteLine("******************************************");
        foreach (var item in selectedProducts)
        {
            Console.WriteLine($"\t{item.Key} \tX {item.Value[0]}  = {item.Value[1]}");
        }
        Console.WriteLine("******************************************");
        Console.WriteLine($"\tTotal cost is {totalCost}");
        Console.WriteLine("******************************************");
        Console.WriteLine("Thank you for shopping with us");
    }
}
