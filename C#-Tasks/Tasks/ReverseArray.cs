using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks;
class ReverseArray
{
    int[] arrayOfNumbers = { };
    int number;
    public void getValue()
    {
        Console.WriteLine("Enter the number of values that you want to store in array : ");
        int number;
    GetNumber:
        int.TryParse(Console.ReadLine(), out number);
        arrayOfNumbers = new int[number];
        if (number > 0)
        {
            for (int i = 0; i < number; i++)
            {
                Console.Write($"Enter no {i + 1} : ");
                int.TryParse(Console.ReadLine(), out number);
                arrayOfNumbers[i] = number;
            }
        }
        else
        {
            Console.Write("Enter a valid number : ");
            goto GetNumber;
        }
    }

    public void reverse()
    {
        int left = 0;
        int right = arrayOfNumbers.Length - 1;
        int mid = (left + right) / 2;
        for (int i = 0; i < mid; i++)
        {
            int temp = arrayOfNumbers[left];
            arrayOfNumbers[left] = arrayOfNumbers[right];
            arrayOfNumbers[right] = temp;
            left++;
            right--;
        }
    }

    public void printValue()
    {
        foreach (int number in arrayOfNumbers)
        {
            Console.WriteLine(number);
        }
    }

    public void average()
    {
        double total = 0;
        double avg;
        foreach (int i in arrayOfNumbers)
        {
            total += i;
        }
        Console.WriteLine(total);
        avg = (double)(total / arrayOfNumbers.Length);
        Console.WriteLine($"The average of given numbers is : {avg}");
    }
}
