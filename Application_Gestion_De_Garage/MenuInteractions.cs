using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

            PromptHelper.PromptSubTitle("Those are the more complexe options, just use <beer> followed byt the option to do stuff");
            Parser.Instance.Get_2Args_options().ForEach(one_a_ops =>
            {
                Console.WriteLine();
                Console.WriteLine(one_a_ops.option + "              " + one_a_ops.optionDescription);
                one_a_ops._args.ForEach(ops =>
                {
                    Console.WriteLine("*******  " + ops.option + "        " + ops.optionDescription);
                });
            });

            PromptHelper.PromptWarning("Beware when you are building a garage or building a vehicle those commands are disbled and you have to finsih the process you are currently in");
            AwaitForUser();
        }

        public static void AwaitForUser()
        {
            Console.WriteLine();
            Console.WriteLine("Press ENTER to continue");
            Console.ReadLine();
        }

        public static bool CheckYesNo(string message = "")
        {
            if (message != "") Console.WriteLine(message);
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

        public static void GetAStringOfType<T>(out T result, string instruction = "")
        {
            bool isValid = false;
            result = default(T);
            string prompt = "";
            while (!isValid)
            {
                if (instruction != "") PromptHelper.PromptSubSubTitle(instruction);
                try
                {
                    prompt = Console.ReadLine();

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
                                    result = (T)Convert.ChangeType(prompt, typeof(T));
                                    isValid = true;
                                    break;
                                case true when typeof(T) == typeof(int):
                                    result = (T)Convert.ChangeType(prompt, typeof(T));
                                    isValid = true;
                                    break;
                                case true when typeof(T) == typeof(brand_enum):
                                    if(Enum.TryParse<brand_enum>(prompt, out brand_enum brand)){
                                        result = (T)Convert.ChangeType(brand, typeof(T));
                                        isValid = true;
                                    }
                                    else
                                    {
                                        throw (new Exception("Please enter the name of one of the brands availible"));
                                    }                                  
                                    break;
                                case true when typeof(T) == typeof(motor_type):
                                    if (Enum.TryParse<motor_type>(prompt, out motor_type botor_type))
                                    {
                                        result = (T)Convert.ChangeType(botor_type, typeof(T));
                                        isValid = true;
                                    }
                                    else
                                    {
                                        throw (new Exception("Please enter the name of one of the motor types availible"));
                                    }
                                    break;
                                default:
                                    throw (new NotImplementedException($"The type{typeof(T)} is not supported yet"));
                            }
                        }
                        catch (Exception ex)
                        {
                            ExceptionHandler.HandleException(ex);
                        }
                    }
                    else
                    {
                        throw (new Exception("Well you have to write something"));
                    }
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
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

        public static bool GarageCheck(MenuManager menuManager)
        {
            if (menuManager.CurrentGarage == null)
            {
                ExceptionHandler.HandleException(new Exception("Please select or create a Garage first")); 
                return false;
            }
            return true;
        }

        public static bool VehicleCheck(MenuManager menuManager)
        {
            if (menuManager.CurrentVehicle == null)
            {
                ExceptionHandler.HandleException(new Exception("Please select a vehicle first"));
                return false;
            }
            return true;
        }
        #endregion
        #region Fonctionalities
        #region Garage fonctionnalities
        public static void GetAGarage(MenuManager menuManager)
        {
            if (CheckYesNo("Do you wish to have a list of all the garages ?"))
            {
                menuManager.GetGarageList().ForEach(garage =>
                {
                    PromptHelper.PromptSubSubTitle($"This is the {garage.Name} garage");
                    Console.WriteLine($"it constains {garage.GetVehicles().Count} vehicles");
                    Console.WriteLine($"for a total value of {garage.CalculateGarageValue()}");
                });
            }

            Console.WriteLine("Please Choose a garage by entering its name");
            Garage garage;
            while (true)
            {
                GetAStringOfType(out string line);
                if (menuManager.GetGarageList().Count <= 0)
                {
                    ExceptionHandler.HandleException(new Exception("Well there are no garages created yet please load one or create a new one"), true);
                    return;
                }
                garage = menuManager.GetGarageList().Where(garage => garage.Name.ToLower() == line.ToLower().Trim())
                                                    .ToList()
                                                    .First();
                if (garage != null)
                {
                    SetCurrentGarage(menuManager, garage);
                    Console.WriteLine($"Thanks for choosing the {garage.Name} garage");
                    return;
                }
            }
        }
        public static void GenerateTheGarage(MenuManager menuManager, string name = null, List<Vehicle> vehicles = null)
        {
            if (name == null)
            {
                GetAStringOfType(out name, "What is the name of your garage");
                if (menuManager.GetGarageList().Where(garage => garage.Name == name).ToList().Count > 0) PromptHelper.PromptWarning("A garage already got this name");
            }

            Garage garage = new Garage(name);
            SetCurrentGarage(menuManager, garage);

            if (CheckYesNo("Do you wish to add some vehicles to this garage ?"))
            {
                CreateVehicles();
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
        #endregion
        #region vehicles creation
        public static void CreateVehicles()
        {
            bool isWishingForMore = true;

            while (isWishingForMore)
            {
                Console.WriteLine("Please enter <beer -add>  <-car> or <-truck> or <-moto> depending on which type of vehicule you want to add to your garage");
                GetAParsable();
                if (!CheckYesNo("Do you wish to add more vehicles?"))
                {
                    isWishingForMore = false;
                }
            }
        }

        public static void CreateAVehicle(MenuManager menuManager)
        {
            if (!GarageCheck(menuManager)) return;

            Console.WriteLine("Please enter <beer -add>  <-car> or <-truck> or <-moto> depending on which type of vehicule you want to add to your garage");
            GetAParsable();
        }

        public static Car CreateACar(MenuManager menuManager)
        {
            if (!GarageCheck(menuManager)) return null;
            CarData carData = new CarData();
            carData.vehicleData = CreateVehicle();
            GetAStringOfType(out carData.taxHorsePower, "What is the amount of tax horse power of the car ?");
            GetAStringOfType(out carData.doorNumber, "What is the amount of doors of the car ?");
            GetAStringOfType(out carData.sitsNumber, "What is the number of setis of the car ?");
            GetAStringOfType(out carData.carTrunkSize, "What is the size of the car trunk ?");
           
            PromptHelper.PromptCongratulation("Nice you've done it, you've created a nice car");
            return new Car(carData);
        }

        public static Truck CreateATruck(MenuManager menuManager)
        {
            if (!GarageCheck(menuManager)) return null;
            TruckData truckData = new TruckData();
            truckData.vehicleData = CreateVehicle();
            GetAStringOfType(out truckData.axle, "What is number of axle of this truck ?");
            GetAStringOfType(out truckData.weight, "What is the weight of the truck?");
            GetAStringOfType(out truckData.volume, "What is the volume of the truck?");

            PromptHelper.PromptCongratulation("Nice you've done it, you've created a nice truck");
            return new Truck(truckData);
        }

        public static Moto CreateAMoto(MenuManager menuManager)
        {
            if (!GarageCheck(menuManager)) return null;
            MotoData motoData = new MotoData();
            motoData.vehicleData = CreateVehicle();
            GetAStringOfType(out motoData.cylinders, "What is the number of cylinder of the moto?");

            PromptHelper.PromptCongratulation("Nice you've done it, you've created a nice moto");
            return new Moto(motoData);
        }

        private static VehicleData CreateVehicle()
        {
            VehicleData vehicleData = new VehicleData();

            GetAStringOfType(out vehicleData.Name, "What is the name of your vehicle ?");
            GetAStringOfType(out vehicleData.priceHT, "What is the price HT (euro) of your vehicle ?");
            if(CheckYesNo("Do you wish to see all garage brands ?"))
            {
                ShowAllBrands();
            }
            GetAStringOfType(out vehicleData.brand, "What is the brand of your vehicle ?");

            Console.WriteLine();
            Console.WriteLine("Well you are not selling rubish, your vehicules needs a motor");
            vehicleData.motor = SpecifyMotor();

            vehicleData.options = new List<OptionData>();
            if (CheckYesNo("Do you wish to add some options to this vehicle maybe ?"))
            {
                vehicleData.options.AddRange(SpecifyOptions());
            }

            return vehicleData;
        }

        public static MotorData SpecifyMotor()
        {
            MotorData motor = new MotorData();

            GetAStringOfType(out motor.Name, "what is the name of the motor ?");
            Console.WriteLine(motor.Name);
            GetAStringOfType(out motor.power, "what is the horse power of your motor");
            GetAStringOfType(out motor.price, "what is the price of this motor");
            if (CheckYesNo("Do you wish to see all the motor types"))
            {
                Enum.GetNames(typeof(motor_type)).ToList().ForEach(name => Console.WriteLine("- " + name));
            }
            GetAStringOfType(out motor.motortype, "what is the motor type");
            
            return motor;
        }

        public static List<OptionData> SpecifyOptions()
        {
            List<OptionData> options = new List<OptionData>();
            do
            {
                OptionData option = new OptionData();
                GetAStringOfType(out option.Name, "What is your option name ?");
                GetAStringOfType(out option.price, "What is your option price ?");
                options.Add(option);
            }
            while (CheckYesNo("Do you wish to add one more option ?"));
            return options;
        }
        #endregion
        #region ObjectDestruction
        public static void DestroySelectedVehicle(MenuManager menuManager)
        {
            if (!VehicleCheck(menuManager)) return;
            if (CheckYesNo($"Are you sure you want to delete {menuManager.CurrentVehicle.Name}"))
            {
                menuManager.CurrentGarage.RemoveAVehicle(menuManager.CurrentVehicle);
            }

            PromptHelper.PromptCongratulation($"Congratulation you have deleted {menuManager.CurrentVehicle.Name}");
        }
        public static void RemoveOptionOnSelectedVehicle(MenuManager menuManager)
        {
            if (!VehicleCheck(menuManager)) return;
            if (CheckYesNo("Do you wish to see all the vehicule options ?"))
            {
                menuManager.CurrentVehicle.ShowOptions();
            }

            GetAStringOfType(out int id, "PLease enter the id of the option you wish to remove");
            menuManager.CurrentVehicle.RemoveAnOption(id);

        }
        #endregion
        #region Object Selection
        public static Vehicle SelectAVehicle(MenuManager menuManager)
        {
            if (!GarageCheck(menuManager)) return null;
            if (CheckYesNo("Do you wish to have a list of all vehicles by ID ?"))
            {
                menuManager.CurrentGarage.Show(true);
                AwaitForUser();
            }
            GetAStringOfType(out int id, "What vehicle id do you wish to select ?");
            return menuManager.CurrentGarage.GetVehicleByID(id);
        }
        public static Vehicle SelectACar(MenuManager menuManager)
        {
            if (!GarageCheck(menuManager)) return null;
            if (CheckYesNo("Do you wish to have a list of all Car by ID ?"))
            {
                menuManager.CurrentGarage.ShowCars(true);
                AwaitForUser();
            }
            GetAStringOfType(out int id, "What vehicle id do you wish to select ?");
            return menuManager.CurrentGarage.GetVehicleByID(id);
        }
        public static Vehicle SelectATruck(MenuManager menuManager)
        {
            if (!GarageCheck(menuManager)) return null;
            if (CheckYesNo("Do you wish to have a list of all Truck by ID ?"))
            {
                menuManager.CurrentGarage.ShowTrucks(true);
                AwaitForUser();
            }
            GetAStringOfType(out int id, "What vehicle id do you wish to select ?");
            return menuManager.CurrentGarage.GetVehicleByID(id);
        }
        public static Vehicle SelectAMoto(MenuManager menuManager)
        {
            if (!GarageCheck(menuManager)) return null;
            if (CheckYesNo("Do you wish to have a list of all Moto by ID ?"))
            {
                menuManager.CurrentGarage.ShowMoto(true);
                AwaitForUser();
            }
            GetAStringOfType(out int id, "What vehicle id do you wish to select ?");
            return menuManager.CurrentGarage.GetVehicleByID(id);
        }
        #endregion
        #region Show data
        public static void ShowAllMotorsInGarage(MenuManager menuManager)
        {
            if (!GarageCheck(menuManager)) return;
            menuManager.CurrentGarage.GetVehicles().ForEach(vehicle =>
            {
                PromptHelper.PromptSubSubTitle($"The motor {vehicle.VehicleMotor.Name} has");
                Console.WriteLine($" a power of {vehicle.VehicleMotor.Power} HP");
                Console.WriteLine($" a price of {vehicle.VehicleMotor.Price} eurodollar");
                Console.WriteLine($" and is of the {vehicle.VehicleMotor.Motor_Type} type");
            });
            AwaitForUser();
        }

        public static void ShowAllBrands()
        {
            Enum.GetNames(typeof(brand_enum)).ToList().ForEach(name => Console.WriteLine("- " + name));
        }
        #endregion
        #endregion
    }
}
