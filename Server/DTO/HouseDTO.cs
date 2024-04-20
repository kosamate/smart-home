using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DTO
{
    internal class HouseDTO
    {
        public List<RoomDTO> Rooms { get; private set; }
        public BathroomDTO Bathroom {  get; private set; }

        public void addRoom(RoomDTO room)
        {
            this.Rooms.Add(room);
        }

        public void addBathRoom(BathroomDTO bathroom)
        {
            this.Bathroom = bathroom;
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
