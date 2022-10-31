using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Gestion_De_Garage
{
    public class Option
    {
        public Option(string name, decimal price)
        {
            this.name = name;
            this.price = price;
            id = Incrementator.OptionIncrement;
        }

        private int id;

        private string name;
        public string Name { get { return name; } set { name = value; } }

        private decimal price; 
        public decimal Price { get { return price; } set { price = value; } }

        public void Show()
        {
            Console.WriteLine($"Option name = {Name}");
            Console.WriteLine($"Option price = {Price}");
        }
    }
}
