using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SonsOfUncleBob.Models;

namespace SonsOfUncleBob.ViewModels
{
    public class SignalViewModel: ObservableObject
    {
        private SignalModel signal;
        private Dictionary<SignalModel.SignalCategory, Image> icons = new Dictionary<SignalModel.SignalCategory, Image>() 
        {
            { SignalModel.SignalCategory.Temperature, new Image {Source="temperature.png" } },
            { SignalModel.SignalCategory.Humidity, new Image {Source="humidity.png" } }
        };


        public SignalViewModel(SignalModel signal)
        {
            this.signal = signal;
            signal.PropertyChanged += ModelChanged;
        }

        public string Name { get => signal.Name; }
        public string DesiredValueWithUnit { get => $"{signal.DesiredValue} {signal.UnitOfMeasure}"; }

        public float? DesiredValue { get => signal.DesiredValue; }
        public float CurrentValue { get => signal.CurrentValue; }
        public string CurrentValueWithUnit { get => $"{signal.CurrentValue} {signal.UnitOfMeasure}"; }
        public Image Icon { get => icons[signal.Category]; }

        public void ModelChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Notify(e.PropertyName);
        }
    }
}
