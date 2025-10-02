using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameTime
{
    public partial struct TimeOnly
    {
        
        private const string DEFAULT_FORMAT = "hh:mm:ss:fff tt";
        
        private const string FULL_HOURS_24 = "HH";
        private const string FULL_HOURS_12 = "hh";
        private const string HOURS_24 = "H";
        private const string HOURS_12 = "h";
        private const string FULL_MINUTES = "mm";
        private const string MINUTES = "m";
        private const string FULL_SECONDS = "ss";
        private const string SECONDS = "s";
        private const string MILLISECONDS = "fff";
        private const string SUFFIX = "tt";

        public override string ToString() => ToString(DEFAULT_FORMAT);

        public string ToString(string format) => ToString(format, null);

        public string ToString(string format, IFormatProvider provider)
        {

            format ??= DEFAULT_FORMAT;
            
            format = format.Replace(FULL_HOURS_24, $"{Hour:00}");
            format = format.Replace(HOURS_24, $"{Hour:0}");
            format = format.Replace(FULL_HOURS_12, $"{TimeConversions.Convert24HourTo12Hour(Hour):00}");
            format = format.Replace(HOURS_12, $"{TimeConversions.Convert24HourTo12Hour(Hour):0}");
            format = format.Replace(FULL_MINUTES, $"{Minute:00}");
            format = format.Replace(MINUTES, $"{Minute:0}");
            format = format.Replace(FULL_SECONDS, $"{Second:00}");
            format = format.Replace(SECONDS, $"{Second:0}");
            format = format.Replace(MILLISECONDS, $"{Millisecond:000}");
            format = format.Replace(SUFFIX, GetTimeSuffix());
            
            return format;
            
        }
        
    }
}