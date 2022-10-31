using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Gestion_De_Garage
{
    public static class ExceptionHandler
    {
        public static void HandleException(Exception exception, bool erase = false)
        {
            switch (exception)
            {
                case NotImplementedException notImplemented:
                    //Create a ticket on the trello project
                    Console.WriteLine("Something wrong happened ::");
                    Console.WriteLine(notImplemented.Message.ToString());
                    break;
                case Exception ex:
                    Console.WriteLine("Something wrong happened ::");
                    Console.WriteLine(ex.Message.ToString());
                    break;
            }

            Console.WriteLine("Feel Free to use the <beer -h> command if you need help");
            Console.WriteLine("....");
            MenuInteractions.AwaitForUser();
            if (erase) Console.Clear();
            else Console.WriteLine();
        }
    }
}
