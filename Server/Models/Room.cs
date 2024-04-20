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

        public string Name { get; }
        public double Temperature { get; set; }
        public double DesiredTemperature { get; set; }
        //Thermal time constant in seconds (typical value is between 15 and 30 minutes)
        public double ThermalTimeConstant { get; }
        public TimeOnly LastAdjusted { get; set; }
        

        public Room(string name,
                    double temperature = defaultTemperature,
                    double desiredTemperature = defaultTemperature,
                    double timeConstant = defaultThermalTimeConstant)
        {
            this.Name = name;
            if (temperature < -20.0 || temperature > 45.0)
                temperature = defaultTemperature;
            if (desiredTemperature < 15.0 || desiredTemperature > 28.0)
                desiredTemperature = defaultTemperature;
            if (timeConstant <= 0.0)
                timeConstant = defaultThermalTimeConstant;
            this.Temperature = temperature;
            this.DesiredTemperature = desiredTemperature;
            this.ThermalTimeConstant = timeConstant;
            this.LastAdjusted = TimeOnly.FromDateTime(DateTime.Now);
        }

        public void updateTemperature()
        {
            if (Temperature == DesiredTemperature)
                return;
            var timeNow = TimeOnly.FromDateTime(DateTime.Now);
            var timeDifference = timeNow - LastAdjusted;
            double differenceInMinutes = calculateDifferenceInMinutes(timeDifference);
            double ratio = differenceInMinutes / (5 * ThermalTimeConstant);
            if (ratio > 1)
                ratio = 1;
            Temperature = Temperature + ratio * (DesiredTemperature - Temperature);
        }

        public void updateDesiredTemperature(double temperature)
        {
            LastAdjusted = TimeOnly.FromDateTime(DateTime.Now);
            if (temperature <= 15.0 || temperature > 28.0)
                temperature = defaultTemperature;
            DesiredTemperature = temperature;
        }

        private float calculateDifferenceInMinutes(TimeSpan difference)
        {
            float hoursDiff = (float)difference.TotalHours;
            float minutesDiff = (float)difference.TotalMinutes;
            float secondsDiff = (float)difference.TotalSeconds;
            float totalDiffMinutes = hoursDiff * 60 + minutesDiff + secondsDiff / 60;
            return totalDiffMinutes;
        }



    }
}
