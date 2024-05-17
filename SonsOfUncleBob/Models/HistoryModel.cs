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
using System.ComponentModel.DataAnnotations;

namespace SonsOfUncleBob.Database
{
    public class HistoryModel: ObservableObject
    {
        private readonly HistoryDbContext dbContext = new();

        public HistoryModel()
        {
        
            DataProvider.NewMeasuredValues += NewMeasuredValues;
        }

        private void NewMeasuredValues(object? sender, RoomListEventArgs eventArgs)
        {
            List<Task> tasks = new List<Task>();
            foreach (RoomModel room in eventArgs.Rooms)
              tasks.Add( AddSignalRecords(room));
            Task.WaitAll(tasks.ToArray());
            dbContext.SaveChanges();
            foreach (SignalRecord sr in dbContext.Signals)
                Debug.WriteLine($"#{sr.Timestamp} Room: {sr.Room.Name}, Signal: {sr.Type.Name}, Value: {sr.Value} {sr.Type.UnitOfMeasure}");

            Notify("History");

        }

        public IEnumerable<KeyValuePair<DateTime, float>> GetSignalHistory(string roomName, string signalName, DateTime from, DateTime to)
        {

            return dbContext.Signals
                .Where(s => s.Room.Name == roomName && s.Type.Name == signalName && s.Timestamp > from && s.Timestamp < to)
                .Select(s => new KeyValuePair<DateTime, float>(s.Timestamp, s.Value))
                .OrderBy(pair => pair.Key);
        }
        private Random r = new Random();
        private async Task AddSignalRecords(RoomModel updatedRoom)
        {
            var roomTask = dbContext.Rooms.FirstOrDefaultAsync(r => r.Name == updatedRoom.Name);
            DateTime timestamp = DateTime.Now;

            foreach (SignalModel updatedSignal in updatedRoom.Signals)
            {


                var signaltypeTask = dbContext.SignalTypes.FirstOrDefaultAsync(s => s.Name == updatedSignal.Name && s.UnitOfMeasure == updatedSignal.UnitOfMeasure);

                Task? addRoomTask = null, addSignalTypeTask = null;

                var room = await roomTask;
                if (room == null)
                {
                    room = new Room { Name = updatedRoom.Name };
                    addRoomTask = AddRoom(room);
                }

                var signaltype = await signaltypeTask;
                if (signaltype == null)
                {
                    signaltype = new SignalType {Name = updatedSignal.Name, UnitOfMeasure =  updatedSignal.UnitOfMeasure };
                    addSignalTypeTask = AddSignalType(signaltype);
                }

                SignalRecord signal = new SignalRecord {Room = room, Type = signaltype, Timestamp = timestamp, Value = updatedSignal.CurrentValue };

                if (addRoomTask != null) await addRoomTask;
                if (addSignalTypeTask != null) await addSignalTypeTask;
                await dbContext.Signals.AddAsync(signal);
            }
        }

        private async Task AddRoom(Room room)
        {
            await dbContext.Rooms.AddAsync(room);
        }

        private async Task AddSignalType(SignalType signaltype)
        {
            await dbContext.SignalTypes.AddAsync(signaltype);
        }

    }
}
