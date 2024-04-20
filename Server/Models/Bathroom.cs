using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{
    internal class Bathroom : Room
    {
        const double defaultHumidity = 40.0;
        const double defaultHumidityTimeConstant = 40.0;

        public double Humidity { get; }
        public double DesiredHumidity { get; set; }
        //Humidity time constant in seconds (typical value is a few hours)
        public double HumidityTimeConstant { get; }


        public Bathroom(string name,
                        double temperature = defaultTemperature,
                        double desiredTemperature = defaultTemperature,
                        double timeConstant = defaultThermalTimeConstant,
                        double humidity = defaultHumidity,
                        double desiredHumidity = defaultHumidity,
                        double humidityTimeConstant = defaultHumidityTimeConstant)
            :base(name, temperature, desiredTemperature, timeConstant)
        {
            if (humidity > 70.0 || humidity < 30.0)
                humidity = defaultHumidity;
            if (desiredHumidity > 70.0 || desiredHumidity < 30.0)
                desiredHumidity = defaultHumidity;
            if (humidityTimeConstant <= 0.0)
                humidityTimeConstant = defaultHumidityTimeConstant;
            this.Humidity = humidity;
            this.DesiredHumidity = desiredHumidity;
            this.HumidityTimeConstant = humidityTimeConstant;
        }


    }
}
