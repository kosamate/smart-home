using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Http.DTO
{
    public class RoomDTO
    {
        public string Name { get; set; }
        public double Temperature { get; set; }
        public double DesiredTemperature { get; set; }
        public bool Light { get; set; }

        
        public RoomDTO(string name, double temperature, double desiredTemperature, bool light)
        {
            Name = name;
            Temperature = temperature;
            DesiredTemperature = desiredTemperature;
            Light = light;
        }
        

        public override string ToString()
        {
            string light = "Off";
            if (Light == true)
                light = "On";
            return $"{Name}:\n\ttemperature: {Temperature}\n\tdesired temperature: {DesiredTemperature}\n\tlight: {light}\n";
        }
    }
}
