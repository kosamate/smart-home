using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SonsOfUncleBob.Models;

namespace SonsOfUncleBob.ViewModels
{
    public class RoomViewModel
    {
        private Room room;
        private List<HomeSignalViewModel> signals = new List<HomeSignalViewModel>();
        public string Name { get => room.Name; }
        public string Light { get => room.Light ? "On" : "Off"; }
        public string SignalSummary
        {
            get {
                string summary = "";
                foreach (HomeSignal signal in room.Signals)
                    summary += $"{signal.Name}: {signal.Values.Peek()} {signal.UnitOfMeasure}\n";
                return summary;
            }
        }
        public IEnumerable<HomeSignalViewModel> Signals { get => signals; }

        public RoomViewModel(Room room)
        {
            this.room = room;
            foreach (HomeSignal signal in room.Signals)
                signals.Add(new HomeSignalViewModel(signal));
        }
    }
}
