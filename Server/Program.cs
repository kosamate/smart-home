﻿using Server.Http.Listener;

namespace Server
{
    class Program
    {
        static async Task Main()
        {
            HttpServer server = new HttpServer();

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