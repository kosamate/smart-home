using Server.Http.DTO;
using Common.DTO;
using Common.Defaults;

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
                this.Rooms.Add(new RealRoom(roomName, RoomDefaults.defaultDesiredTemperature, RoomDefaults.defaultLightState, RoomDefaults.defaultDesiredTemperature, RoomDefaults.defaultThermalTimeConstant));
            }
            this.Rooms.Add(new RealBathroom("Bathroom", RoomDefaults.defaultDesiredTemperature, RoomDefaults.defaultLightState, 70.0, RoomDefaults.defaultDesiredTemperature, RoomDefaults.defaultThermalTimeConstant, BathroomDefaults.defaultDesiredHumidity, BathroomDefaults.defaultHumidityTimeConstant));
        }

        public void updateDesiredValues(HouseDTO houseDTO)
        {
            foreach (RealRoom room in this.Rooms)
                foreach (RoomDTO roomDTO in houseDTO.Rooms)
                    if (room.Name == roomDTO.Name)
                    {
                        room.UpdateDesiredTemperature(roomDTO.DesiredTemperature);
                        room.Light = roomDTO.Light;
                        if (roomDTO is BathroomDTO)
                            ((RealBathroom)room).UpdateDesiredHumidity(((BathroomDTO)roomDTO).DesiredHumidity);
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
