
namespace SonsOfUncleBob.Http.DTO
{
    internal class BathroomDTO : RoomDTO
    {
        public double Humidity { get; set; }
        public double DesiredHumidity { get; set; }

        public BathroomDTO(string name, double temperature, double desiredTemperature, bool light,
            double humidity, double desiredHumidity) : base(name, temperature, desiredTemperature, light)
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
