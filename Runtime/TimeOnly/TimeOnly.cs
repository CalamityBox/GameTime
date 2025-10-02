using System;
using UnityEngine;
using static GameTime.TimeConversions;

namespace GameTime
{
    /// <summary>
    /// The <c>TimeOnly</c> struct represents the time of day, absent of a specific date structure. The
    /// time is recorded to maximum level of precision of one millisecond.
    /// </summary>
    public readonly partial struct TimeOnly : IEquatable<TimeOnly>, IComparable<TimeOnly>, IFormattable
    {
        
        /// <summary>
        /// The hour of the time using a 24-hour format. The hour is bound between 0 and 23.
        /// </summary>
        public int Hour => MillisecondsToHours(BackingTime);

        /// <summary>
        /// The minute of the time. The minute is bound between 0 and 59.
        /// </summary>
        public int Minute => MillisecondsToMinutes(BackingTime - ToMilliseconds(Hour, 0));

        /// <summary>
        /// The second of the time. The second is bound between 0 and 59.
        /// </summary>
        public int Second => MillisecondsToSeconds(BackingTime - ToMilliseconds(Hour, Minute));

        /// <summary>
        /// The millisecond of the time. The millisecond is bound between 0 and 999.
        /// </summary>
        public int Millisecond => (int)(BackingTime - ToMilliseconds(Hour, Minute, Second));

        /// <summary>
        /// The total number of milliseconds elapsed between midnight and the current time. The <c>BackingTime</c>
        /// is bound between 0 and 86,399,999.
        /// </summary>
        public int BackingTime { get; }

        /// <summary>
        /// Converts a <c>Duration</c> into a <c>TimeOnly</c>. The conversion process ignores the <c>Day</c>
        /// component of the <c>Duration</c>. If the <c>Duration</c> is positive, then the resulting
        /// <c>TimeOnly</c> will be equal to the <c>Hour</c>, <c>Minute</c>, <c>Second</c> and
        /// <c>Millisecond</c> components of the <c>Duration</c>. If the <c>Duration is negative</c>, the
        /// <c>TimeOnly</c> will "subtract" the time from midnight. For instance, if the <c>Duration</c> is
        /// negative two hours, the resulting <c>TimeOnly</c> will be 22:00:00:00 because that time is
        /// two hours from midnight.
        /// </summary>
        /// <param name="duration">The <c>Duration</c> to convert.</param>
        public TimeOnly(Duration duration) : this(duration.BackingTime) {}

        /// <summary>
        /// Creates a new <c>TimeOnly</c> at the specified time. Note that the individual components
        /// are not required to be bound by clock standards, such as the hour being between 0 and 23.
        /// The parameters will be totaled and converted into simplest form on assignment. The input
        /// can be thought of as the time elapsed since midnight. That being said, when creating a new
        /// <c>TimeOnly</c> to represent a specific time, such as 12:38 PM, the most logical and readable
        /// way to define this time is by adhering to clock conventions.
        /// </summary>
        /// <param name="hours">The total number of hours elapsed since midnight.</param>
        /// <param name="minutes">The total number of minutes elapsed since midnight.</param>
        /// <param name="seconds">The total number of seconds elapsed since midnight.</param>
        /// <param name="milliseconds">The total number of milliseconds elapsed since midnight.</param>
        public TimeOnly(int hours, int minutes, int seconds, int milliseconds) : this(ToMilliseconds(hours, minutes, seconds, milliseconds)) {}

        /// <summary>
        /// Creates a new <c>TimeOnly</c> at the specified time. Note that the individual components
        /// are not required to be bound by clock standards, such as the hour being between 0 and 23.
        /// The parameters will be totaled and converted into simplest form on assignment. The input
        /// can be thought of as the time elapsed since midnight. That being said, when creating a new
        /// <c>TimeOnly</c> to represent a specific time, such as 12:38 PM, the most logical and readable
        /// way to define this time is by adhering to clock conventions.
        /// </summary>
        /// <param name="hours">The total number of hours elapsed since midnight.</param>
        /// <param name="minutes">The total number of minutes elapsed since midnight.</param>
        /// <param name="seconds">The total number of seconds elapsed since midnight.</param>
        public TimeOnly(int hours, int minutes, int seconds = 0) : this(ToMilliseconds(hours, minutes, seconds)) {}
        
        /// <summary>
        /// Creates a new <c>TimeOnly</c> determined by the amount of elapsed time.
        /// </summary>
        /// <param name="milliseconds">The total number of milliseconds elapsed since midnight.</param>
        public TimeOnly(float milliseconds) : this(Mathf.RoundToInt(milliseconds)) {}

        /// <summary>
        /// Creates a new <c>TimeOnly</c> determined by the amount of elapsed time.
        /// </summary>
        /// <param name="milliseconds">The total number of milliseconds elapsed since midnight.</param>
        public TimeOnly(int milliseconds) : this((long)milliseconds) {}

        /// <summary>
        /// Creates a new <c>TimeOnly</c> determined by the amount of elapsed time.
        /// </summary>
        /// <param name="milliseconds">The total number of milliseconds elapsed since midnight.</param>
        public TimeOnly(long milliseconds)
        {
            // Utilize the duration struct parsing to remove excess days from the input time and handle negative inputs
            Duration duration = new Duration(milliseconds).Abs();
            int timespanWithoutDays = (int)duration.FlattenDays();
            // If the input time is less than 0, subtract the duration (excluding the days) from "midnight"
            // In other words, a negative value represents the time until midnight in milliseconds
            if (milliseconds < 0)
            {
                BackingTime = NUM_MILLISECONDS_PER_DAY - timespanWithoutDays;
            }
            else
            {
                BackingTime = timespanWithoutDays;
            }
        }

        /// <summary>
        /// Determines if this <c>TimeOnly</c> is between a lower and upper bound. The method will
        /// always assume that the upper bound is greater than the lower bound. For example, 12 PM
        /// is not between 1 PM and 2 PM. However, if the bounds are reversed such that 2 PM is the
        /// lower bound and 1 PM is the upper bound, the method will assume that the upper bound is
        /// 1 PM the following day. Therefore, 12 PM is between 2 PM and 1 PM.
        /// </summary>
        /// <param name="lower">The lower bound time, inclusive.</param>
        /// <param name="upper">The upper bound time, inclusive.</param>
        /// <returns>
        /// <c>true</c> if the <c>TimeOnly</c> is between the lower and upper bounds. Returns
        /// <c>false</c> otherwise.
        /// </returns>
        public bool IsBetween(TimeOnly lower, TimeOnly upper)
        {
            Duration duration = new(this);
            Duration lowerDuration = new(lower);
            Duration upperDuration = new(upper);
            if (lowerDuration > upperDuration)
            {
                upperDuration += Duration.OneDay;
            }
            return duration >= lowerDuration && duration <= upperDuration;
        }

        /// <summary>
        /// Determines if the <c>Hour</c> component of this <c>TimeOnly</c> is equal to
        /// the <c>Hour</c> component of another <c>TimeOnly</c>.
        /// </summary>
        /// <param name="other">The time to compare this one against.</param>
        /// <returns>
        /// <c>true</c> if the <c>TimeOnly.Hour</c> is equal to <c>other.Hour</c>.
        /// </returns>
        public bool IsHourEqual(TimeOnly other) => Hour == other.Hour;
        
        /// <summary>
        /// Determines if the <c>Minute</c> component of this <c>TimeOnly</c> is equal to
        /// the <c>Minute</c> component of another <c>TimeOnly</c>.
        /// </summary>
        /// <param name="other">The time to compare this one against.</param>
        /// <returns>
        /// <c>true</c> if the <c>TimeOnly.Minute</c> is equal to <c>other.Minute</c>.
        /// </returns>
        public bool IsMinuteEqual(TimeOnly other) => Minute == other.Minute;
        
        /// <summary>
        /// Determines if the <c>Second</c> component of this <c>TimeOnly</c> is equal to
        /// the <c>Second</c> component of another <c>TimeOnly</c>.
        /// </summary>
        /// <param name="other">The time to compare this one against.</param>
        /// <returns>
        /// <c>true</c> if the <c>TimeOnly.Second</c> is equal to <c>other.Second</c>.
        /// </returns>
        public bool IsSecondEqual(TimeOnly other) => Second == other.Second;
        
        /// <summary>
        /// Determines if the <c>Millisecond</c> component of this <c>TimeOnly</c> is equal to
        /// the <c>Millisecond</c> component of another <c>TimeOnly</c>.
        /// </summary>
        /// <param name="other">The time to compare this one against.</param>
        /// <returns>
        /// <c>true</c> if the <c>TimeOnly.Hour</c> is equal to <c>other.Hour</c>.
        /// </returns>
        public bool IsMillisecondEqual(TimeOnly other) => Millisecond == other.Millisecond;

        /// <summary>
        /// Computes the proportion of the day completed relative to midnight.
        /// </summary>
        /// <returns>A float between [0,1) that represents the proportion of the day completed.</returns>
        public float GetDayProgress() => (float)NUM_MILLISECONDS_PER_DAY / BackingTime;
        
        /// <summary>
        /// Determines if it is morning.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the <c>Hour</c> is less than 12. Returns <c>false</c> otherwise.
        /// </returns>
        public bool IsMorning() => Hour < 12;

        /// <summary>
        /// Retrieves the 12-hour time suffix.
        /// </summary>
        /// <returns>
        /// <c>"AM"</c> if it is morning. Returns <c>"PM"</c> otherwise.
        /// </returns>
        public string GetTimeSuffix() => IsMorning() ? MORNING_SUFFIX : AFTERNOON_SUFFIX;

    }
    
}
