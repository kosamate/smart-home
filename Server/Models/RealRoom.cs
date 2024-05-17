using Server.Models.Supporter;
using Common.Defaults;

namespace Server.Models
{
    internal class RealRoom
    {
        public string Name { get; }
        public double Temperature { get; private set; }
        public bool Light { get; set; }
        public double DesiredTemperature { get; private set; }
        //Thermal time constant in seconds (typical value is between 15 and 30 minutes)
        public double ThermalTimeConstant { get; }
        public TimeOnly LastAdjusted { get; protected set; }

        private bool hasReachedTheDesiredTemperature;

        public RealRoom(string name, double temperature, bool light,
                    double desiredTemperature = RoomDefaults.defaultDesiredTemperature,
                    double timeConstant = RoomDefaults.defaultThermalTimeConstant)
        {
            this.Name = name;

            if (temperature < RoomDefaults.temperatureMin)
                this.Temperature = RoomDefaults.temperatureMin;
            else if (temperature > RoomDefaults.temperatureMax)
                this.Temperature = RoomDefaults.temperatureMax;
            else
                this.Temperature = temperature;

            this.Light = light;

            if (desiredTemperature < RoomDefaults.temperatureMin || desiredTemperature > RoomDefaults.temperatureMax)
                this.DesiredTemperature = RoomDefaults.defaultDesiredTemperature;
            else
                this.DesiredTemperature = desiredTemperature;

            if (timeConstant <= 0.0)
                this.ThermalTimeConstant = RoomDefaults.defaultThermalTimeConstant;
            else
                this.ThermalTimeConstant = timeConstant;

            this.LastAdjusted = TimeOnly.FromDateTime(DateTime.Now);

            if (this.Temperature <= (this.DesiredTemperature + 0.01) && this.Temperature >= (this.DesiredTemperature - 0.01))
                this.hasReachedTheDesiredTemperature = true;
            else
                this.hasReachedTheDesiredTemperature = false;
        }

        public virtual void UpdateMeasuredValues()
        {
            if (Temperature <= (DesiredTemperature + RoomDefaults.temperatureInsensitivity)
                && Temperature >= (DesiredTemperature - RoomDefaults.temperatureInsensitivity)
                && hasReachedTheDesiredTemperature)
            {
                UpdateTemperatureBetweenInsensibility();
                hasReachedTheDesiredTemperature = !IsOutOfInsensivityRange();
                return;
            }

            double difference = Helper.CalculateDifference(Temperature, DesiredTemperature, LastAdjusted, ThermalTimeConstant);
            Temperature += difference;

            hasReachedTheDesiredTemperature = IsTemperatureAndDesiredTemperatureEqual();
        }

        public void UpdateDesiredTemperature(double temperature)
        {
            LastAdjusted = TimeOnly.FromDateTime(DateTime.Now);
            if (temperature < RoomDefaults.temperatureMin)
                DesiredTemperature = RoomDefaults.temperatureMin;
            else if (temperature > RoomDefaults.temperatureMax)
                DesiredTemperature = RoomDefaults.temperatureMax;
            else
                DesiredTemperature = temperature;
        }

        public override string ToString()
        {
            string light = "Off";
            if (Light)
                light = "On";
            return $"{Name}:\n\tlight: {light}\n\ttemperature: {Temperature},\n\tdesired temperature: {DesiredTemperature},\n" +
                $"\tthermal time constant: {ThermalTimeConstant}\n\tlast adjusted: {LastAdjusted.ToString()},\n" +
                $"\ttime now: {TimeOnly.FromDateTime(DateTime.Now).ToString()}\n" +
                $"\ttime between them: {(TimeOnly.FromDateTime(DateTime.Now)-LastAdjusted).ToString()}\n" +
                $"\tis reached the desired temperature: {hasReachedTheDesiredTemperature}";
        }

        //The outside temperature can be more or less than the inside, so the inside temperature can change up or down.
        //Now I decide this with the help of a random number generator.
        private void UpdateTemperatureBetweenInsensibility()
        {
            if (IsTemperatureAndDesiredTemperatureEqual())
            {
                double sign = Helper.GetRandomSign();
                Temperature += (sign * RoomDefaults.temperatureChangeStep);
            }
            else
            {
                if (Temperature > DesiredTemperature)
                    Temperature += RoomDefaults.temperatureChangeStep;
                else
                    Temperature -= RoomDefaults.temperatureChangeStep;
            }

        }

        private bool IsOutOfInsensivityRange()
        {
            return (Temperature >= (DesiredTemperature + RoomDefaults.temperatureInsensitivity)
                    || Temperature <= (DesiredTemperature - RoomDefaults.temperatureInsensitivity));
        }

        private bool IsTemperatureAndDesiredTemperatureEqual()
        {
            return (Temperature <= (DesiredTemperature + 0.01) && Temperature >= (DesiredTemperature - 0.01));
        }
    }
}
