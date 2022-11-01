using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Application_Gestion_De_Garage
{
    public class SaveAndLoadHandler
    {
        string jsonString = "";
        public void SaveData(MenuManager menuManager)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, Formatting = Formatting.Indented };

            string path = Directory.GetCurrentDirectory();
            Directory.CreateDirectory(path + @"\Saves");

            menuManager.GetGarageList().ForEach(garage =>
            {
                File.WriteAllText(@$"{path}\{garage.Name}.json", JsonConvert.SerializeObject(garage, Formatting.Indented, settings));
                MenuInteractions.AwaitForUser();
            });
        }

        public void LoadData(MenuManager menuManager)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, Formatting = Formatting.Indented };

            string path = Directory.GetCurrentDirectory();
            List<string> paths = Directory.GetDirectories(path).ToList().Where(dir_path => dir_path.Contains("Saves")).ToList();

            if (paths == null || paths.Count <= 0)
            {
                ExceptionHandler.HandleException(new Exception("Well no save file yet you have to create the data using the app"));
                return;
            }
            else
            {
                paths.ForEach(p =>
                {
                    string jsonString = File.ReadAllText(p); // buffer to remove
                    GarageData garageData = JsonConvert.DeserializeObject<GarageData>(jsonString, settings);
                    menuManager.AddGarage(new Garage(garageData));
                });
            }
            
            var e = JsonConvert.DeserializeObject(jsonString);
            Console.WriteLine(e);
        }
    }

    #region helper data struct
    public class Data { }


    [System.Serializable]
    public class GarageData : Data
    {
        public string name;
        public List<Data> vehicles;
    }

    [System.Serializable]
    public class VehicleData : Data
    {
        public string Name;
        public decimal priceHT;
        public brand_enum brand;
        public MotorData motor;
        public List<OptionData> options;
    }
    [System.Serializable]
    public class MotorData : Data
    {
        public string Name;
        public int power;
        public decimal price;
        public motor_type motortype;
    }
    [System.Serializable]
    public class OptionData : Data
    {
        public string Name;
        public decimal price;
    }
    [System.Serializable]
    public class CarData : Data
    {
        public VehicleData vehicleData;
        public int taxHorsePower;
        public int doorNumber;
        public int sitsNumber;
        public int carTrunkSize;
    }
    [System.Serializable]
    public class TruckData : Data
    {
        public VehicleData vehicleData;
        public int axle;
        public int weight;
        public int volume;
    }
    [System.Serializable]
    public class MotoData : Data
    {
        public VehicleData vehicleData;
        public int cylinders;
    }

    #endregion
}

//CarData carData = new CarData()
//{
//    carTrunkSize = 12,
//    doorNumber = 4,
//    sitsNumber = 5,
//    taxHorsePower = 150,
//    vehicleData = new VehicleData()
//    {
//        brand = brand_enum.Peugeot,
//        Name = "alice coopper",
//        priceHT = 8000,
//        motor = new MotorData()
//        {
//            motortype = motor_type.Diesel,
//            Name = "pat pat",
//            power = 1562,
//            price = 4984,
//        },
//        options = new List<OptionData>() 
//        { 
//            new OptionData()
//            {
//                Name = "emilio",
//                price = 150
//            },
//            new OptionData()
//            {
//                Name = "toto",
//                price = 652
//            }
//        }
//    }          
//};

//GarageData garageData = new GarageData()
//{
//    name = "garage_name",
//    vehicles = new List<Data>()
//    {
//        carData,
//    }
//};
