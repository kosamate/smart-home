using System.Text;
using System.Text.Json;
using System.Net;
using Server.Http.DTO;
using Server.Models;

namespace Server.Http.Listener
{
    internal class HttpServer
    {
        public HouseDTO HouseDTO {  get; set; }
        public RealHouse RealHouse {  get; set; }
        public HttpListener Listener { get; set; }

        public static readonly string baseuri = "http://localhost:8000/";
        public static readonly string room = "room/";
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
                else
                    await HandleRoomGet(req, resp);
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
                this.HouseDTO.updateRoom(roomDTO);
                this.RealHouse.updateDesiredValues(this.HouseDTO);
                await BuildResponse(resp, req.ContentEncoding, $"Updated the desired values in the {roomDTO.Name}.");
            }
        }

        private async Task HandleRoomGet(HttpListenerRequest req, HttpListenerResponse resp)
        {
            this.HouseDTO.updateMeasuredValues(this.RealHouse);
            foreach (RoomDTO roomDTO in this.HouseDTO.Rooms)
                if (!(roomDTO is BathroomDTO))
                { 
                    string jsonString = JsonSerializer.Serialize(roomDTO);
                    await BuildResponse(resp, req.ContentEncoding, jsonString);
                }
        }

        private async Task HandleBathroomPut(HttpListenerRequest req, HttpListenerResponse resp)
        {
            string reqcontent = await GetStringContent(req);

            BathroomDTO? bathroomDTO = JsonSerializer.Deserialize<BathroomDTO>(reqcontent);
            
            if (bathroomDTO != null)
            {
                this.HouseDTO.updateRoom(bathroomDTO);
                this.RealHouse.updateDesiredValues(this.HouseDTO);
                await BuildResponse(resp, req.ContentEncoding, $"Updated the desired values in the {bathroomDTO.Name}.");
            }
        }

        private async Task HandleBathroomGet(HttpListenerRequest req, HttpListenerResponse resp)
        {
            this.HouseDTO.updateMeasuredValues(this.RealHouse);
            foreach (RoomDTO roomDTO in this.HouseDTO.Rooms)
                if (roomDTO is BathroomDTO)
                {
                    string jsonString = JsonSerializer.Serialize(HouseDTO);
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
