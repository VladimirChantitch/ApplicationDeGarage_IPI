using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Gestion_De_Garage
{
    public static class MenuInteractions
    {
        #region utilitary
        public static void ReallyWantToQuit(MenuManager menuManager, UserOptionsMenu menu)
        {
            PromptHelper.PromptWarning("Do you really want to quit ?");
            if (CheckYesNo())
            {
                menuManager.IsWorking = false;
            }
            else
            {
                menuManager.CurrentMenu = menu;
            }
        }

        public static void ShowHelp()
        {
            PromptHelper.PromptSubTitle("Here is a little recap of the differents menus you can use to run your garage");
            Parser.Instance.MenuManager.GetUserOptions().ForEach(option =>
            {
                Console.WriteLine();
                Console.WriteLine(option.MenuName + "             " + option.MenuDescription);
            });

            PromptHelper.PromptSubTitle("Those are the simple option, just use <beer> followed byt the option to do stuff");
            Parser.Instance.Get_1Arg_options().ForEach(no_a_ops =>
            {
                Console.WriteLine();
                Console.WriteLine(no_a_ops.option + "              " + no_a_ops.optionDescription);
            });

            PromptHelper.PromptWarning("Beware when you are building a garage or building a vehicle those commands are disbled and you have to arrive at the end of the process you are in");
            AwaitForUser();
        }

        public static void AwaitForUser()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }

        public static bool CheckYesNo()
        {
            string truth = " ";
            while (truth != "yes" || truth != "no")
            {
                Console.WriteLine("Yes/No");
                try
                {
                    if ((truth = Console.ReadLine().ToLower()) != null)
                    {
                        if (truth == "yes")
                        {
                            return true;
                        }
                        else if (truth == "no")
                        {
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                }
            }
            return false;
        }

        public static void GetAString<T>(out T result, string instruction = "")
        {
            if (instruction != "") PromptHelper.PromptSubSubTitle(instruction);
            bool isValid = false;
            string prompt = "";
            while (!isValid)
            {
                try
                {
                    prompt = Console.ReadLine();
                }
                catch(Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                }

                if (prompt != "")
                {
                    try
                    {
                        switch (true)
                        {
                            case true when typeof(T) == typeof(string):
                                result = (T)Convert.ChangeType(prompt, typeof(T));
                                isValid = true;
                                break;
                            case true when typeof(T) == typeof(decimal):
                                try
                                {

                                }
                                catch
                                {

                                }
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler.HandleException(ex);
                    }
                }
            }
        }

        public static void GetAParsable()
        {
            string line = "";
            while (!Parser.Instance.isParse(line))
            {
                try
                {
                   line = Console.ReadLine();
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                }
            }
        }
        #endregion
        #region Fonctionalities
        public static Garage GenerateTheGarage(MenuManager menuManager, string name = null, List<Vehicle> vehicles = null)
        {
            if (name == null)
            {
                GetAString(out name, "What is the name of your garage");
                if (menuManager.GetGarageList().Where(garage => garage.Name == name).ToList().Count > 0) PromptHelper.PromptWarning("A garage already got this name");
            }

            if (vehicles == null)
            {
                Console.WriteLine("Do you wish to add some vehicles to this garage ?");
                if (CheckYesNo())
                {
                    vehicles = CreateVehicles();
                }
                else
                {
                    vehicles = new List<Vehicle>();
                }
            }

            Garage garage = new Garage(name, vehicles);
            SetCurrentGarage(menuManager, garage);
            return garage;
        }

        public static void SetCurrentGarage(MenuManager menuManager)
        {
            Console.WriteLine("Do you wish to have a list of all the garages ?");
            if (CheckYesNo())
            {
                menuManager.GetGarageList().ForEach(garage =>
                {
                    PromptHelper.PromptSubSubTitle($"This is the {garage.Name} garage");
                    Console.WriteLine($"it constains {garage.vehicles.Count} vehicles");
                    Console.WriteLine($"for a total value of {garage.CalculateGarageValue()}");
                });
            }

            Console.WriteLine("Please Choose a garage by entering its name");
            Garage garage;
            while (true)
            {
                GetAString(out string line);
                if (menuManager.GetGarageList().Count <= 0) 
                {
                    ExceptionHandler.HandleException(new Exception("Well there are no garages created yet please load one or create a new one"), true); 
                    return; 
                }
                garage = menuManager.GetGarageList().Where(garage => garage.Name.ToLower() == line.ToLower().Trim())
                                                    .ToList()
                                                    .First();
                if (garage != null){
                    SetCurrentGarage(menuManager, garage);
                    Console.WriteLine($"Thanks for choosing the {garage.Name} garage");
                    return;
                }
            }
        }

        public static void SetCurrentGarage(MenuManager menuManager, Garage garage)       
        {
            menuManager.CurrentGarage = garage;
            menuManager.AddGarage(garage);
        }

        public static void SetCurrentGarage(MenuManager menuManager, string garage_name)
        {
            List<Garage> garages = menuManager.GetGarageList();
            if (garages.Count > 0)
            {
                menuManager.CurrentGarage = garages.Where(garage => garage.Name == garage_name).ToList().First();
            }
        }

        public static List<Vehicle> CreateVehicles()
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            bool isWishingForMore = true;

            while (isWishingForMore)
            {
                Console.WriteLine("Please enter <beer -car or -truck or -moto> depending on which type of vehicule you want to add to your garage");
                GetAParsable();
                Console.WriteLine("Do you wish to continue ?");
                if (!CheckYesNo())
                {
                    isWishingForMore = false;
                }
            }

            return vehicles;
        }

        public static void CreateACar()
        {
            VehicleData vehicleData = new VehicleData();
            Console.WriteLine("a car");
        }

        public static void CreateATruck()
        {
            VehicleData vehicleData = new VehicleData();
            Console.WriteLine("a truck");
        }

        public static void CreateAMoto()
        {
            VehicleData vehicleData = new VehicleData();
            Console.WriteLine("a moto");
        }

        private static VehicleData CreateVehicle()
        {
            VehicleData vehicleData = new VehicleData();
            GetAString(out vehicleData.Name, "What is the name of your vehicle ?");
            GetAString(out vehicleData.priceHT, "What is the name of your vehicle ?");
            return vehicleData;
        }
        #endregion

        #region helper data struct
        public struct VehicleData
        {
            public string Name;
            public decimal priceHT;
            public brand_enum brand;
            public Motor motor;
            public List<Option> options;
        }
        #endregion
    }
}
