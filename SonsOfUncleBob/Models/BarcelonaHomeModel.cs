using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using SonsOfUncleBob.Models.EventArguments;
using SonsOfUncleBob.Http;

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
            DataProvider.NewMeasuredValues += updateMeasuredValues;
        }
        public override List<RoomModel> Rooms => rooms;

        private void updateMeasuredValues(object sender, RoomListEventArgs eventArgs)
        {
            foreach (RoomModel room in this.Rooms)
                foreach (RoomModel updatedRoom in eventArgs.Rooms)
                {
                    if (room.Name == updatedRoom.Name)
                    {
                        room.Light = updatedRoom.Light;
                        foreach (SignalModel signal in room.Signals)
                            foreach (SignalModel updatedSignal in updatedRoom.Signals)
                                if (signal.Category == updatedSignal.Category)
                                {
                                    signal.CurrentValue = updatedSignal.CurrentValue;
                                    signal.DesiredValue = updatedSignal.DesiredValue;
                                }
                    }
                }
        }

    }
}
