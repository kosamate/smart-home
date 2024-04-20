using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DTO
{
    internal class BathroomDTO : RoomDTO
    {
        public double Humidity { get; set; }
        public double DesiredHumidity { get; set; }

        public override string ToString()
        {
            string roomString =  base.ToString();
            string answer = roomString + $"\thumidity: {Humidity}\n\tdesired humidity: {DesiredHumidity}";
            return answer;
        }
    }
}
