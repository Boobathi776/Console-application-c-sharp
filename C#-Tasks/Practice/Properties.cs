using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Practice;

//UsingProperty usingProperty = new UsingProperty();
//usingProperty.PrintPropertyName();
namespace Tasks.Practice
{
    internal class Properties
    {
        public string PropertyName { get; set; }
        public void Method()
        {
            int[] arr = new int[10];
            Console.WriteLine(arr.Length);
        }
    }

    public class UsingProperty
    {
        public void PrintPropertyName()
        {
            Properties properties = new Properties();
            properties.PropertyName = "Boobathi";
            Console.WriteLine(properties.PropertyName);
        }
    }
}
