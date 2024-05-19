using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Defaults;
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

        public string Name { get => signal.Name;}
        public string DesiredValueWithUnit { get => $"{signal.DesiredValue:0.00} {signal.UnitOfMeasure}"; }

        public float? DesiredValue {
            get => (signal.DesiredValue == null) ? float.NaN : (float)signal.DesiredValue;
            set
            {
                signal.DesiredValue = value;
            }
        }

        public float CurrentValue {
            get => signal.CurrentValue;
            set
            {
                signal.CurrentValue = value;
            }
        }
        
        public string CurrentValueWithUnit { get => $"{signal.CurrentValue:0.00} {signal.UnitOfMeasure}"; }

        public Image Icon { get => icons[signal.Category]; }

        private float requestedDesiredValue = float.NaN;
        public string RequestedDesiredValue
        {
            get => requestedDesiredValue.ToString();
            set
            {
                bool validValue = float.TryParse(value, out requestedDesiredValue);
                if (validValue && requestedDesiredValue != float.NaN)
                {
                    if ((requestedDesiredValue < MinimumValue) || (requestedDesiredValue > MaximumValue))
                        IsDesiredOutOfRange = true;
                    else
                    {
                        IsDesiredOutOfRange = false;
                        DesiredValue = requestedDesiredValue;
                    }
                }
            }
        }

        private bool isDesiredOutOfRange = false;
        public bool IsDesiredOutOfRange
        {
            get => isDesiredOutOfRange;
            set
            {
                isDesiredOutOfRange = value;
                Notify();
            }
        }
       

        public string DesiredValueOutOfRangeText { get => $"The requested value is out of range! Minimum: {MinimumValue:0.00}, maximum: {MaximumValue:0.00}."; }

        public float MinimumValue
        {
            get
            {
                if (signal.Category == SignalModel.SignalCategory.Temperature)
                    return (float)RoomDefaults.temperatureMin;
                if (signal.Category == SignalModel.SignalCategory.Humidity)
                    return (float)BathroomDefaults.humidityMin;
                else
                    return (float)0.0;
            }
        }

        public float MaximumValue
        {
            get
            {
                if (signal.Category == SignalModel.SignalCategory.Temperature)
                    return (float)RoomDefaults.temperatureMax;
                if (signal.Category == SignalModel.SignalCategory.Humidity)
                    return (float)BathroomDefaults.humidityMax;
                else
                    return (float)100.0;
            }
        }


        public void ModelChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != null)
            {
                Notify(e.PropertyName);
                if (e.PropertyName == nameof(DesiredValue))
                    Notify(nameof(DesiredValueWithUnit));
                if (e.PropertyName == nameof(CurrentValue))
                    Notify(nameof(CurrentValueWithUnit));
            }
        }
    }
}
