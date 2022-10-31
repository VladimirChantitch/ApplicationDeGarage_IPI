using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Gestion_De_Garage
{
    public class Truck : Vehicle
    {
        private int axle;
        private int weight;
        private int volume;

        public Truck(int axle, int weight, int volume, string name, decimal priceHT, brand_enum brand, List<Option> options = null) : base(name, priceHT, brand, options)
        {
            this.axle = axle;
            this.weight = weight;   
            this.volume = volume;
        }

        public override decimal CalcultateTax()
        {
            return axle * 50;
        }

        public override void Show(bool showId = false)
        {
            base.Show(showId);
            Console.WriteLine($"The axle is == {axle}");
            Console.WriteLine($"The weight is == {weight}");
            Console.WriteLine($"The volume is == {volume}");
        }
    }
}
