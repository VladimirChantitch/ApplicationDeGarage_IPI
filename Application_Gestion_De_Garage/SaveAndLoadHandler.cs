using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Gestion_De_Garage
{
    internal class SaveAndLoadHandler
    {
    }

    #region helper data struct
    //Are going to be used for save and load
    [System.Serializable]
    public struct VehicleData
    {
        public string Name;
        public decimal priceHT;
        public brand_enum brand;
        public Motor motor;
        public List<Option> options;
    }
    [System.Serializable]
    public struct MotorData
    {
        public string Name;
        public int power;
        public decimal price;
        public motor_type motortype;
    }
    [System.Serializable]
    public struct OptionData
    {
        public string Name;
        public decimal price;
    }
    [System.Serializable]
    public struct CarData
    {
        public VehicleData vehicleData;
        public int taxHorsePower;
        public int doorNumber;
        public int sitsNumber;
        public int carTrunkSize;
    }
    [System.Serializable]
    public struct TruckData
    {
        public VehicleData vehicleData;
        public int axle;
        public int weight;
        public int volume;
    }
    [System.Serializable]
    public struct MotoData
    {
        public VehicleData vehicleData;
        public int cylinders;
    }

    #endregion
}
