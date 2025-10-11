using static GameTime.TimeConversions;

namespace GameTime
{
    
    public partial struct Duration
    {
        
        /// <summary>
        /// Creates a new <c>Duration</c> where the timespan is equal to the specified number of days.
        /// </summary>
        /// <param name="days">The number of days elapsed.</param>
        /// <returns>A new <c>Duration</c>.</returns>
        public static Duration FromDays(int days) => new(DaysToMilliseconds(days));
        
        /// <summary>
        /// Creates a new <c>Duration</c> where the timespan is equal to the specified number of hours.
        /// </summary>
        /// <param name="hours">The number of hours elapsed.</param>
        /// <returns>A new <c>Duration</c>.</returns>
        public static Duration FromHours(int hours) => new(HoursToMilliseconds(hours));
        
        /// <summary>
        /// Creates a new <c>Duration</c> where the timespan is equal to the specified number of minutes.
        /// </summary>
        /// <param name="minutes">The number of minutes elapsed.</param>
        /// <returns>A new <c>Duration</c>.</returns>
        public static Duration FromMinutes(int minutes) => new(MinutesToMilliseconds(minutes));
        
        /// <summary>
        /// Creates a new <c>Duration</c> where the timespan is equal to the specified number of seconds.
        /// </summary>
        /// <param name="seconds">The number of seconds elapsed.</param>
        /// <returns>A new <c>Duration</c>.</returns>
        public static Duration FromSeconds(int seconds) => new(SecondsToMilliseconds(seconds));
        
        /// <summary>
        /// Creates a new <c>Duration</c> where the timespan is equal to the specified number of seconds.
        /// </summary>
        /// <param name="seconds">The number of seconds elapsed.</param>
        /// <returns>A new <c>Duration</c>.</returns>
        public static Duration FromSeconds(float seconds) => new(SecondsToMilliseconds(seconds));
        
    }
    
}