using SonsOfUncleBob.Http;
using SonsOfUncleBob.Http.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonsOfUncleBob
{
    class Program_main
    {
        public static async Task Main()
        {
            Client client = new Client();

            List<RoomDTO> roomlist = await client.GetRoomsList();
            foreach (RoomDTO room in roomlist)
                Debug.WriteLine(room);

            RoomDTO roomDTO = roomlist[0];
            roomDTO.DesiredTemperature = 28.5;
            roomDTO.Light = true;
            string res = await client.PutRoom(roomDTO);
            Debug.WriteLine(res);

            roomlist = await client.GetRoomsList();
            foreach (RoomDTO room in roomlist)
                Debug.WriteLine(room);

            BathroomDTO bathroom = await client.GetBathroom();
            Debug.WriteLine(bathroom);

            bathroom.DesiredHumidity = 55.0;
            bathroom.Light = true;
            bathroom.DesiredTemperature = 18.5;
            res = await client.PutBathroom(bathroom);
            Debug.WriteLine(res);

            bathroom = await client.GetBathroom();
            Debug.WriteLine(bathroom);

            res = await client.DeleteRooms();
            Debug.WriteLine(res);

            bathroom = await client.GetBathroom();
            roomlist = await client.GetRoomsList();
            foreach (RoomDTO room in roomlist)
                Debug.WriteLine(room);
            Debug.WriteLine(bathroom);

        }   
    }    
}
