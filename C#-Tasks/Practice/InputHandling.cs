using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Practice
{
    internal class InputHandling
    {
        public void GetIntInput()
        {
            Console.WriteLine("Enter a number : ");
            string number = Console.ReadLine();
            int input;
            while(!(int.TryParse(number,out input) && input>0))
            {
                Console.Write("Enter a valid input :");
                number = Console.ReadLine();
            }
            Console.WriteLine(input);
        }

        public void GetValidOption()
        {
            Console.Write("ENter a option form 1 - 4");
            int option;
            while(!(int.TryParse(Console.ReadLine(),out option) && option > 0 && option <=4))
            {
                Console.Write("Enter a valid optino : ");
            }
            Console.WriteLine(option);
        }


        public void GetYesOrNo()
        {
            //Console.WriteLine("Enter a yes or no (y/r) : ");
            //bool isYes;
            //string Yes = Console.ReadLine().Trim().ToLower();
            //while(string.IsNullOrWhiteSpace(Yes))
            //{
            //    Console.Write("Enter y or n : ");
            //    Yes = Console.ReadLine();

            //}
            //while(!(Yes=="y" || Yes == "n"))
            //{
            //   Yes=  Console.ReadLine();
            //}
            //Console.WriteLine(Yes);

            string input;
            do
            {
                Console.Write("Enter 'y' or 'n': ");
                input = Console.ReadLine()?.Trim().ToLower();
            }
            while (input != "y" && input != "n");

            Console.WriteLine($"You entered: {input}");
        }

        public void GetString()
        {
            string name;
            name = Console.ReadLine();

            while(!(!string.IsNullOrWhiteSpace(name) && !int.TryParse(name,out _) && (name.Length >2)) )
            {
                Console.Write("enter a valid string : ");
                name = Console.ReadLine();
            }
            Console.Write(name);
        }
    }
}
