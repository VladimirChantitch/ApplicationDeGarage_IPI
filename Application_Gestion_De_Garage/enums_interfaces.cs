using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Gestion_De_Garage
{
    #region enums
    public enum brand_enum
    {
        Peugeot,
        Renault,
        Citroen,
        Audi,
        Ferrari
    }

    public enum motor_type
    {
        Diesel,
        Gazoline,
        Hybride,
        Electric
    }

    public enum Filter
    {
        id,
        name,
        priceHT,
        price
    }
    #endregion
}
