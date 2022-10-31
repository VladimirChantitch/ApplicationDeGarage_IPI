using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application_Gestion_De_Garage
{
    public class Car : Vehicle
    {
        private int taxHorsePower;
        private int doorNumber;
        private int sitsNumber;
        private int carTrunkSize;

        public Car(int taxHorsePower, int doorNumber, int sitsNumber, int carTrunkSize, string name, decimal priceHT, brand_enum brand, List<Option> options = null) : base(name, priceHT, brand, options)
        {
            this.taxHorsePower = taxHorsePower;
            this.doorNumber = doorNumber;
            this.sitsNumber = sitsNumber;
            this.carTrunkSize = carTrunkSize;
        }

        public override decimal CalcultateTax()
        {
            return taxHorsePower * 10;
        }

        public override void Show(bool showId = false)
        {
            base.Show(showId);
            Console.WriteLine($"The taxHorsePower is == {taxHorsePower}");
            Console.WriteLine($"The doorNumber is == {doorNumber}");
            Console.WriteLine($"The sitsNumber is == {sitsNumber}");
            Console.WriteLine($"The carTrunkSize is == {carTrunkSize}");
        }
    }
}
