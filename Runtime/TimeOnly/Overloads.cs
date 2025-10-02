using System;
using JetBrains.Annotations;
using static GameTime.Duration;

namespace GameTime
{
    public partial struct TimeOnly
    {

        public override bool Equals([CanBeNull] object obj)
        {
            if (obj is TimeOnly other)
            {
                return Equals(other);
            }
            throw new ArgumentException("Object is not a TimeOnly");
        }

        public bool Equals(TimeOnly timeOnly)
            => timeOnly.BackingTime == BackingTime;
        
        public override int GetHashCode() 
            => BackingTime.GetHashCode();
        
        public static bool operator ==(TimeOnly left, TimeOnly right) 
            => left.Equals(right);
        
        public static bool operator !=(TimeOnly left, TimeOnly right) 
            => !(left == right);
        
        public int CompareTo([CanBeNull] object obj)
        {
            if (obj is TimeOnly other)
            {
                return CompareTo(other);
            }
            throw new ArgumentException("Object is not a TimeOnly.");
        }

        public int CompareTo(TimeOnly other)
        {
            return BackingTime.CompareTo(other.BackingTime);
        }
        
        public static bool operator <(TimeOnly left, TimeOnly right) 
            => left.BackingTime < right.BackingTime;
        
        public static bool operator >(TimeOnly left, TimeOnly right) 
            => !(left < right) && left != right;
        
        public static bool operator <=(TimeOnly left, TimeOnly right) 
            => !(left > right);
        
        public static bool operator >=(TimeOnly left, TimeOnly right) 
            => !(left < right);

        public static TimeOnly operator +(TimeOnly timeOnly, Duration duration)
            => new(timeOnly.BackingTime + duration.BackingTime);

        public static TimeOnly operator +(Duration duration, TimeOnly timeOnly)
            => timeOnly + duration;

        // Always assume that the leftmost time is in the future compared to the rightmost time.
        // For example, the difference between 0:00 and 23:00 should be one hour because midnight
        // is one hour from 23:00. However, a simple subtraction would yield -23 hours. Thus, add
        // one day to the output when it is below zero to get the correct answer.
        public static Duration operator -(TimeOnly left, TimeOnly right)
        {
            Duration output = new(left.BackingTime - right.BackingTime);
            if (output < Zero) output += OneDay;
            return output;
        }

        public static TimeOnly operator -(TimeOnly timeOnly, Duration duration)
            => new(timeOnly.BackingTime - duration.BackingTime);

    }
}