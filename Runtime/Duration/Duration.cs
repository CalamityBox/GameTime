using System;
using JetBrains.Annotations;
using UnityEngine;
using static GameTime.TimeConversions;

namespace GameTime
{
    /// <summary>
    /// The <c>Duration</c> struct represents a length of time in milliseconds.
    /// </summary>
    public readonly partial struct Duration : IEquatable<Duration>, IComparable<Duration>
    {

        /// <summary>
        /// The number of days of the <c>Duration</c>. Note that if the duration is negative,
        /// then the <c>Day</c> component will also be negative.
        /// </summary>
        public int Day => MillisecondsToDays(BackingTime);

        /// <summary>
        /// The number of hours of the <c>Duration</c> in simplest form, relative to a 24-hour day.
        /// The maximum absolute value of <c>Hour</c> is 23. Adding an additional hour of time span
        /// will increment the <c>Day</c> component by one and the <c>Hour</c> will become zero.
        /// Note that if the duration is negative, then the <c>Hour</c> component will also be negative.
        /// </summary>
        public int Hour 
            => MillisecondsToHours(BackingTime - ToMilliseconds(Day, 0, 0, 0, 0));
        
        /// <summary>
        /// The number of minutes of the <c>Duration</c> in simplest form, relative to a clock with 60 minutes per hour.
        /// The maximum absolute value of <c>Minute</c> is 59. Adding an additional minute of time span
        /// will increment the <c>Hour</c> component by one and the <c>Minute</c> will become zero.
        /// Note that if the duration is negative, then the <c>Minute</c> component will also be negative.
        /// </summary>
        public int Minute 
            => MillisecondsToMinutes(BackingTime - ToMilliseconds(Day, Hour, 0, 0, 0));

        /// <summary>
        /// The number of seconds of the <c>Duration</c> in simplest form, relative to a clock with 60 seconds per minute.
        /// The maximum absolute value of <c>Second</c> is 59. Adding an additional second of time span
        /// will increment the <c>Minute</c> component by one and the <c>Second</c> will become zero.
        /// Note that if the duration is negative, then the <c>Second</c> component will also be negative.
        /// </summary>
        public int Second 
            => MillisecondsToSeconds(BackingTime - ToMilliseconds(Day, Hour, Minute, 0, 0));

        /// <summary>
        /// The number of milliseconds of the <c>Duration</c> in simplest form, relative to a clock with 1000 milliseconds per second.
        /// The maximum absolute value of <c>Millisecond</c> is 999. Adding an additional millisecond of time span
        /// will increment the <c>Second</c> component by one and the <c>Millisecond</c> will become zero.
        /// Note that if the duration is negative, then the <c>Millisecond</c> component will also be negative.
        /// </summary>
        public int Millisecond 
            => (int)(BackingTime - ToMilliseconds(Day, Hour, Minute, Second, 0));

        /// <summary>
        /// The total number of milliseconds of the <c>Duration</c> timespan.
        /// </summary>
        public long BackingTime { get; }

        /// <summary>
        /// Converts a <c>TimeOnly</c> struct into a <c>Duration</c>. The <c>TimeOnly</c> will be
        /// interpreted as the timespan between midnight and the <c>TimeOnly</c>. For example, the
        /// <c>TimeOnly</c> representing 8:00 AM would be converted to a <c>Duration</c> of 8 hours.
        /// </summary>
        /// <param name="timeOnly">The input time to convert.</param>
        public Duration(TimeOnly timeOnly) : this(timeOnly.BackingTime) {}

        /// <summary>
        /// Creates a new <c>Duration</c> struct. Note that the timespan parameters are not bounded by clock
        /// standards. For instance, the number of hours can be <c>1,000</c> and the number of minutes can be
        /// <c>12,943</c>. The parameters will be totaled and converted into simplest form on assignment.
        /// </summary>
        /// <param name="days">The total number of days in the <c>Duration</c>.</param>
        /// <param name="hours">The total number of hours in the <c>Duration</c>.</param>
        /// <param name="minutes">The total number of minutes in the <c>Duration</c>.</param>
        /// <param name="seconds">The total number of seconds in the <c>Duration</c>.</param>
        /// <param name="milliseconds">The total number of milliseconds in the <c>Duration</c>.</param>
        public Duration(int days, int  hours, int minutes, int seconds, int milliseconds) : this(ToMilliseconds(days, hours, minutes, seconds, milliseconds)) {}
        
        /// <summary>
        /// Creates a new <c>Duration</c> struct. Note that the timespan parameters are not bounded by clock
        /// standards. For instance, the number of hours can be <c>1,000</c> and the number of minutes can be
        /// <c>12,943</c>. They parameters will be totaled and converted into simplest form on assignment.
        /// </summary>
        /// <param name="hours">The total number of hours in the <c>Duration</c>.</param>
        /// <param name="minutes">The total number of minutes in the <c>Duration</c>.</param>
        /// <param name="seconds">The total number of seconds in the <c>Duration</c>.</param>
        public Duration(int hours, int minutes, int seconds = 0) : this(ToMilliseconds(hours,  minutes, seconds)) {}
        
        /// <summary>
        /// Creates a new <c>Duration</c> struct.
        /// </summary>
        /// <param name="milliseconds">The total timespan in milliseconds.</param>
        public Duration(float milliseconds) : this((long)milliseconds) {}

        /// <summary>
        /// Creates a new <c>Duration</c> struct.
        /// </summary>
        /// <param name="milliseconds">The total timespan in milliseconds.</param>
        public Duration(int milliseconds) : this((long)milliseconds) {}

        /// <summary>
        /// Creates a new <c>Duration</c> struct.
        /// </summary>
        /// <param name="milliseconds">The total timespan in milliseconds.</param>
        public Duration(long milliseconds) => BackingTime = milliseconds;

        /// <summary>
        /// Determines if this <c>Duration</c> is between a lower and upper <c>Duration</c>, inclusive.
        /// Note that the comparison is performed on the literal value and not the absolute value. Thus,
        /// a <c>Duration</c> of -2 hours is between -3 and -1 hours. However, it is not between 1 and 3
        /// hours, even though the absolute value is 2 hours.
        /// </summary>
        /// <param name="lower">The lower bound, inclusive.</param>
        /// <param name="upper">The upper bound, inclusive.</param>
        /// <returns>
        /// <c>true</c> if this <c>Duration</c> is greater than or equal to the lower bound and less
        /// than or equal to the upper bound. Returns <c>false</c> otherwise.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Throws when the lower bound is strictly greater than the upper bound.
        /// </exception>
        public bool IsBetween(Duration lower, Duration upper)
        {
            if (lower > upper)
            {
                throw new ArgumentOutOfRangeException(nameof(lower), "Lower bound should be less than or equal to upper bound.");
            }
            return this >= lower && this <=  upper;
        }

        /// <summary>
        /// Calculates the total milliseconds elapsed excluding the <c>Day</c> component. In practice, this returns
        /// the equivalent number of milliseconds elapsed to a <c>TimeOnly</c> struct whose <c>Hour</c>, <c>Minute</c>,
        /// <c>Second</c>, and <c>Millisecond</c> components are equal to this <c>Duration</c>.
        /// </summary>
        /// <returns>
        /// The total milliseconds elapsed according to the <c>Hour</c>, <c>Minute</c>,
        /// <c>Second</c>, and <c>Millisecond</c> components.
        /// </returns>
        public long FlattenDays()
            => ToMilliseconds(Hour, Minute, Second, Millisecond);

        /// <summary>
        /// Converts a <c>Duration</c> into its absolute value. If the timespan is zero or positive, then
        /// <c>myDuration</c> is equal to <c>myDuration.Abs()</c>. If the timespan is negative, then
        /// <c>myDuration</c> is equal to <c>-1 * myDuration.Abs()</c>.
        /// </summary>
        /// <returns>The absolute <c>Duration</c>.</returns>
        public Duration Abs()
            => new(Math.Abs(BackingTime));
        
    }
    
}
