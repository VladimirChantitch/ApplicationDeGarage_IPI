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

        public Moto(MotoData motoData) : base(motoData.vehicleData)
        {
            cylinders = motoData.cylinders;
        }

        public override Data GetData()
        {
            List<OptionData> opDatas = new List<OptionData>();
            options.ForEach(op => opDatas.Add(op.GetData()));

            MotorData motorData = VehicleMotor.GetData();
            return new MotoData()
            {
                vehicleData = new VehicleData()
                {
                    Name = Name,
                    priceHT = PriceHT,
                    brand = Brand,
                    options = opDatas,
                    motor = motorData
                },
                cylinders = cylinders
            };
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
