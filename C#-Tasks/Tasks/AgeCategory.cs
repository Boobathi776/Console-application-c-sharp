using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks;
class AgeCategory
{
    public void checkAge()
    {
        Console.WriteLine("Age category checker");
        int age;
        do
        {
        age:
            Console.Write("Enter your age or 0 to exit : ");
            int.TryParse(Console.ReadLine(), out age);
            if (age > 0 && age < 122)
            {
                string category = age < 13 ? "Child" : age >= 13 && age <= 17 ? "Teen" : age >= 18 && age <= 59 ? "Adult" : "Senior";
                Console.WriteLine($"According to your age your category is {category}");
                Console.WriteLine("\n--------------------------------------------------------------------------------------------------");
            }
            else if (age == 0) break;
            else
            {
                Console.WriteLine("I think you enter a wrong age of yours chect it");
                goto age;
            }
        } while (age != 0);
    }
}