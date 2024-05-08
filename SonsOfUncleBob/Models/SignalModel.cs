using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonsOfUncleBob.Models
{
    public class SignalModel

    {
        public enum SignalCategory
        {
            Temperature,
            Humidity
        }
        public string Name { get; private set; }
        public string UnitOfMeasure { get; private set; }
        public float CurrentValue { get; set; }
        public float? DesiredValue { get; set; }
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
