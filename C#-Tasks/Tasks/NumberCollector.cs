using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks;
class NumberCollector
{
    public void collecting()
    {
        string text;
        int number;
        bool isSuccess;
        List<int> numbers = new List<int>();
        Console.WriteLine(" Safe Number Collector");
        do
        {
        Doing:
            Console.Write("Enter a Number or \"exit\" : ");
            text = Console.ReadLine().Trim();
            if (text == "exit") break;
            else isSuccess = int.TryParse(text, out number);
            if (isSuccess)
                numbers.Add(number);
            else
            {
                Console.WriteLine("Enter a valid value...");
                goto Doing;
            }
        } while (true);

        int[] collectedNumbers = new int[numbers.Count];
        int i = 0;
        foreach (int n in collectedNumbers)
        {
            collectedNumbers[i] = n;
            i++;
        }
        //Console.WriteLine(arr);
        foreach (int no in collectedNumbers)
        {
            Console.Write(no + " ");
        }
    }
}
