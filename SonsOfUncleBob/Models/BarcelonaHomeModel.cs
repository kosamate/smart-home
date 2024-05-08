using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonsOfUncleBob.Models
{
    public class BarcelonaHomeModel : HomeModel
    {
        private List<RoomModel> rooms = new();
        public BarcelonaHomeModel()
        {
            foreach (string roomName in new string[] { "Kitchen", "Living Room", "Bedroom" })
                rooms.Add(
                    new RoomModel.RoomBuilder()
                    .SetName(roomName)
                    .AddSignal(new SignalModel("Temperature", "C°", SignalModel.SignalCategory.Temperature))
                    .Build()
                    );
            rooms.Add(
                new RoomModel.RoomBuilder()
                    .SetName("Bathroom")
                    .AddSignal(new SignalModel("Temperature", "C°", SignalModel.SignalCategory.Temperature))
                    .AddSignal(new SignalModel("Humidity", "%", SignalModel.SignalCategory.Humidity))
                    .Build()
                );
        }
        public override List<RoomModel> Rooms => rooms;
    }
}
