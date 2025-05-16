using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks;
class GislenTwist
{
    public void print()
    {
        Console.WriteLine("GislenSoftware with a Twist");
        List<int> list = new List<int>();
        for (int i = 1; i <= 100; i++)
        {
            if (i % 3 == 0 && i % 5 == 0) Console.WriteLine($"{i} Gislen Software");
            else if (i % 3 == 0) Console.WriteLine($"{i} Gislen");
            else if (i % 5 == 0) Console.WriteLine($"{i} Software");
            else if (i % 7 == 0) list.Add(i);
        }
        Console.WriteLine("The values that divisible by 7 are : ");

        foreach (int number in list)
        {
            Console.Write(number + " ");
        }
    }
}