using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{
    internal class RealBathroom : RealRoom
    {
        private const double defaultHumidity = 40.0;
        private const double defaultHumidityTimeConstant = 40.0;
        private const double humidityMax = 70;
        private const double humidityMin = 30;
        private const double humidityInsesibility = 4.0;
        private const double humidityChangeStep = 1.0;

        public double Humidity { get; private set; }
        public double DesiredHumidity { get; private set; }
        //Humidity time constant in seconds (typical value is a few hours)
        public double HumidityTimeConstant { get; }


        public RealBathroom(string name,
                        double temperature = defaultTemperature,
                        double desiredTemperature = defaultTemperature,
                        double timeConstant = defaultThermalTimeConstant,
                        double humidity = defaultHumidity,
                        double desiredHumidity = defaultHumidity,
                        double humidityTimeConstant = defaultHumidityTimeConstant)
            :base(name, temperature, desiredTemperature, timeConstant)
        {
            if (humidity < humidityMin)
                humidity = humidityMin;
            else if (humidity > 100.0)
                humidity = humidityMax;
            if (desiredHumidity < humidityMin || desiredHumidity > humidityMax)
                desiredHumidity = defaultHumidity;
            if (humidityTimeConstant <= 0.0)
                humidityTimeConstant = defaultHumidityTimeConstant;
            this.Humidity = humidity;
            this.DesiredHumidity = desiredHumidity;
            this.HumidityTimeConstant = humidityTimeConstant;
        }

        public void updateHumidity()
        {
            if (Humidity <= (DesiredHumidity + humidityInsesibility))
            {
                updateHumidityWithoutDehumidifier();
                return;
            }
            var timeNow = TimeOnly.FromDateTime(DateTime.Now);
            var timeDifference = timeNow - LastAdjusted;
            double differenceInSeconds = helper.calculateDifferenceInSeconds(timeDifference);
            double ratio = helper.calculateRatio(differenceInSeconds, this.HumidityTimeConstant);
            double disturbance = helper.getRandomDisturbance();
            Humidity += (ratio * (DesiredHumidity - Humidity) + disturbance);
        }

        public void updateDesiredHumidity(double humidity)
        {
            LastAdjusted = TimeOnly.FromDateTime(DateTime.Now);
            if (humidity < humidityMin)
                humidity = humidityMin;
            else if (humidity > humidityMax)
                humidity = humidityMax;
            this.Humidity = humidity;
        }

        private void updateHumidityWithoutDehumidifier()
        {
            Humidity += humidityChangeStep;
            return;
        }
    }
}
