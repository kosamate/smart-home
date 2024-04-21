using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Http.DTO
{
    internal class HouseDTO
    {
        public List<RoomDTO> Rooms { get; set; }
        public BathroomDTO Bathroom { get; set; }

        public void addRoom(RoomDTO room)
        {
            Rooms.Add(room);
        }

        public void addBathRoom(BathroomDTO bathroom)
        {
            Bathroom = bathroom;
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
