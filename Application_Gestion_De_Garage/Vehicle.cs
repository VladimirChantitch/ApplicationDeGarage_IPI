using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Gestion_De_Garage
{
    public abstract class Vehicle : IComparable<Vehicle>
    {
        private int id;
        public int Id { get { return id; } }

        private string name;
        public string Name { get { return name; } set { name = value; } }

        private decimal priceHT;
        public decimal PriceHT { get { return priceHT; } set { priceHT = value; } }

        private brand_enum brand;
        public brand_enum Brand { get { return brand; }}

        private Motor vehicleMotor;
        public Motor VehicleMotor { get { return vehicleMotor;} set { vehicleMotor = value; } }

        protected List<Option> options = new List<Option>();

        private Filter filter = Filter.id; public void SetFilter(Filter filter) { this.filter = filter; }

        public Vehicle(string name, decimal priceHT, brand_enum brand, List<Option> options = null)
        {
            this.id = Incrementator.VehicleIncrement;
            this.name = name;
            this.priceHT = priceHT;
            this.brand = brand;
            if (options != null)
            {
                this.options = options;
            }
        }

        public Vehicle(string name, decimal priceHT, brand_enum brand, Motor vehicleMotor, List<Option> options = null)
        {
            this.id = Incrementator.VehicleIncrement;
            this.name = name;
            this.priceHT = priceHT;
            this.brand = brand;
            if (options != null)
            {
                this.options = options;
            }
            this.vehicleMotor = vehicleMotor;
        }

        public Vehicle(VehicleData vehicleData)
        {
            id = Incrementator.VehicleIncrement;
            name = vehicleData.Name;
            priceHT = vehicleData.priceHT;
            brand = vehicleData.brand;
            options = new List<Option>();
            vehicleData.options.ForEach(op => options.Add(new Option(op)));

            vehicleMotor = new Motor(vehicleData.motor);
        }

        public abstract Data GetData();

        public virtual void Show(bool withIds = false)
        {
            PromptHelper.PromptSubTitle($"Informations about {brand} :: {name}");
            if (withIds) Console.WriteLine($"ID ====== {id}");
            Console.WriteLine($"The brand is == {brand}");
            Console.WriteLine($"The name is == {name}");
            Console.WriteLine($"The price HT is == {priceHT}");
            Console.WriteLine($"The motor type is == {vehicleMotor?.Motor_Type}");
        }

        public void ModifyMotor(Motor newMotor)
        {
            vehicleMotor = newMotor;
        }

        public virtual void ShowOptions()
        {
            if (options != null)
            {
                PromptHelper.PromptSubTitle($"The vehicle named {name} of the brand {brand} has {options.Count} options which are as follow");
                options.ForEach(option =>
                {
                    option.Show();
                });
            }
        }

        public virtual void AddOption(Option option)
        {
            if (options == null) options = new List<Option>();

            try
            {
                if (options.Contains(option))
                {
                    throw (new Exception($"Well the option {option.Name} is already applied to the vehicle {Name}"));
                }
                else
                {
                    options.Add(option);
                    Console.WriteLine($"option {option.Name} added");
                    PromptHelper.PromptOptionAddedSuccess(this);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }

        public virtual void AddOptions(List<OptionData> new_options)
        {
            if (options == null) options = new List<Option>();
            new_options.ForEach(op => {
                options.Add(new Option(op));
                Console.WriteLine($"option {op.Name} added");
            });

            PromptHelper.PromptOptionAddedSuccess(this);
        }

        public virtual void RemoveOption(Option option)
        {
            if (options != null)
            {
                options.Remove(option);
                PromptHelper.PromptOptionRemovedSuccess(this);
            }
        }

        public virtual void RemoveOption(int _id)
        {
            RemoveOption(options.Where(option => option.Id == _id).ToList().First());
        }

        public virtual decimal GetOptionsTotalPrice() 
        {
            decimal price = 0;
            if (options != null)
            {
                options.ForEach(option =>
                {
                    price += option.Price;
                });
            }

            return price;
        }

        public abstract decimal CalcultateTax();

        public virtual decimal CalculateTotalPrice()
        {
            decimal totalPrice = 0;
            totalPrice += priceHT;
            totalPrice += VehicleMotor.Price;
            totalPrice += CalcultateTax();
            totalPrice += GetOptionsTotalPrice();
            
            return totalPrice;
        }

        public int CompareTo(Vehicle? other)
        {
            switch (other.filter)
            {
                case Filter.id:
                    if (Id < other.Id) return -1;
                    if (Id == other.Id) return 0;
                    if (Id > other.Id) return 1;
                    break;

                case Filter.name:
                    if (Name.First() < other.Name.First()) return -1;
                    if (Name.First() == other.Name.First()) return 0;
                    if (Name.First() > other.Name.First()) return 1;
                    break;

                case Filter.priceHT:
                    if (priceHT < other.PriceHT) return -1;
                    if (priceHT == other.PriceHT) return 0;
                    if (priceHT > other.PriceHT) return 1;
                    break;

                case Filter.price:
                    decimal Other_Price = other.CalculateTotalPrice();
                    decimal This_Price = CalculateTotalPrice();

                    if (Other_Price < This_Price) return -1;
                    else if (Other_Price == This_Price) return 0;
                    else return 1;
                    break;
            }
            return 0;
        }
    }
}
