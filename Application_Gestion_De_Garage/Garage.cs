using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using AppGarage;

namespace Application_Gestion_De_Garage
{
    public class Garage
    {
        public Garage(string name, List<Vehicle> vehicles)
        {
            this.name = name;
            if (vehicles != null)
            {
                this.vehicles = vehicles;
            }
        }

        public Garage(string name)
        {
            this.name = name;
        }

        public List<Vehicle> vehicles = new List<Vehicle>();

        private string name;
        public string Name { get { return name; } set { name = value; } }

        public decimal CalculateGarageValue()
        {
            decimal value = 0;
            vehicles.ForEach(vehicle =>
            {
                value += vehicle.CalculateTotalPrice();
            });
            return value;
        }

        public void AddAVehicle(Vehicle vehicle)
        {
            if (vehicle == null) return;
            if (vehicles == null) vehicles = new List<Vehicle>();
            vehicles.Add(vehicle);
        }

        public Vehicle GetVehicleByID(int id)
        {
            if (vehicles == null) return null;
            return vehicles.Where(vehicle => vehicle.Id == id).First();
        }

        public List<Vehicle> GetVehicleByName(string name)
        {
            if (vehicles == null) return new List<Vehicle>();
            return vehicles.Where(vehicle => vehicle.Name == name).ToList();
        }

        public List<Vehicle> GetVehiculesByBrand(brand_enum brand)
        {
            if (vehicles == null) return new List<Vehicle>();
            return vehicles.Where(vehicle => vehicle.Brand == brand).ToList();  
        }

        public void AddOptionsToVehicules(Option option, List<Vehicle> vehicles)
        {
            vehicles?.ForEach(vehicle => vehicle.AddOption(option));
        }

        public void RemoveOptionToVehicles(Option option, List<Vehicle> vehicles)
        {
            vehicles?.ForEach(vehicle => vehicle.RemoveOption(option));
        }

        public void Show(bool withIds = false)
        {
            ShowCars(withIds);
            ShowMoto(withIds);
            ShowTrucks(withIds);
        }

        public void Show(List<Vehicle> vehicles, bool withIds = false)
        {
            ShowCars(vehicles, withIds);
            ShowMoto(vehicles, withIds);
            ShowTrucks(vehicles, withIds);
        }

        public void ShowCars(bool withIds = false)
        {
            PromptHelper.PromptSubTitle("Let's take a look at the cars");
            if (vehicles != null)
            {
                vehicles.ForEach(vehicle =>
                {
                    if (vehicle is Car)
                    {
                        vehicle.Show(withIds);
                    }
                });
            }
        }

        public void ShowCars(List<Vehicle> vehicles, bool withIds = false)
        {
            PromptHelper.PromptSubTitle("Let's take a look at the cars");
            if (vehicles != null)
            {
                vehicles.ForEach(vehicle =>
                {
                    if (vehicle is Car)
                    {
                        vehicle.Show(withIds);
                    }
                });
            }
        }

        public void ShowTrucks(bool withIds = false)
        {
            PromptHelper.PromptSubTitle("Let's take a look at the trucks");
            if (vehicles != null)
            {
                vehicles.ForEach(vehicle =>
                {
                    if (vehicle is Truck)
                    {
                        vehicle.Show(withIds);
                    }
                });
            }
        }

        public void ShowTrucks(List<Vehicle> vehicles, bool withIds = false)
        {
            PromptHelper.PromptSubTitle("Let's take a look at the trucks");
            if (vehicles != null)
            {
                vehicles.ForEach(vehicle =>
                {
                    if (vehicle is Truck)
                    {
                        vehicle.Show(withIds);
                    }
                });
            }
        }

        public void ShowMoto(bool withIds = false)
        {
            PromptHelper.PromptSubTitle("Let's take a look at the motos");
            if (vehicles != null)
            {
                vehicles.ForEach(vehicle =>
                {
                    if (vehicle is Moto)
                    {
                        vehicle.Show(withIds);
                    }
                });
            }
        }

        public void ShowMoto(List<Vehicle> vehicles, bool withIds = false)
        {
            PromptHelper.PromptSubTitle("Let's take a look at the motos");
            if (vehicles != null)
            {
                vehicles.ForEach(vehicle =>
                {
                    if (vehicle is Moto)
                    {
                        vehicle.Show(withIds);
                    }
                });
            }
        }

        public void SortVehicles(Filter filter, Action<List<Vehicle>> action = null)
        {
            List<Vehicle> list_withFilter = new List<Vehicle>();
            list_withFilter.AddRange(vehicles);
            list_withFilter.ForEach(vehicle => { vehicle.SetFilter(filter); });
            list_withFilter.Sort();

            if (action != null) action.Invoke(list_withFilter);
            else Show(list_withFilter);
        }
    }
}
