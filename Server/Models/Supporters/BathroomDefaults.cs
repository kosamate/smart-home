using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models.Supporters
{
    public readonly struct BathroomDefaults
    {
        public const double defaultDesiredHumidity = 40.0;
        public const double defaultHumidityTimeConstant = 40.0;
        public const double humidityMax = 70.0;
        public const double humidityMin = 30.0;
        public const double humidityInsensitivity = 4.0;
        public const double humidityChangeStep = 1.0;
    }
}
