using Common.Defaults;
using Common.DTO;
using Server.Models;

namespace Server.Http.DTO
{
    internal class HouseDTO
    {
        public List<RoomDTO> Rooms { get; set; }

        public HouseDTO()
        {
            this.Rooms = new List<RoomDTO>();
            foreach (string roomName in new string[] { "Kitchen", "Living Room", "Bedroom" })
                this.Rooms.Add(new RoomDTO(roomName, 24.0, RoomDefaults.defaultDesiredTemperature, RoomDefaults.defaultLightState));
            this.Rooms.Add(new BathroomDTO("Bathroom", 24.0, RoomDefaults.defaultDesiredTemperature, RoomDefaults.defaultLightState, 70.0, BathroomDefaults.defaultDesiredHumidity));
        }

        public void UpdateRoom(RoomDTO roomDTO)
        {
            int index = Rooms.FindIndex(room => room.Name == roomDTO.Name);
            if (index != -1)
                Rooms[index] = roomDTO;
        }

        public void UpdateMeasuredValues(RealHouse realHouse)
        {
            foreach (RoomDTO roomDTO in Rooms)
                foreach (RealRoom realRoom in realHouse.Rooms)
                    if (roomDTO.Name == realRoom.Name)
                    {
                        realRoom.UpdateMeasuredValues();
                        roomDTO.Temperature = realRoom.Temperature;
                        roomDTO.DesiredTemperature = realRoom.DesiredTemperature;
                        if (realRoom is RealBathroom)
                        {
                            ((BathroomDTO)roomDTO).Humidity = ((RealBathroom)realRoom).Humidity;
                            ((BathroomDTO)roomDTO).DesiredHumidity = ((RealBathroom)realRoom).DesiredHumidity;
                        }
                    }
        }

        public override string ToString()
        {
            string answer = "";
            foreach (RoomDTO room in Rooms)
            {
                answer += (room.ToString() + "\n");
            }
            return answer;
        }
    }
}
