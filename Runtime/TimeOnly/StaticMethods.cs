using static GameTime.TimeConversions;

namespace GameTime
{
    
    public partial struct TimeOnly
    {
        
        /// <summary>
        /// Creates a new <c>TimeOnly</c> from the specified hours.
        /// </summary>
        /// <param name="hours">The number of hours elapsed since midnight.</param>
        /// <returns>A new <c>TimeOnly</c>.</returns>
        public static TimeOnly FromHours(int hours) => new(hours, 0);

        /// <summary>
        /// Creates a new <c>TimeOnly</c> from the specified minutes.
        /// </summary>
        /// <param name="minutes">The number of minutes elapsed since midnight.</param>
        /// <returns>A new <c>TimeOnly</c>.</returns>
        public static TimeOnly FromMinutes(int minutes) => new(0, minutes);
        
        /// <summary>
        /// Creates a new <c>TimeOnly</c> from the specified seconds.
        /// </summary>
        /// <param name="seconds">The number of seconds elapsed since midnight.</param>
        /// <returns>A new <c>TimeOnly</c>.</returns>
        public static TimeOnly FromSeconds(int seconds) => new(SecondsToMilliseconds(seconds));
        
    }
    
}