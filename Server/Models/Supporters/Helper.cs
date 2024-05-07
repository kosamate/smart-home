using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models.Supporters
{
    public static class Helper
    {
        public static double calculateDifference(double actual, double desired, TimeOnly lastAdjusted, double timeConstant)
        {
            double differenceInSeconds = calculateDifferenceInSeconds(TimeOnly.FromDateTime(DateTime.Now), lastAdjusted);
            double ratio = calculateRatio(differenceInSeconds, timeConstant);
            double disturbance = getRandomDisturbance();
            return (ratio * (desired - actual) + disturbance);
        }
        
        public static double calculateDifferenceInSeconds(TimeOnly recent, TimeOnly last)
        {
            return (recent - last).TotalSeconds;
        }
        internal static double calculateRatio(double difference, double timeConstant)
        {
            return Math.Min(1.0, difference / (5 * timeConstant));
        }
        internal static double getRandomSign()
        {
            var rand = new Random();
            double randomDouble = rand.NextDouble();
            if (randomDouble > 0.5)
                return 1.0;
            else
                return -1.0;
        }

        internal static double getRandomDisturbance()
        {
            var rand = new Random();
            double sign = getRandomSign();
            double randomNumber = sign * (rand.NextDouble() / 10);
            return randomNumber;
        }
    }
}
