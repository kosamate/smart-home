using System.Text;
using System.Text.Json;
using System.Net;
using Server.Models;
using Server.Http.DTO;
using Common.DTO;
using Common.Defaults;

namespace Server.Http.Listener
{
    internal class HttpServer
    {
        public HouseDTO HouseDTO {  get; set; }
        public RealHouse RealHouse {  get; set; }
        public HttpListener Listener { get; set; }

        public static readonly string baseuri = "http://localhost:8000/";
        public static readonly string room = "rooms/";
        public static readonly string bathroom = "bathroom/";
        public static readonly string roomUri = string.Concat(baseuri, room);
        public static readonly string bathroomUri = string.Concat(baseuri, bathroom);

        public HttpServer()
        {
            this.HouseDTO = new HouseDTO();
            this.RealHouse = new RealHouse();
            this.Listener = new HttpListener();
            Listener.Prefixes.Add(roomUri);
            Listener.Prefixes.Add(bathroomUri);
            this.RealHouse.updateDesiredValues(this.HouseDTO);
            this.HouseDTO.UpdateMeasuredValues(this.RealHouse);
        }

        public async Task HandlerMethod(HttpListenerContext context)
        {
            HttpListenerRequest req = context.Request;
            HttpListenerResponse resp = context.Response;
            Console.WriteLine($"URL: {req.Url} \t{req.HttpMethod}");

            if (req.Url != null && req.Url.ToString().Equals(roomUri))
            {
                if (req.HttpMethod.Equals("PUT"))
                    await HandleRoomPut(req, resp);
                else if (req.HttpMethod.Equals("DELETE"))
                    await HandleRoomDelete(req, resp);
                else
                    await HandleRoomsGet(req, resp);
            }
            else
            {
                if (req.HttpMethod.Equals("PUT"))
                    await HandleBathroomPut(req, resp);
                else
                    await HandleBathroomGet(req, resp);
            }
        }

        private async Task HandleRoomPut(HttpListenerRequest req, HttpListenerResponse resp)
        {
            string reqcontent = await GetStringContent(req);

            RoomDTO? roomDTO= JsonSerializer.Deserialize<RoomDTO>(reqcontent);
            
            if (roomDTO != null)
            {
                this.HouseDTO.UpdateRoom(roomDTO);
                this.RealHouse.updateDesiredValues(this.HouseDTO);
                await BuildResponse(resp, req.ContentEncoding, $"Updated the desired values in the {roomDTO.Name}.\n");
            }
        }

        private async Task HandleRoomDelete(HttpListenerRequest req, HttpListenerResponse resp)
        {
            foreach (RoomDTO roomDTO in this.HouseDTO.Rooms)
            {
                {
                    roomDTO.DesiredTemperature = RoomDefaults.defaultDesiredTemperature;
                    roomDTO.Light = RoomDefaults.defaultLightState;
                    if (roomDTO is BathroomDTO)
                        ((BathroomDTO)roomDTO).DesiredHumidity = BathroomDefaults.defaultDesiredHumidity;
                }
            }
            this.RealHouse.updateDesiredValues(this.HouseDTO);
            await BuildResponse(resp, req.ContentEncoding, "Updated the desired values to the defaults.\n");
        }

        private async Task HandleRoomsGet(HttpListenerRequest req, HttpListenerResponse resp)
        {
            this.HouseDTO.UpdateMeasuredValues(this.RealHouse);
            List<RoomDTO> rooms = new List<RoomDTO>();
            foreach (RoomDTO roomDTO in this.HouseDTO.Rooms)
                if (!(roomDTO is BathroomDTO)) 
                    rooms.Add(roomDTO);
            string jsonString = JsonSerializer.Serialize(rooms);
            await BuildResponse(resp, req.ContentEncoding, jsonString);
        }

        private async Task HandleBathroomPut(HttpListenerRequest req, HttpListenerResponse resp)
        {
            string reqcontent = await GetStringContent(req);

            BathroomDTO? bathroomDTO = JsonSerializer.Deserialize<BathroomDTO>(reqcontent);
            
            if (bathroomDTO != null)
            {
                this.HouseDTO.UpdateRoom(bathroomDTO);
                this.RealHouse.updateDesiredValues(this.HouseDTO);
                await BuildResponse(resp, req.ContentEncoding, $"Updated the desired values in the {bathroomDTO.Name}.\n");
            }
        }

        private async Task HandleBathroomGet(HttpListenerRequest req, HttpListenerResponse resp)
        {
            this.HouseDTO.UpdateMeasuredValues(this.RealHouse);
            foreach (RoomDTO roomDTO in this.HouseDTO.Rooms)
                if (roomDTO is BathroomDTO)
                {
                    string jsonString = JsonSerializer.Serialize(roomDTO as BathroomDTO);
                    await BuildResponse(resp, req.ContentEncoding, jsonString);
                }
        }

        private async Task<string> GetStringContent(HttpListenerRequest req)
        {
            string result = "";
            using (var bodyStream = req.InputStream)
            {
                var encoding = req.ContentEncoding;
                using (var streamReader = new StreamReader(bodyStream, encoding))
                {
                    result = await streamReader.ReadToEndAsync();
                }
            }
            return result;
        }



        private async Task BuildResponse(HttpListenerResponse resp, Encoding encoding, string content)
        {
            resp.StatusCode = 200;
            byte[] buffer = encoding.GetBytes(content);
            resp.ContentLength64 = buffer.Length;
            await resp.OutputStream.WriteAsync(buffer);
            resp.OutputStream.Close();
        }
    }
}
