using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Practice
{
 class Vehicle
    {
        int speed;
        //public abstract void Stop();
        public void Drive()
        {
            Console.WriteLine("Driving at speed ");
        }

    }

    internal class InterfaceTry : Vehicle
    {

        //this method will get called based on the reference type of the instance
        public void Drive()
        {
            int a = 10;
            Console.WriteLine(@$"{a}i\n am derived class");
        }
    }
}

