﻿using Server.Models.Supporter;
using Common.Defaults;

namespace Server.Models
{
    internal class RealBathroom : RealRoom
    {
        public double Humidity { get; private set; }
        public double DesiredHumidity { get; private set; }
        //Humidity time constant in seconds (typical value is a few hours)
        public double HumidityTimeConstant { get; }


        public RealBathroom(string name, double temperature, bool light, double humidity,
                        double desiredTemperature = RoomDefaults.defaultDesiredTemperature,
                        double timeConstant = RoomDefaults.defaultThermalTimeConstant,
                        double desiredHumidity = BathroomDefaults.defaultDesiredHumidity,
                        double humidityTimeConstant = BathroomDefaults.defaultHumidityTimeConstant)
            :base(name, temperature, light, desiredTemperature, timeConstant)
        {
            if (humidity < BathroomDefaults.humidityMin)
                this.Humidity = BathroomDefaults.humidityMin;
            else if (humidity > 100.0)
                this.Humidity = BathroomDefaults.humidityMax;
            else
                this.Humidity = humidity;

            if (desiredHumidity < BathroomDefaults.humidityMin || desiredHumidity > BathroomDefaults.humidityMax)
                this.DesiredHumidity = BathroomDefaults.defaultDesiredHumidity;
            else
                this.DesiredHumidity = desiredHumidity;

            if (humidityTimeConstant <= 0.0)
                this.HumidityTimeConstant = BathroomDefaults.defaultHumidityTimeConstant;
            else
                this.HumidityTimeConstant = humidityTimeConstant;
        }

        public override void UpdateMeasuredValues()
        {
            base.UpdateMeasuredValues();

            if (Humidity <= (DesiredHumidity + BathroomDefaults.humidityInsensitivity))
            {
                UpdateHumidityWithoutDehumidifier();
                return;
            }

            double difference = Helper.CalculateDifference(Humidity, DesiredHumidity, LastAdjusted, HumidityTimeConstant);
            Humidity += difference;
        }

        public void UpdateDesiredHumidity(double humidity)
        {
            LastAdjusted = TimeOnly.FromDateTime(DateTime.Now);
            if (humidity < BathroomDefaults.humidityMin)
                DesiredHumidity = BathroomDefaults.humidityMin;
            else if (humidity > BathroomDefaults.humidityMax)
                DesiredHumidity = BathroomDefaults.humidityMax;
            else
                DesiredHumidity = humidity;
        }

        public override string ToString()
        {
            string roomString = base.ToString();
            string answer = roomString + $"\n\thumidity: {Humidity}\n\tdesired humidity: {DesiredHumidity}\n" +
                $"\thumidity time constant: {HumidityTimeConstant}";
            return answer;
        }

        private void UpdateHumidityWithoutDehumidifier()
        {
            Humidity += BathroomDefaults.humidityChangeStep;
            return;
        }
    }
}
