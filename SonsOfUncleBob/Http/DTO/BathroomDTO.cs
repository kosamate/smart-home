
namespace SonsOfUncleBob.Http.DTO
{
    //Valamiért nem tudtuk a projekt dependency-jébe a Server projektet felvenni
    //Így egyelőre itt is megvalósítottam ezeket az osztályokat/struktúrát, hogy a http klienssel tudjak haladni
    public class BathroomDTO : RoomDTO
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
