using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Gestion_De_Garage
{
    public static class PromptHelper
    {
        public static void PromptATitle(string title)
        {
            Console.Clear();
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine(title);
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine();
        }
        
        public static void PromptSubTitle(string subTitle)
        {
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine(subTitle);
            Console.WriteLine("--------------------------");
            Console.WriteLine();
        }

        public static void PromptSubSubTitle(string subTitle)
        {
            Console.WriteLine("*************************");
            Console.WriteLine(subTitle);
            Console.WriteLine("*************************");
        }

        public static void PromptWarning(string message)
        {
            Console.WriteLine();
            Console.WriteLine("!!!!!!!!!!!!");
            Console.WriteLine("!!! " + message + " !!!");
            Console.WriteLine();
        }

        public static void PromptCongratulation(string message)
        {
            Console.WriteLine();
            Console.WriteLine("------------ " + message + " -----------");
            Console.WriteLine();
        }

        public static void PromptOptionAddedSuccess(Vehicle vehicle)
        {
            Console.WriteLine();
            Console.WriteLine("Option successfully Added");
            Console.WriteLine($"The total price of your options is now {vehicle.GetOptionsTotalPrice()} euros");
        }

        public static void PromptOptionRemovedSuccess(Vehicle vehicle)
        {
            Console.WriteLine();
            Console.WriteLine("Option successfully Removed");
            Console.WriteLine($"The total price of your options is now {vehicle.GetOptionsTotalPrice()} euros");
        }
    }
}
