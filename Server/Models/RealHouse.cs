using Server.Http.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{
    internal class RealHouse
    {
        public List<RealRoom> Rooms { get; set; }
        public RealBathroom Bathroom { get; set; }

        public RealHouse()
        {
            foreach (string roomName in new string[] { "Kitchen", "Living Room", "Bedroom" })
            {
                this.Rooms.Add(new RealRoom(roomName));
            }
            this.Bathroom = new RealBathroom("Bathroom");
        }

        public void updateDesiredValues(HouseDTO houseDTO)
        {
            updateDesiredValuesInRooms(houseDTO);
            updateDesiredValuesInBathroom(houseDTO);
        }

        private void updateDesiredValuesInRooms(HouseDTO houseDTO)
        {
            foreach (RealRoom room in this.Rooms)
                foreach (RoomDTO roomDTO in houseDTO.Rooms)
                    if (room.Name == roomDTO.Name)
                    {
                        room.updateDesiredTemperature(roomDTO.DesiredTemperature);
                        room.Light = roomDTO.Light;
                    }
        }

        private void updateDesiredValuesInBathroom(HouseDTO houseDTO)
        {
            this.Bathroom.updateDesiredTemperature(houseDTO.Bathroom.DesiredTemperature);
            this.Bathroom.updateDesiredHumidity(houseDTO.Bathroom.DesiredHumidity);
            this.Bathroom.Light = houseDTO.Bathroom.Light;
        }

     
    }
}
