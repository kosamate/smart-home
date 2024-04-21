using Server.Models;

namespace Server.Http.DTO
{
    internal class HouseDTO
    {
        public List<RoomDTO> Rooms { get; set; }
        public BathroomDTO Bathroom { get; set; }

        public HouseDTO()
        {
            this.Rooms = new List<RoomDTO>();
            foreach (string roomName in new string[] { "Kitchen", "Living Room", "Bedroom" })
                this.Rooms.Add(new RoomDTO(roomName));
            this.Bathroom = new BathroomDTO("Bathroom");
        }

        public void updateMeasuredValues(RealHouse realHouse)
        {
            updateMeasuredValuesInRoomDTOs(realHouse);
            updateMeasuredValuesInBathroomDTO(realHouse);
        }


        private void updateMeasuredValuesInRoomDTOs(RealHouse realHouse)
        {
            foreach (RoomDTO roomDTO in Rooms)
                foreach (RealRoom realRoom in realHouse.Rooms)
                    if (roomDTO.Name == realRoom.Name)
                    {
                        realRoom.updateTemperature();
                        roomDTO.Temperature = realRoom.Temperature;
                    }

        }

        private void updateMeasuredValuesInBathroomDTO(RealHouse realHouse)
        {
            realHouse.Bathroom.updateTemperature();
            realHouse.Bathroom.updateHumidity();
            Bathroom.Temperature = realHouse.Bathroom.Temperature;
            Bathroom.Humidity = realHouse.Bathroom.Humidity;
        }


        public override string ToString()
        {
            string answer = "";
            foreach (RoomDTO room in Rooms)
            {
                answer += room.ToString();
            }
            answer += Bathroom.ToString();
            return answer;
        }
    }
}
