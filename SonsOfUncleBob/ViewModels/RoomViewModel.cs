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
        public RoomViewModel(RoomModel room)
        {
            this.room = room;
            foreach (SignalModel signal in room.Signals)
                signals.Add(new SignalViewModel(signal));
        }
        private RoomModel room;
        private List<SignalViewModel> signals = new List<SignalViewModel>();
        public string Name { get => room.Name; }
        public string Light { get => room.Light ? "On" : "Off"; }
        public string SignalSummary
        {
            get {
                string summary = "";
                foreach (SignalViewModel signal in signals)
                    summary += $"{signal.Name}: {signal.CurrentValueWithUnit}\n";
                return summary;
            }
        }
        public List<SignalViewModel> Signals { get => signals; }

    }
}
