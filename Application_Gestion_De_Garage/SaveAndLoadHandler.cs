using System;
using System.Collections.Generic;
using System.IO;
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

            try
            {
                string path = Directory.GetCurrentDirectory();
                Directory.CreateDirectory(path + @"\Saves");

                List<GarageData> garageDatas = new List<GarageData>();
                menuManager.GetGarageList().ForEach(garage =>
                {
                    garageDatas.Add(garage.GetData());
                });

                garageDatas.ForEach(garage =>
                {
                    File.WriteAllText(@$"{path}\Saves\{garage.name}.json", JsonConvert.SerializeObject(garage, settings));
                });
            }
            catch(Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }

            PromptHelper.PromptCongratulation("Congratulation !!! You've mange to save all of your garages");
            MenuInteractions.AwaitForUser();
        }

        public void LoadData(MenuManager menuManager)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, Formatting = Formatting.Indented };

            string path = Directory.GetCurrentDirectory();
            path = Directory.GetDirectories(path).ToList().Where(dir_path => dir_path.Contains("Saves")).ToList().First();

            if (path == null || !path.Contains("Saves"))
            {
                ExceptionHandler.HandleException(new Exception("Well no save file yet you have to create the data using the app"));
                return;
            }

            List<string> paths = Directory.GetFiles(path).ToList();

            paths.ForEach(p =>
            {
                try
                {
                    GarageData garageData = JsonConvert.DeserializeObject<GarageData>(File.ReadAllText(p), settings);
                    menuManager.AddGarage(new Garage(garageData));
                }
                catch(Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                }
            });

            PromptHelper.PromptCongratulation("Congratulation !!! You've mange to load all of your garages");
            MenuInteractions.AwaitForUser();
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
