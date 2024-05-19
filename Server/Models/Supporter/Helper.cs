namespace Server.Models.Supporter
{
    public static class Helper
    {
        public static double CalculateDifference(double actual, double desired, TimeOnly lastAdjusted, double timeConstant)
        {
            double differenceInSeconds = CalculateDifferenceInSeconds(TimeOnly.FromDateTime(DateTime.Now), lastAdjusted);
            double ratio = CalculateRatio(differenceInSeconds, timeConstant);
            double disturbance = GetRandomDisturbance();
            return (ratio * (desired - actual) + disturbance);
        }
        
        public static double CalculateDifferenceInSeconds(TimeOnly recent, TimeOnly last)
        {
            return (recent - last).TotalSeconds;
        }
        internal static double CalculateRatio(double difference, double timeConstant)
        {
            return Math.Min(1.0, difference / (5 * timeConstant));
        }
        internal static double GetRandomSign()
        {
            var rand = new Random();
            double randomDouble = rand.NextDouble();
            if (randomDouble > 0.5)
                return 1.0;
            else
                return -1.0;
        }

        internal static double GetRandomDisturbance()
        {
            var rand = new Random();
            double sign = GetRandomSign();
            double randomNumber = sign * (rand.NextDouble() / 10);
            return randomNumber;
        }
    }
}
