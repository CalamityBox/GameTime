using static GameTime.TimeConversions;

namespace GameTime
{
    public partial struct Duration
    {
        /// <summary>
        /// A <c>Duration</c> representing a timespan of zero milliseconds elapsed. This functions
        /// as the additive identity such that <c>myDuration + Zero = myDuration</c> and
        /// <c>myTimeOnly + Zero = myTimeOnly</c>.
        /// </summary>
        public static Duration Zero = new(0);
        
        /// <summary>
        /// A <c>Duration</c> representing a timespan of one millisecond elapsed. This is the smallest
        /// non-zero-in-magnitude increment of time that can elapse.
        /// </summary>
        public static Duration OneMillisecond = new(1);
        
        /// <summary>
        /// A <c>Duration</c> representing a timespan of one second elapsed.
        /// </summary>
        public static Duration OneSecond = new(SecondsToMilliseconds(1));
        
        /// <summary>
        /// A <c>Duration</c> representing a timespan of one minute elapsed.
        /// </summary>
        public static Duration OneMinute = new(MinutesToMilliseconds(1));
        
        /// <summary>
        /// A <c>Duration</c> representing a timespan of one hour elapsed.
        /// </summary>
        public static Duration OneHour = new(HoursToMilliseconds(1));
        
        /// <summary>
        /// A <c>Duration</c> representing a timespan of one day elapsed. This functions as
        /// an additional <c>TimeOnly</c> additive identity such that
        /// <c>myTimeOnly + OneDay = myTimeOnly</c>.
        /// </summary>
        public static Duration OneDay = new(DaysToMilliseconds(1));
    }
}