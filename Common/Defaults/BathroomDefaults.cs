namespace Common.Defaults
{
    public readonly struct BathroomDefaults
    {
        public const double defaultDesiredHumidity = 40.0;
        public const double defaultHumidityTimeConstant = 5.0;
        public const double humidityMax = 70.0;
        public const double humidityMin = 30.0;
        public const double humidityInsensitivity = 4.0;
        public const double humidityChangeStep = 1.0;
    }
}
