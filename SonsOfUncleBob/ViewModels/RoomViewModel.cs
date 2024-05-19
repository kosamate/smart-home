using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SonsOfUncleBob.Models;
using System.Diagnostics;
using SonsOfUncleBob.Models.EventArguments;

namespace SonsOfUncleBob.ViewModels
{
    public class RoomViewModel : ObservableObject
    {
        public RoomViewModel(RoomModel room)
        {
            this.room = room;
            foreach (SignalModel signal in room.Signals)
                signals.Add(new SignalViewModel(signal));
                
            foreach (SignalViewModel signalViewModels in signals)
                signalViewModels.PropertyChanged += ModelOrSignalViewModelsChanged;

            room.PropertyChanged += ModelOrSignalViewModelsChanged;
        }

        internal static event EventHandler<RoomEventArgs> NewDesiredValues;

        private RoomModel room;
        private List<SignalViewModel> signals = new List<SignalViewModel>();
        public string Name { get => room.Name; }
        public bool Light 
        { 
            get => room.Light; 
            set 
            {  
                room.Light = value;
                //Updating it in the server aswell.
                if (this.Signals[0].DesiredValue != null)
                {
                    RoomEventArgs eventArgs = new RoomEventArgs();
                    eventArgs.Room = this.room;
                    NewDesiredValues?.Invoke(this, eventArgs);
                }
            } 
        }
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

        public void ModelOrSignalViewModelsChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Notify(e.PropertyName);
            if (e.PropertyName != nameof(Light))
                Notify(nameof(SignalSummary));

            //If the desired value changed, it is needed to be updated in the server aswell.
            if(e.PropertyName == "DesiredValue")
            {
                if (this.Signals[0].DesiredValue != null)
                {
                    RoomEventArgs eventArgs = new RoomEventArgs();
                    eventArgs.Room = this.room;
                    NewDesiredValues?.Invoke(this, eventArgs);
                }
            }
        }
    }
}
