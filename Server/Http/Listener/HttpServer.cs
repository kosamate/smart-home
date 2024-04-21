using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Server.Http.DTO;
using Server.Models;

namespace Server.Http.Listener
{
    internal class HttpServer
    {
        public static HouseDTO HouseDTO {  get; set; }
        public static RealHouse RealHouse {  get; set; }
        public static HttpListener Listener { get; set; }
        public static readonly string Uri = "http://localhost:8000/house";

        public static async Task HandlerMethod(HttpListenerContext context)
        {
            HttpListenerRequest req = context.Request;
            HttpListenerResponse resp = context.Response;
            Console.WriteLine($"URL: {req.Url} \t{req.HttpMethod}");

            if (req.HttpMethod.Equals("PUT"))
                await HandleHousePut(req, resp);
            else
                await HandleHouseGet(req, resp);
        }

        private static async Task HandleHousePut(HttpListenerRequest req, HttpListenerResponse resp)
        {
            string reqcontent = await GetStringContent(req);

            JObject json = JObject.Parse(reqcontent);
            houseDTO = json.ToObject<House>();


            await BuildResponse(resp, req.ContentEncoding, "Updated the desired values in the server.");
        }

        private static async Task HandleHouseGet(HttpListenerRequest req, HttpListenerResponse resp)
        {
            string jsonString = JsonConvert.SerializeObject(houseDTO);

            await BuildResponse(resp, req.ContentEncoding, jsonString);
        }

        private static async Task<string> GetStringContent(HttpListenerRequest req)
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

        private static async Task BuildResponse(HttpListenerResponse resp, Encoding encoding, string content)
        {
            resp.StatusCode = 200;
            byte[] buffer = encoding.GetBytes(content);
            resp.ContentLength64 = buffer.Length;
            await resp.OutputStream.WriteAsync(buffer);
            resp.OutputStream.Close();
        }
    }
}
