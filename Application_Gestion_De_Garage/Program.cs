using Application_Gestion_De_Garage;
using System;
using System.Numerics;

namespace AppGarage
{
    public static class AppGarageMain
    {

        public static void Main()
        {
            MenuManager menuManager = new MenuManager();
            new Parser(menuManager);

            menuManager.StartManagingTheGarages();

            //Garage garage = CreateAGarage();

            //garage.AddAVehicle(new Car(2, 5, 123, 12, "aspergus", 175, brand_enum.Renault));
            //garage.AddAVehicle(new Car(2, 5, 123, 12, "lol", 13000, brand_enum.Renault));
            //garage.AddAVehicle(new Car(2, 5, 123, 12, "aspergus", 1, brand_enum.Renault));
            //garage.AddAVehicle(new Moto(2, "hehe", 11300, brand_enum.Renault));

            ////garage.Show(true);


            //garage.Show(garage.GetVehicleByName("aspergus"));
        }

        public static Garage CreateAGarage() 
        {
            return new Garage("JasonDragonTrucksAndGears", null);
        }
    }
}
