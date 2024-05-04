using Server.Models;
using Server.Models.Supporters;

namespace Server.Http.DTO
{
    internal class HouseDTO
    {
        public List<RoomDTO> Rooms { get; set; }

        public HouseDTO()
        {
            this.Rooms = new List<RoomDTO>();
            foreach (string roomName in new string[] { "Kitchen", "Living Room", "Bedroom" })
                this.Rooms.Add(new RoomDTO(roomName, 24.0, 21.0, LightState.Off));
            this.Rooms.Add(new BathroomDTO("Bathroom", 24.0, 21.0, LightState.Off, 70.0, 40.0));
        }

        public void updateRoom(RoomDTO roomDTO)
        {
            int index = Rooms.FindIndex(room => room.Name == roomDTO.Name);
            if (index != -1)
                Rooms[index] = roomDTO;
        }

        public void updateMeasuredValues(RealHouse realHouse)
        {
            foreach (RoomDTO roomDTO in Rooms)
                foreach (RealRoom realRoom in realHouse.Rooms)
                    if (roomDTO.Name == realRoom.Name)
                    {
                        realRoom.updateMeasuredValues();
                        roomDTO.Temperature = realRoom.Temperature;
                        if (realRoom is RealBathroom)
                            ((BathroomDTO)roomDTO).Humidity = ((RealBathroom)realRoom).Humidity;
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
