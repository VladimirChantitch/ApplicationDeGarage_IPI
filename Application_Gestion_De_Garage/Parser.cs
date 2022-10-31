using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Application_Gestion_De_Garage
{
    public class Parser
    {
        public static Parser Instance {get; private set;}
        List<Option_1_Arg> one_arg_options;
        public List<Option_1_Arg> Get_1Arg_options()
        {
            if (one_arg_options != null)
            {
                return one_arg_options;
            }
            return new List<Option_1_Arg>();
        }

        List<Option_2_Arg> two_arg_options;
        public List<Option_2_Arg> Get_2Args_options()
        {
            if (two_arg_options != null)
            {
                return two_arg_options;
            }
            return new List<Option_2_Arg>();
        }

        MenuManager menuManager;
        public MenuManager MenuManager { get { return menuManager; } }

        public Parser(MenuManager menuManager)
        {
            if (Instance == null)
            {
                Instance = this;
            }

            this.menuManager = menuManager;

            one_arg_options = new List<Option_1_Arg>();
            one_arg_options.AddRange(
                new List<Option_1_Arg>(){
                    new Option_1_Arg("-h", "Use this command to ask for help menu", () => {MenuInteractions.ShowHelp(); }),
                    new Option_1_Arg("-q", "Use this command to quit the app",() => {MenuInteractions.ReallyWantToQuit(menuManager, menuManager.CurrentMenu); }),
                    new Option_1_Arg("-w", "Use this option to save the data of the garage",() => {}),
                    new Option_1_Arg("-car", "If you already own a garage, you'll be able to add a car to this garage", ()=>{MenuInteractions.CreateACar(); }),
                    new Option_1_Arg("-truck", "If you already own a garage, you'll be able to add a truck to this garage", ()=>{MenuInteractions.CreateATruck(); }),
                    new Option_1_Arg("-moto", "If you already own a garage, you'll be able to add a moto to this garage", ()=>{MenuInteractions.CreateAMoto(); }),
                    new Option_1_Arg("-main", "Use this to get to <MAIN MENU>",() => {menuManager.CurrentMenu = menuManager.MainMenu; }),
                    new Option_1_Arg("-garage", "Use this to get to <GARAGE MENU>",() => {menuManager.CurrentMenu = menuManager.GarageMenu; }),
                    new Option_1_Arg("-vehicle", "Use this to get to <VEHICLE MENU>",() => {menuManager.CurrentMenu = menuManager.GarageMenu; }),
                }
            );

            two_arg_options = new List<Option_2_Arg>();
            two_arg_options.AddRange(
                new List<Option_2_Arg>()
                { 

                }
            );
        }

        public bool isParse(string line)
        {
            if (line.ToLower().Contains("beer"))
            {
                ParseCommand(line);
                return true;
            }
            return false;
        }

        public void ParseCommand(string line)
        {
            string[] commands = line.Split(" ");
            switch (commands.Length)
            {
                case 1: Console.WriteLine("use the -h option to get some help"); break;
                case 2: Handle_1_ArgOption(commands[1]); break;
                case 3: Handle_2_ArgsOptions(commands); break;
                case 4: Console.WriteLine("Well this complexity of prompt is not supported yet <SORRY>"); break ;
            }
        }

        private void Handle_1_ArgOption(string command)
        {
            try
            {
                one_arg_options.Where(op => op.option == command.ToLower()).First().action.Invoke();
            }
            catch
            {
                ExceptionHandler.HandleException(new Exception("Well please use the <beer -h> to check the right use of the commands"));
            }
        }

        private void Handle_2_ArgsOptions(string[] commands)
        {
            
        }

        public class Unit_Option
        {
            public Unit_Option(string option, string optionDescription)
            {
                this.option = option;
                this.optionDescription = optionDescription;
            }

            public string option;
            public string optionDescription;
        }

        public class Option_1_Arg : Unit_Option
        {
            public Option_1_Arg(string option, string optionDescription, Action action) : base(option, optionDescription)
            {
                this.action = action;
            }

            public Action action;
        }

        public class Option_2_Arg : Unit_Option
        {
            public Option_2_Arg(string option, string optionDescription) : base(option, optionDescription)
            {
                
            }

            List <Option_1_Arg> _args; 
        }
    }
}
