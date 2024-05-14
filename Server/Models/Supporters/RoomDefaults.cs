using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models.Supporters
{
    public readonly struct RoomDefaults
    {
        public const double defaultDesiredTemperature = 21.0;
        public const double defaultThermalTimeConstant = 20.0;
        public const double temperatureMax = 28.0;
        public const double temperatureMin = 15.0;
        public const double temperatureInsensitivity = 0.5;
        public const double temperatureChangeStep = 0.1;
        public const bool defaultLightState = false;
    }
}
