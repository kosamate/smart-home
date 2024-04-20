using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DTO
{
    internal class RoomDTO
    {
        public string Name { get; set; }
        public double Temperature { get; set; }
        public double DesiredTemperature { get; set; }
        public bool Light {  get; set; }

        public override string ToString()
        {
            string light = "Off";
            if (Light)
                light = "On";
            return $"{Name}:\n\ttemperature: {Temperature},\n\tdesired temperature: {DesiredTemperature}\n\tlight: {light}\n";
        }
    }
}
