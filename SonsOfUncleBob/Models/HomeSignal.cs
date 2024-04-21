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
        public float Data { get; set; }
        public SignalCategory Category { get; private set;}

        public HomeSignal(string name, string unitOfMeasure, float data, SignalCategory category)
        {
            Name = name;
            UnitOfMeasure = unitOfMeasure;
            Data = data;
            Category = category;
        }
    }
}
