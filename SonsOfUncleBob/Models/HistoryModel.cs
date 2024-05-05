using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SonsOfUncleBob.Models;
using SonsOfUncleBob.ViewModels;
using SonsOfUncleBob.Http;
using Microsoft.EntityFrameworkCore;

namespace SonsOfUncleBob.Database
{
    public class HistoryModel
    {
        private readonly HistoryDbContext dbContext = new();

        public HistoryModel()
        {
            throw new NotImplementedException();
            HttpDataProvider.Instance.DataReceived += DataReceived;
        }

        public IEnumerable<KeyValuePair<DateTime, float>> GetSignalHistory(Models.Room room, string signalName, DateTime from, DateTime to)
        {

            return dbContext.Signals
                .Where(s => s.Room.Name == room.Name && s.Type.Name == signalName && s.Timestamp > from && s.Timestamp < to)
                .Select(s => new KeyValuePair<DateTime, float>(s.Timestamp, s.Value))
                .OrderBy(pair => pair.Key);
        }

        private void DataReceived(object sender, EventArgs e)
        {
            throw new NotImplementedException();
            var result = AddSignalRecord("", "", "", DateTime.Now, 1.0f);
            result.Wait();
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
