using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonsOfUncleBob.Models
{
    public class BarcelonaHome : Home
    {
        private List<Room> rooms = new();
        public BarcelonaHome()
        {
            foreach (string roomName in new string[] { "Kitchen", "Living Room", "Bedroom" })
                rooms.Add(
                    new Room.RoomBuilder()
                    .SetName(roomName)
                    .AddSignal(new HomeSignal("Temperature", "C°", 20f, HomeSignal.SignalCategory.Temperature))
                    .Build()
                    );
            rooms.Add(
                new Room.RoomBuilder()
                    .SetName("Bathroom")
                    .AddSignal(new HomeSignal("Temperature", "C°", 20f, HomeSignal.SignalCategory.Temperature))
                    .AddSignal(new HomeSignal("Humidity", "%", 20f, HomeSignal.SignalCategory.Humidity))
                    .Build()
                );


        }
        public override List<Room> Rooms => rooms;
    }
}
