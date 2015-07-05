using ConsoleApplication1.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Service1Client test = new Service1Client("BasicHttpBinding_IService1");
            //Double result = new Double();
            double result = test.Add(23.3, 3566);
            Console.WriteLine("The result of 23.3 + 3566 = " + result);
            Console.ReadLine();
        
        }
    }
}
