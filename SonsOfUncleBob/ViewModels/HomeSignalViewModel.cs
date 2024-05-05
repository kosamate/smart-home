using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SonsOfUncleBob.Models;

namespace SonsOfUncleBob.ViewModels
{
    public class HomeSignalViewModel
    {
        private HomeSignal signal;
        private Dictionary<HomeSignal.SignalCategory, Image> icons = new Dictionary<HomeSignal.SignalCategory, Image>() 
        {
            { HomeSignal.SignalCategory.Temperature, new Image {Source="Resources/Images/temperature.png" } },
            { HomeSignal.SignalCategory.Humidity, new Image {Source="Resources/Images/humidity.png" } }
        };


        public HomeSignalViewModel(HomeSignal signal)
        {
            this.signal = signal;
        }

        public string SignalName { get => signal.Name; }
        public string DesiredValue { get => $"{signal.DesiredValue} {signal.UnitOfMeasure}"; }
        public float CurrentValue { get => signal.Values.Peek(); }
        public IEnumerable<float> RecentValues { get => signal.Values; }
        public Image Icon { get => icons[signal.Category]; }
    }
}
