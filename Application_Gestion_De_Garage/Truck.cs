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

        public Truck(TruckData truckData) : base(truckData.vehicleData)
        {
            axle = truckData.axle;
            weight = truckData.weight;
            volume = truckData.volume;
        }
        public override Data GetData()
        {
            List<OptionData> opDatas = new List<OptionData>();
            options.ForEach(op => opDatas.Add(op.GetData()));

            MotorData motorData = VehicleMotor.GetData();
            return new TruckData()
            {
                vehicleData = new VehicleData()
                {
                    Name = Name,
                    priceHT = PriceHT,
                    brand = Brand,
                    options = opDatas,
                    motor = motorData
                },
                axle = axle,
                volume = volume,
                weight = weight
            };
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
