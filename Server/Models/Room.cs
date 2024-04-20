using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{
    internal class Room
    {
        protected const double defaultTemperature = 21.0;
        protected const double defaultThermalTimeConstant = 20.0;
        private const double temperatureMax = 28.0;
        private const double temperatureMin = 15.0;
        private const double temperatureInsensibility = 0.5;
        private const double temperatureChangeStep = 0.1;
        protected static Helper helper = new Helper();

        public string Name { get; }
        public double Temperature { get; private set; }
        public double DesiredTemperature { get; private set; }
        public bool Light {  get; private set; }
        //Thermal time constant in seconds (typical value is between 15 and 30 minutes)
        public double ThermalTimeConstant { get; }
        public TimeOnly LastAdjusted { get; protected set; }

        public bool isReachedTheDesiredTemperature { get; private set; }

        public Room(string name,
                    double temperature = defaultTemperature,
                    double desiredTemperature = defaultTemperature,
                    double timeConstant = defaultThermalTimeConstant)
        {
            this.Name = name;
            if (temperature < temperatureMin)
                temperature = temperatureMin;
            else if (temperature > temperatureMax)
                temperature = temperatureMax;
            if (desiredTemperature < temperatureMin || desiredTemperature > temperatureMax)
                desiredTemperature = defaultTemperature;
            if (timeConstant <= 0.0)
                timeConstant = defaultThermalTimeConstant;
            this.Temperature = temperature;
            this.DesiredTemperature = desiredTemperature;
            this.ThermalTimeConstant = timeConstant;
            this.LastAdjusted = TimeOnly.FromDateTime(DateTime.Now);
            if (temperature <= (desiredTemperature + 0.01) && temperature >= (desiredTemperature - 0.01))
                isReachedTheDesiredTemperature = true;
            else
                isReachedTheDesiredTemperature = false;
        }

        public void updateTemperature()
        {
            if (Temperature <= (DesiredTemperature + temperatureInsensibility)
                && Temperature >= (DesiredTemperature - temperatureInsensibility)
                && isReachedTheDesiredTemperature)
            {
                updateTemperatureBetweenInsensibility();
                return;
            }
            var timeNow = TimeOnly.FromDateTime(DateTime.Now);
            var timeDifference = timeNow - LastAdjusted;
            double differenceInSeconds = helper.calculateDifferenceInSeconds(timeDifference);
            double ratio = helper.calculateRatio(differenceInSeconds, this.ThermalTimeConstant);
            double disturbance = helper.getRandomDisturbance();
            Temperature += (ratio * (DesiredTemperature - Temperature) + disturbance);
            if (Temperature <= (DesiredTemperature + 0.01) && Temperature >= (DesiredTemperature - 0.01))
                isReachedTheDesiredTemperature = true;
        }

        public void updateDesiredTemperature(double temperature)
        {
            LastAdjusted = TimeOnly.FromDateTime(DateTime.Now);
            if (temperature < temperatureMin)
                temperature = temperatureMin;
            else if (temperature > temperatureMax)
                temperature = temperatureMax;
            DesiredTemperature = temperature;
        }

        public void changeLight()
        {
            Light = !Light;
        }
        //The outside temperature can be more or less than the inside, so the inside temperature can change up or down.
        //Now I decide this with the help of a random number generator.
        private void updateTemperatureBetweenInsensibility()
        {
            if (Temperature <= (DesiredTemperature + 0.01) && Temperature >= (DesiredTemperature - 0.01))
            {
                double sign = helper.getRandomSign();
                Temperature += (sign * temperatureChangeStep);
            }
            else
            {
                if (Temperature > DesiredTemperature)
                    Temperature += temperatureChangeStep;
                else
                    Temperature -= temperatureChangeStep;
            }

        }
    }
}
