using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonsOfUncleBob.Models
{
    public class HomeSignal

    {
        public enum SignalCategory
        {
            Temperature,
            Humidity
        }
        public string Name { get; private set; }
        public string UnitOfMeasure { get; private set; }
        public Queue<float> Values { get; set; }
        public float? DesiredValue { get; set; }
        public SignalCategory Category { get; private set;}

        public HomeSignal(string name, string unitOfMeasure, SignalCategory category)
        {
            Name = name;
            UnitOfMeasure = unitOfMeasure;
            Values = new Queue<float>(capacity: PreviousValuesCount);
            Category = category;
            DesiredValue = null;
        }

        private const int PreviousValuesCount = 20;
    }
}
