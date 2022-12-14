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

        public Car(CarData carData) : base(carData.vehicleData)
        {
            taxHorsePower = carData.taxHorsePower;
            doorNumber = carData.doorNumber;
            sitsNumber = carData.sitsNumber;    
            carTrunkSize = carData.carTrunkSize;    
        }

        public override Data GetData()
        {
            List<OptionData> opDatas = new List<OptionData>();
            options.ForEach(op => opDatas.Add(op.GetData()));

            MotorData motorData = VehicleMotor.GetData();
            return new CarData()
            {
                vehicleData = new VehicleData()
                {
                    Name = Name,
                    priceHT = PriceHT,
                    brand = Brand,
                    options = opDatas,
                    motor = motorData
                },
                carTrunkSize = carTrunkSize,
                doorNumber = doorNumber,
                sitsNumber = sitsNumber,
                taxHorsePower = taxHorsePower
            };
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
