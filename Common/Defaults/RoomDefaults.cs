namespace Common.Defaults
{
    public readonly struct RoomDefaults
    {
        public const double defaultDesiredTemperature = 21.0;
        public const double defaultThermalTimeConstant = 5.0;
        public const double temperatureMax = 28.0;
        public const double temperatureMin = 15.0;
        public const double temperatureInsensitivity = 0.5;
        public const double temperatureChangeStep = 0.1;
        public const bool defaultLightState = false;
    }
}
