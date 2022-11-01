using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Gestion_De_Garage
{
    public class MenuManager
    {
        bool isWorking = true;
        public bool isOverriden;
        public bool IsWorking { get { return isWorking; } set { isWorking = value; } }

        UserOptionsMenu mainMenu;
        public UserOptionsMenu MainMenu { get { return mainMenu; }}
        UserOptionsMenu garageMenu;
        public UserOptionsMenu GarageMenu { get { return garageMenu; }}
        UserOptionsMenu vehicleMenu;
        public UserOptionsMenu VehicleMenu { get { return vehicleMenu; }}
        List<UserOptionsMenu> userOptions = null;
        public List<UserOptionsMenu> GetUserOptions()
        {
            if (userOptions == null) return new List<UserOptionsMenu>();
            return userOptions;
        }

        UserOptionsMenu currentMenu;
        public UserOptionsMenu CurrentMenu { get { return currentMenu; } set { currentMenu = value; } }

        Garage currentGarage; 
        public Garage CurrentGarage { get { return currentGarage; } set { currentGarage = value; } }

        Vehicle currentVehicle;
        public Vehicle CurrentVehicle { get { return currentVehicle; } set { currentVehicle = value; } }

        List<Garage> garages = new List<Garage>();
        public void AddGarage(Garage garage)
        {
            if (garages == null) garages = new List<Garage>();
            if (!garages.Contains(garage))
            {
                garages.Add(garage);
            }
        }
        public void RemoveGarage(Garage garage)
        {
            if (garages == null) return;
            garages.Remove(garage);
        }
        public List<Garage> GetGarageList()
        {
            if (garages != null)
            {
                return garages;
            }
            else
            {
                PromptHelper.PromptWarning("There are no Garage registered");
                return new List<Garage>();
            }
        }

        public MenuManager()
        {
            currentMenu = mainMenu;
            mainMenu = new UserOptionsMenu(
                "MAIN MENU",
                "This is the main menu to save and load garages using JSON serialized files and to go to the different menus",
                new List<unitary_option>()
                {
                    new unitary_option(){
                        option_description = "Prompt 0 to quit the application",
                        action = () => {MenuInteractions.ReallyWantToQuit(this, currentMenu); }
                    },
                    new unitary_option()
                    {
                        option_description = "Prompt 1 to get some help",
                        action= () => {MenuInteractions.ShowHelp(); }
                    },
                    new unitary_option()
                    {
                        option_description = "Prompt 2 to save the garage",
                        action = () => {new SaveAndLoadHandler().SaveData(this); }
                    },
                    new unitary_option()
                    {
                        option_description = "Prompt 3 to load a saveFile",
                        action = () => {new SaveAndLoadHandler().LoadData(this);}
                    },
                    new unitary_option(){
                        option_description = "Prompt 4 to use the garage menu", 
                        action = () => {currentMenu = garageMenu; }
                    },
                    new unitary_option()
                    {
                        option_description = "Prompt 5 to go to the vehicule menu",
                        action = () => {currentMenu = vehicleMenu; }
                    }
                }
            );

            garageMenu = new UserOptionsMenu(
                "GARAGE MENU",
                "This is the menu to manage the garages and their stock",
                new List<unitary_option>()
                {
                    new unitary_option()
                    {
                        option_description = "Prompt 0 to go back to Main Menu",
                        action = () => {currentMenu = mainMenu; }
                    },
                    new unitary_option()
                    {
                        option_description = "Prompt 1 to create a new garage --> NB: this garage is going to become the active one",
                        action = () => {MenuInteractions.GenerateTheGarage(this); }
                    },
                    new unitary_option()
                    {
                        option_description = "Prompt 2 to select a Garage using a garage name",
                        action = () => {MenuInteractions.GetAGarage(this); }
                    },
                    new unitary_option()
                    {
                        option_description = $"Prompt 3 to add one or multiple vehicule to {currentGarage?.Name} garage",
                        action = () => {
                            if (currentGarage == null){ 
                                ExceptionHandler.HandleException(new Exception("You need to have a garage, please load or create a new one"), true); 
                                return;
                            }
                            MenuInteractions.CreateVehicles();
                        }
                    },
                    new unitary_option()
                    {
                        option_description = "Prompt 4 to show all the availible brand present in the garage",
                        action = () => {MenuInteractions.ShowAllBrands(); }
                    },
                    new unitary_option()
                    {
                        option_description = "Prompt 5 to show all the availible type motors present in the garage",
                        action = () => {MenuInteractions.ShowAllMotorsInGarage(this); }
                    },
                    new unitary_option()
                    {
                        option_description = "Prompt 6 to go to the vehicule menu",
                        action = () => { currentMenu = vehicleMenu; }
                    }
                }
            );

            vehicleMenu = new UserOptionsMenu(
                "VEHICLE MENU",
                "This is the menu to manage individual vehicles, adding/removing options ect ....",
                new List<unitary_option>()
                {
                    new unitary_option()
                    {
                        option_description = "Prompt 0 to go back to Main Menu",
                        action = () => {currentMenu = mainMenu; }
                    },
                    new unitary_option()
                    {
                        option_description = "Prompt 1 to go back to Garage Menu",
                        action = () => {currentMenu = garageMenu; }
                    },
                    new unitary_option()
                    {
                        option_description = "Prompt 2 to select a vehicle",
                        action = () => {
                            currentVehicle = MenuInteractions.SelectAVehicle(this);
                        }
                    },
                    new unitary_option()
                    {
                        option_description = "Prompt 3 to add a vehicle [selects it for further use]",
                        action = () => {
                            MenuInteractions.CreateAVehicle(this);
                            if (!MenuInteractions.GarageCheck(this)) return;
                            currentVehicle = currentGarage.GetVehicles().Last();
                        }
                    },
                    new unitary_option()
                    {
                        option_description = "Prompt 4 to delete a vehicle [the selected one by default]",
                        action = () => {
                            MenuInteractions.DestroySelectedVehicle(this);
                        }
                    },
                    new unitary_option()
                    {
                        option_description = "Prompt 5 to show the options of a vehicle [selected one by default]",
                        action = () => {
                            if (!MenuInteractions.VehicleCheck(this)) return;
                            currentVehicle.ShowOptions();
                        }
                    },
                    new unitary_option()
                    {
                        option_description = "Prompt 5 to show the data of a vehicle [selected one by default]",
                        action = () => {
                            if (!MenuInteractions.VehicleCheck(this)) return;
                            currentVehicle.Show();
                        }
                    },
                    new unitary_option()
                    {
                        option_description = "Prompt 6 to add some options to a vehicle [selected one by default]",
                        action = () => {
                            if (!MenuInteractions.VehicleCheck(this)) return;
                            currentVehicle.AddOptions(MenuInteractions.SpecifyOptions());
                        }
                    },
                    new unitary_option()
                    {
                        option_description = "Prompt 7 to remove some options to a vehicle [selected one by default]",
                        action = () => {
                            MenuInteractions.RemoveOptionOnSelectedVehicle(this);
                        }
                    },
                });

            userOptions = new List<UserOptionsMenu>()
            {
                mainMenu,
                garageMenu,
                vehicleMenu,
            };
        }

        #region Menu  Loop
        public async void StartManagingTheGarages()
        {
            currentMenu = mainMenu;
            while (isWorking)
            {
                ShowMenu();
            }
        }

        private void ShowMenu()
        {
            ShowOptions();
        }

        private void ShowOptions()
        {
            PromptHelper.PromptSubTitle($"Welcome to {currentMenu.MenuName}");
            Console.WriteLine($"What do you wish to do? Here are {currentMenu.unitary_Options.Count() - 1} options you can choose from");
            Console.WriteLine();
            currentMenu.unitary_Options.ForEach(uo => Console.WriteLine("- " + uo.option_description));
            Console.WriteLine();

            int index = -1;

            while (index > currentMenu.unitary_Options.Count() || index < 0)
            {
                Console.WriteLine("Please choose wisely");
                try
                {
                    string prompt = Console.ReadLine();
                    if (Parser.Instance.isParse(prompt))
                    {
                        return;
                    }
                    else
                    {
                        index = Convert.ToInt32(prompt);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            currentMenu.unitary_Options[index].action();
        }

        #endregion
    }
    #region MenuDataStructure
    public class UserOptionsMenu
    {
        public UserOptionsMenu(string MenuName, string Menudescription, List<unitary_option> unitary_Options, bool bonusLine = true)
        {
            this.MenuName = MenuName;
            this.MenuDescription = Menudescription;
            this.unitary_Options = unitary_Options;

            if (bonusLine)
            {
                unitary_Options.Add(
                    new unitary_option()
                    {
                        option_description = "remember you can always use command lines to navigate using the <beer> keyword",
                        action = () => { }
                    }
                );
            }
        }

        public string MenuName { get; private set; }
        public string MenuDescription { get; private set; }
        public List<unitary_option> unitary_Options = new List<unitary_option>();
    }

    public struct unitary_option
    {
        public string option_description;
        public Action action;
    }
    #endregion
}
