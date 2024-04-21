using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Server.Models;
using Server.Http.DTO;
using Server.Http.Listener;

namespace HttpExample.Server
{
    class Program
    {
         private static HttpServer server = new HttpServer();

        static async Task Main()
        {
            server = new HouseDTO();
            server.RealHouse = new RealHouse();
            server.Listener = new HttpListener();

            listener.Prefixes.Add(Uri);

            listener.Start();
            Console.WriteLine($"Listening...");

            while (listener.IsListening)
            {
                var context = await listener.GetContextAsync();
                try
                {
                    await HttpServer.HandlerMethod(context);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }

            }

            listener.Close();
            Console.WriteLine("Stopped listening");
            Console.ReadKey();
        }


        


    }
}
