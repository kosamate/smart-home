using Server.Http.DTO;
using Server.Http.Listener;
using Server.Models;
using Server.Models.Supporters;
using System.Text.Json;

namespace HttpExample.Server
{
    class Program
    {
        static async Task Main()
        {
            HttpServer server = new HttpServer();

            string jsonString = JsonSerializer.Serialize(server.HouseDTO);
            Console.WriteLine(jsonString);



            if (server.Listener != null)
                server.Listener.Start();
            else
                return;

            Console.WriteLine("Listening...");

            while (server.Listener.IsListening)
            {
                var context = await server.Listener.GetContextAsync();
                try
                {
                    await server.HandlerMethod(context);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }

            }

            server.Listener.Close();
            Console.WriteLine("Stopped listening");
            Console.ReadKey();
        }
    }
}