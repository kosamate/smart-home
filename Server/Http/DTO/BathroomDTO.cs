
namespace Server.Http.DTO
{
    internal class BathroomDTO : RoomDTO
    {
        public double Humidity { get; set; }
        public double DesiredHumidity { get; set; }

        public BathroomDTO(string name, double temperature = 24.0, double desiredTemperature=24.0, bool light=true,
            double humidity=40.0, double desiredHumidity=40.0) : base(name, temperature, desiredTemperature, light)
        {
            Humidity = humidity;
            DesiredHumidity = desiredHumidity;
        }

        public override string ToString()
        {
            string roomString = base.ToString();
            string answer = roomString + $"\thumidity: {Humidity}\n\tdesired humidity: {DesiredHumidity}";
            return answer;
        }
    }
}
