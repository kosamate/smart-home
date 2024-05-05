using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SonsOfUncleBob.Http.DTO;

namespace SonsOfUncleBob.Http
{
    internal class Client
    {
        private static readonly HttpClient sender = new HttpClient();

        public static readonly string baseuri = "http://localhost:8000/";
        public static readonly string room = "rooms/";
        public static readonly string bathroom = "bathroom/";
        public static readonly string roomUri = string.Concat(baseuri, room);
        public static readonly string bathroomUri = string.Concat(baseuri, bathroom);

        public async Task<List<RoomDTO>> GetRoomsList()
        {
            var res = await sender.GetAsync(roomUri);
            string stringres = await res.Content.ReadAsStringAsync();
            List<RoomDTO> result = JsonSerializer.Deserialize<List<RoomDTO>>(stringres);
            return result;
        }

        public async Task<string> DeleteRooms()
        {
            var res = await sender.DeleteAsync(roomUri);
            string stringres = await res.Content.ReadAsStringAsync();
            return stringres;
        }

        public async Task<string> PutRoom(RoomDTO roomDTO)
        {
            string jsonString = JsonSerializer.Serialize(roomDTO);
            StringContent content = new StringContent(jsonString);
            var result = await sender.PutAsync(roomUri, content);
            string stringres = await result.Content.ReadAsStringAsync();
            return stringres;
        }

        public async Task<BathroomDTO> GetBathroom()
        {
            var res = await sender.GetAsync(bathroomUri);
            string stringres = await res.Content.ReadAsStringAsync();
            BathroomDTO result = JsonSerializer.Deserialize<BathroomDTO>(stringres);
            return result;
        }

        public async Task<string> PutBathroom(BathroomDTO roomDTO)
        {
            string jsonString = JsonSerializer.Serialize(roomDTO);
            StringContent content = new StringContent(jsonString);
            var result = await sender.PutAsync(bathroomUri, content);
            string stringres = await result.Content.ReadAsStringAsync();
            return stringres;
        }
    }
}
