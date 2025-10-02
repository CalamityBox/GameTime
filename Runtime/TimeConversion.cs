using System;
using UnityEngine;

namespace GameTime
{
    public static class TimeConversions
    {

        public const int NUM_MILLISECONDS_PER_SECOND = 1_000;
        public const int NUM_MILLISECONDS_PER_MINUTE = NUM_SECONDS_PER_MINUTE * NUM_MILLISECONDS_PER_SECOND;
        public const int NUM_MILLISECONDS_PER_HOUR = NUM_SECONDS_PER_HOUR * NUM_MILLISECONDS_PER_SECOND;
        public const int NUM_MILLISECONDS_PER_DAY = NUM_SECONDS_PER_DAY * NUM_MILLISECONDS_PER_SECOND;
        public const int NUM_SECONDS_PER_MINUTE = 60;
        public const int NUM_SECONDS_PER_HOUR = NUM_SECONDS_PER_MINUTE * NUM_MINUTES_PER_HOUR;
        public const int NUM_SECONDS_PER_DAY = NUM_HOURS_PER_DAY * NUM_SECONDS_PER_HOUR;
        public const int NUM_MINUTES_PER_HOUR = 60;
        public const int NUM_MINUTES_PER_DAY = NUM_HOURS_PER_DAY * NUM_MINUTES_PER_HOUR;
        public const int NUM_HOURS_PER_DAY = 24;

        public static int MillisecondsToSeconds(long milliseconds, out long remainder)
        {
            return (int)Math.DivRem(milliseconds, NUM_MILLISECONDS_PER_SECOND, out remainder);
        }

        public static int MillisecondsToSeconds(long milliseconds)
        {
            return MillisecondsToSeconds(milliseconds, out _);
        }

        public static int MillisecondsToMinutes(long milliseconds, out long remainder)
        {
            return (int)Math.DivRem(milliseconds, NUM_MILLISECONDS_PER_MINUTE, out remainder);
        }

        public static int MillisecondsToMinutes(long milliseconds)
        {
            return MillisecondsToMinutes(milliseconds, out _);
        }

        public static int MillisecondsToHours(long milliseconds, out long remainder)
        {
            return (int)Math.DivRem(milliseconds, NUM_MILLISECONDS_PER_HOUR, out remainder);
        }

        public static int MillisecondsToHours(long milliseconds)
        {
            return MillisecondsToHours(milliseconds, out _);
        }

        public static int MillisecondsToDays(long milliseconds, out long remainder)
        {
            return (int)Math.DivRem(milliseconds, NUM_MILLISECONDS_PER_DAY, out remainder);
        }

        public static int MillisecondsToDays(long milliseconds)
        {
            return MillisecondsToDays(milliseconds, out _);
        }

        public static long ToMilliseconds(int days, int hours, int minutes, int seconds, int milliseconds)
        {
            return DaysToMilliseconds(days) + 
                   HoursToMilliseconds(hours) + 
                   MinutesToMilliseconds(minutes) + 
                   SecondsToMilliseconds(seconds) +
                   milliseconds;
        }

        public static long ToMilliseconds(int hours, int minutes, int seconds, int milliseconds)
        {
            return ToMilliseconds(0, hours, minutes, seconds, milliseconds);
        }

        public static long ToMilliseconds(int hours, int minutes, int seconds = 0)
        {
            return ToMilliseconds(hours, minutes, seconds, 0);
        }

        public static long DaysToMilliseconds(int days)
        {
            return days * NUM_MILLISECONDS_PER_DAY;
        }

        public static long HoursToMilliseconds(int hours)
        {
            return hours * NUM_MILLISECONDS_PER_HOUR;
        }

        public static long MinutesToMilliseconds(int minutes)
        {
            return minutes * NUM_MILLISECONDS_PER_MINUTE;
        }

        public static long SecondsToMilliseconds(int seconds)
        {
            return seconds * NUM_MILLISECONDS_PER_SECOND;
        }

        public static long SecondsToMilliseconds(float seconds)
        {
            return (long)(seconds * NUM_MILLISECONDS_PER_SECOND);
        }

        public static int Convert24HourTo12Hour(int hour)
        {
            if (hour < 0 || hour > 23)
            {
                throw new ArgumentOutOfRangeException(nameof(hour), hour, "hour must be between 0 and 23");
            }
            if (hour == 0)
            {
                return 12;
            }
            if (hour > 12)
            {
                return hour - 12;
            }
            return hour;
        }

    }
}