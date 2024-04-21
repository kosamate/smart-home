using System.Text;
using System.Text.Json;
using System.Net;
using Server.Http.DTO;
using Server.Models;

namespace Server.Http.Listener
{
    internal class HttpServer
    {
        public HouseDTO? HouseDTO {  get; set; }
        public RealHouse? RealHouse {  get; set; }
        public HttpListener? Listener { get; set; }
        public readonly string Uri = "http://localhost:8000/house/";

        public HttpServer()
        {
            this.HouseDTO = new HouseDTO();
            this.RealHouse = new RealHouse();
            this.Listener = new HttpListener();
            Listener.Prefixes.Add(Uri);
        }

        public async Task HandlerMethod(HttpListenerContext context)
        {
            HttpListenerRequest req = context.Request;
            HttpListenerResponse resp = context.Response;
            Console.WriteLine($"URL: {req.Url} \t{req.HttpMethod}");

            if (req.HttpMethod.Equals("PUT"))
                await HandleHousePut(req, resp);
            else
                await HandleHouseGet(req, resp);
        }

        private async Task HandleHousePut(HttpListenerRequest req, HttpListenerResponse resp)
        {
            string reqcontent = await GetStringContent(req);

            HouseDTO? houseDTO= JsonSerializer.Deserialize<HouseDTO>(reqcontent);
            this.HouseDTO = houseDTO;
            if (this.RealHouse != null && this.HouseDTO != null)
            {
                this.RealHouse.updateDesiredValues(this.HouseDTO);
                await BuildResponse(resp, req.ContentEncoding, "Updated the desired values in the server.");
            }
            else
                return;
            
        }

        private async Task HandleHouseGet(HttpListenerRequest req, HttpListenerResponse resp)
        {

            if (this.HouseDTO != null && this.RealHouse != null)
            {
                this.HouseDTO.updateMeasuredValues(this.RealHouse);
                string jsonString = JsonSerializer.Serialize(HouseDTO);
                await BuildResponse(resp, req.ContentEncoding, jsonString);

            }
            else
                return;
            
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
