using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{
    internal class Helper
    {
        internal double calculateDifferenceInSeconds(TimeSpan difference)
        {
            double hourDiff = difference.TotalHours;
            double minuteDiff = difference.TotalMinutes;
            double secondDiff = difference.TotalSeconds;
            double millisecondDiff = difference.TotalMilliseconds;
            double totalDiffSeconds = hourDiff * 3600 + minuteDiff * 60 + secondDiff + millisecondDiff;
            return totalDiffSeconds;
        }
        internal double calculateRatio(double difference, double timeConstant)
        {
            double ratio = difference / (5 * timeConstant);
            if (ratio > 1)
                ratio = 1;
            return ratio;
        }
        internal double getRandomSign()
        {
            var rand = new Random();
            double randomDouble = rand.NextDouble();
            if (randomDouble > 0.5)
                return 1.0;
            else
                return -1.0;
        }

        internal double getRandomDisturbance()
        {
            var rand = new Random();
            double sign = getRandomSign();
            double randomNumber = sign * (rand.NextDouble() / 10);
            return randomNumber;
        }
    }
}
