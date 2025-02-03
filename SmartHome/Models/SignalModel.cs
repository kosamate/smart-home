using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.ViewModels;

namespace SmartHome.Models
{
    public class SignalModel : ObservableObject

    {
        public enum SignalCategory
        {
            Temperature,
            Humidity
        }
        public string Name { get; private set; }
        public string UnitOfMeasure { get; private set; }
        private float currentValue;
        public float CurrentValue
        {
            get => currentValue;
            set
            {
                if(currentValue != value)
                {
                    currentValue = value;
                    Notify();
                }
            }
        }

        private float? desiredValue;
        public float? DesiredValue
        {
            get => desiredValue;
            set
            {
                if(desiredValue != value)
                {
                    desiredValue = value;
                    Notify();
                }
            }
        }
        public SignalCategory Category { get; private set;}

        public SignalModel(string name, string unitOfMeasure, SignalCategory category)
        {
            Name = name;
            UnitOfMeasure = unitOfMeasure;
            CurrentValue = float.NaN;
            Category = category;
            DesiredValue = null;
        }

    }
}
