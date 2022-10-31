using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Gestion_De_Garage
{
    public class Moto : Vehicle
    {
        private int cylinders;

        public Moto(int cylinders,string name, decimal priceHT, brand_enum brand, List<Option> options = null) : base(name, priceHT, brand, options)
        {
            this.cylinders = cylinders;
        }

        public override decimal CalcultateTax()
        {
            return (int)(cylinders * 0.3);
        }

        public override void Show(bool showId = false)
        {
            base.Show(showId);
            Console.WriteLine($"The cylinders is == {cylinders}");
        }
    }
}
