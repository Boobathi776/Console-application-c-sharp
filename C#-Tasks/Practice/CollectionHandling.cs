using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Practice
{
    internal class CollectionHandling
    {
        List<string> list = new List<string>()
        {
            //"boobathi","santhosh","durai"
            "boobathi","bathi","bharathi","bathi","Balaji"

        };

        public void Correct()
        {
            var AllBoobathi = list.FirstOrDefault(val => val == "tree");
            Console.WriteLine(AllBoobathi.ToString());
            foreach(var item in AllBoobathi)
            {
                Console.WriteLine(item);
            }
            if(list.Any(value => value == "boobathi"))
                {
                Console.WriteLine("naan ulla thaan irukken");
            }
            else
            {
                Console.WriteLine("naan illa ulla");
            }
        }
    }
}
