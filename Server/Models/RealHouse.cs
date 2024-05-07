using Server.Http.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Models.Supporters;

namespace Server.Models
{
    internal class RealHouse
    {
        public List<RealRoom> Rooms { get; set; }

        public RealHouse()
        {
            this.Rooms = new List<RealRoom>();
            foreach (string roomName in new string[] { "Kitchen", "Living Room", "Bedroom" })
            {
                this.Rooms.Add(new RealRoom(roomName, 24.0, LightState.Off, RoomDefaults.defaultDesiredTemperature, 5.0));
            }
            this.Rooms.Add(new RealBathroom("Bathroom", 24.0, LightState.Off, 70.0, RoomDefaults.defaultDesiredTemperature, 5.0, BathroomDefaults.defaultDesiredHumidity, 5.0));
        }

        public void updateDesiredValues(HouseDTO houseDTO)
        {
            foreach (RealRoom room in this.Rooms)
                foreach (RoomDTO roomDTO in houseDTO.Rooms)
                    if (room.Name == roomDTO.Name)
                    {
                        room.updateDesiredTemperature(roomDTO.DesiredTemperature);
                        room.Light = roomDTO.Light;
                        if (roomDTO is BathroomDTO)
                            ((RealBathroom)room).updateDesiredHumidity(((BathroomDTO)roomDTO).DesiredHumidity);
                    }
        }

        public override string ToString()
        {
            string answer = "";
            foreach (RealRoom room in Rooms)
            {
                answer += (room.ToString() + "\n");
            }
            return answer;
        }

    }
}
