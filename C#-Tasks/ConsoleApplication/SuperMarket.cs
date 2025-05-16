
namespace Tasks;
class SuperMarket
{
    Dictionary<string, int[]> dict = new Dictionary<string, int[]>();

    public void ShowOption()
    {
        Console.WriteLine("\tProduct Id \tProducts");
        Console.WriteLine("\t1. \t\tMilk");
        Console.WriteLine("\t2. \t\tEgg");
        Console.WriteLine("\t3. \t\tBread");
        Console.WriteLine("\t4. \t\tProceed to bill");
        Console.Write("Enter your choice : ");
    }

    /// <summary>
    /// Get the option from user
    /// </summary>
    /// <returns>The user given input</returns>
    public int GetOption()
    {
        int option;
        bool c = int.TryParse(Console.ReadLine(), out option);
    Again:
        if (c && option <= 4 && option > 0)
        {
            return option;
        }
        else
        {
            Console.WriteLine("Please enter a valid option : ");
            goto Again;
        }
    }

    public void Purchasing()
    {
        ShowOption();
        int option = GetOption();
        int quantity, total;
        do
        {
            total = 1;
            quantity = 0;
            switch (option)
            {
                case 1:
                    Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine(" Seleceted product   : Milk \n unit price(1 litre) : 30 rs");
                    Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");

                Again1:
                    Console.Write("Enter how much you want : ");
                    int.TryParse(Console.ReadLine(), out quantity);
                    //Console.WriteLine($"{quantity} Litres of Milk Added to your cart");
                    Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");

                    if (quantity > 0)
                    {
                        total = quantity * 30;
                        BillStore("Milk", quantity, total);
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid quantity : ");
                        goto Again1;

                    }
                    Console.WriteLine();
                    ShowOption();
                    option = GetOption();

                    break;

                case 2:
                    Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine(" Seleceted product   : Egg \n unit price(1 packet) : 40 rs");
                    Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");

                Again2:
                    Console.Write("Enter how much you want : ");
                    int.TryParse(Console.ReadLine(), out quantity);
                    Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");

                    if (quantity > 0)
                    {
                        total = quantity * 40;
                        BillStore("Egg", quantity, total);
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid quantity : ");
                        goto Again2;
                    }
                    Console.WriteLine();
                    ShowOption();
                    option = GetOption();
                    break;

                case 3:
                    Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine(" Seleceted product   : Bread \n unit price(1 packet) : 20 rs");
                    Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");

                Again3:
                    Console.Write("Enter how much you want : ");
                    int.TryParse(Console.ReadLine(), out quantity);
                    Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");

                    if (quantity > 0)
                    {
                        total = quantity * 20;
                        BillStore("Bread", quantity, total);
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid quantity : ");
                        goto Again3;
                    }
                    Console.WriteLine();
                    ShowOption();
                    option = GetOption();
                    break;

                case 4:
                    Console.WriteLine("******************************************");
                    Console.WriteLine("Your Bill is : ");
                    GenerateBill();
                    break;

                default:
                    Console.WriteLine("Please enter a valid option");
                    ShowOption();
                    option = GetOption();
                    Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
                    break;
            }

        } while (option != 4);
        if (option == 4)
        {
            Console.WriteLine();
            Console.WriteLine("Thank you for shopping with us");
            Console.WriteLine("Your bill is generated");
            Console.WriteLine("******************************************");
            GenerateBill();
        }
    }

    /// <summary>
    /// Add the product to the dictionary
    /// </summary>
    /// <param name="product">The product name</param>
    /// <param name="quants">The quantity that user enters</param>
    /// <param name="amount">total amount cost for this particular product</param>
    public void BillStore(string product, int quants, int amount)
    {
        try
        {
            dict.Add(product, new int[] { quants, amount });
        }
        catch (ArgumentException e)
        {
            Console.WriteLine($"You Add the extra {product} in your Cart......");
            //Console.WriteLine(string.Join(", ", dict.Keys));
            dict[product][0] = dict[product][0] + quants;
            if (product == "Milk") dict[product][1] += quants * 30;
            else if (product == "Egg") dict[product][1] += quants * 40;
            else dict[product][1] += quants * 20;
            //dict[product][1] += dict[product][1] + amount; 

            Purchasing();
        }

    }

    public void GenerateBill()
    {
        decimal totalCost = 0;
        foreach (var item in dict)
        {
            totalCost += item.Value[1];
        }
        //Console.WriteLine($"Total cost is {totalCost}");
        Console.WriteLine("******************************************");
        foreach (var item in dict)
        {
            Console.WriteLine($"\t{item.Key} \tX {item.Value[0]}  = {item.Value[1]}");
        }
        Console.WriteLine("******************************************");
        Console.WriteLine($"\tTotal cost is {totalCost}");
        Console.WriteLine("******************************************");
    }
}


