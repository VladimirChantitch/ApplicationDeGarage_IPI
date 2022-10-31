using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Gestion_De_Garage
{
    public class Motor
    {
        public Motor(string name, int power, decimal price, motor_type motor_Type)
        {
            id = Incrementator.MotorIncrement;
            this.name = name;
            this.power = power;
            this.price = price;
            this.motor_Type = motor_Type;
        }

        private int id;
        public int Id { get { return id; } }
        private string name;
        public string Name { get { return name; } set { name = value; } }
        private int power;
        public int Power { get { return power; } set { power = value; } }   
        private decimal price;
        public decimal Price { get { return price; } set { price = value; } }
        private motor_type motor_Type;
        public motor_type Motor_Type { get { return motor_Type; } set { motor_Type = value; } }

        public void Show()
        {
            PromptHelper.PromptSubSubTitle("Here is a Motor");
            Console.WriteLine($"The name is == {name}");
            Console.WriteLine($"The power is == {power}");
            Console.WriteLine($"The price is == {price}");
        }

        public void Show(bool withIds = false)
        {
            PromptHelper.PromptSubSubTitle("Here is a Motor");
            if (withIds) Console.WriteLine($"ID ====== {id}");
            Console.WriteLine($"The name is == {name}");
            Console.WriteLine($"The power is == {power}");
            Console.WriteLine($"The price is == {price}");
        }
    }
}
