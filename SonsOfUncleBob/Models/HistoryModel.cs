using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SonsOfUncleBob.Models;
using SonsOfUncleBob.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using SonsOfUncleBob.Models.EventArguments;
using SonsOfUncleBob.Http;

namespace SonsOfUncleBob.Database
{
    public class HistoryModel
    {
        private readonly HistoryDbContext dbContext = new();
        private List<RoomModel> rooms = new();

        public HistoryModel()
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
            DataProvider.NewMeasuredValues += NewMeasuredValues;
        }

        private void NewMeasuredValues(object sender, RoomListEventArgs eventArgs)
        {
            foreach (RoomModel room in this.rooms)
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

        public IEnumerable<KeyValuePair<DateTime, float>> GetSignalHistory(string roomName, string signalName, DateTime from, DateTime to)
        {

            return dbContext.Signals
                .Where(s => s.Room.Name == roomName && s.Type.Name == signalName && s.Timestamp > from && s.Timestamp < to)
                .Select(s => new KeyValuePair<DateTime, float>(s.Timestamp, s.Value))
                .OrderBy(pair => pair.Key);
        }

        private async Task AddSignalRecord(string roomName, string signalName, string unitOfMeasure, DateTime timestamp, float signalValue)
        {
            var roomTask = dbContext.Rooms.FirstOrDefaultAsync(r => r.Name == roomName);
            var signaltypeTask = dbContext.SignalTypes.FirstOrDefaultAsync(s => s.Name == signalName && s.UnitOfMeasure == unitOfMeasure);

            var room = await roomTask;
            if (room == null)
            {
                room = new Database.Room(roomName);
                await AddRoom(room);
            }

            var signaltype = await signaltypeTask;
            if (signalName == null)
            {
                signaltype = new Database.SignalType(signalName, unitOfMeasure);
                await AddSignalType(signaltype);
            }

            Database.SignalRecord signal = new(room, signaltype, timestamp, signalValue);
            await dbContext.Signals.AddAsync(signal);
            dbContext.SaveChanges();
        }

        private async Task AddRoom(Database.Room room)
        {
            await dbContext.Rooms.AddAsync(room);
        }

        private async Task AddSignalType(Database.SignalType signaltype)
        {
            await dbContext.SignalTypes.AddAsync(signaltype);
        }

    }
}
