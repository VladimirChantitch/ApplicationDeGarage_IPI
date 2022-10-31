using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Gestion_De_Garage
{
    public static class Incrementator
    {
        private static int vehicleIncrement = 0;
        public static int VehicleIncrement
        {
            get { return ++vehicleIncrement; }
        }

        private static int optionIncrement = 0;
        public static int OptionIncrement
        {
            get { return ++optionIncrement; }
        }

        private static int motorIncrement = 0;
        public static int MotorIncrement
        {
            get { return ++motorIncrement; }
        }
    }
}
