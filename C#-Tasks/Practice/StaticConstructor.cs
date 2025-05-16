using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Practice;

//StaticConstructor obj = new StaticConstructor();
//obj.Display1();
//StaticConstructor.Display();

namespace Tasks.Practice
{


    internal class StaticConstructor
    {
        public int Name { get; set; }
        public static int Age { get; set; }

        //static constructor get called only once per execution whenever the user try to access the static memeber of this class the static constrcutor get called 
        //it wont get called each time the instance creation 
        static StaticConstructor()
        {
            Console.WriteLine("Static constructor called");
        }

        public static void Display()
        {

            Console.WriteLine("static void method");
        }

        public void Display1()
        {
            Console.WriteLine("non static void method");
        }
    }
}


