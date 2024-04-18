using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class Room
    {
        public string Name { get; set; }
        public float Temperature { get; set; }
        public float DesiredTemperature { get; set; }
        //TimeConstant in seconds
        public float TimeConstant { get; set; }
        public TimeOnly LastRead { get; set; }
        public void updateTemperature()
        {
            var timeNow = TimeOnly.FromDateTime(DateTime.Now);
            var timeDifference = timeNow - LastRead;
            LastRead = timeNow;
            float differenceInMinutes = calculateDifferenceInMinutes(timeDifference);
            float ratio = (differenceInMinutes) / (5 * TimeConstant);
            if (ratio > 1)
                ratio = 1;
            Temperature = Temperature + ratio * (DesiredTemperature - Temperature);
        }

        private float calculateDifferenceInMinutes(TimeSpan difference)
        {
            float hoursDiff = (float)difference.TotalHours;
            float minutesDiff = (float)difference.TotalMinutes;
            float secondsDiff = (float)difference.TotalSeconds;
            float totalDiffMinutes = hoursDiff * 60 + minutesDiff + (secondsDiff / 60);
            return totalDiffMinutes;
        }



    }
}
