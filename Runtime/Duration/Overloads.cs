using System;
using JetBrains.Annotations;
using UnityEngine;

namespace GameTime
{
    public partial struct Duration
    {

        public override bool Equals([CanBeNull] object obj)
        {
            if (obj is Duration other)
            {
                return Equals(other);
            }
            throw new ArgumentException("Object is not a Duration.");
        }
        
        public bool Equals(Duration duration)
            => duration.BackingTime == BackingTime;
        
        public override int GetHashCode() 
            => BackingTime.GetHashCode();
        
        public static bool operator ==(Duration left, Duration right)
            => left.Equals(right);
        
        public static bool operator !=(Duration left, Duration right)
            => !left.Equals(right);

        public int CompareTo([CanBeNull] object obj)
        {
            if (obj is Duration other)
            {
                return CompareTo(other);
            }
            throw new ArgumentException("Object is not a Duration.");
        }

        public int CompareTo(Duration other)
        {
            return BackingTime.CompareTo(other.BackingTime);
        }
        
        public static bool operator <(Duration left, Duration right)
            => left.BackingTime < right.BackingTime;
        
        public static bool operator <=(Duration left, Duration right)
            => left < right || left == right;

        public static bool operator >(Duration left, Duration right)
            => !(left <= right);

        public static bool operator >=(Duration left, Duration right)
            => !(left < right);
        
        public static Duration operator +(Duration left, Duration right)
            => new(left.BackingTime + right.BackingTime);

        public static Duration operator +(Duration duration, int milliseconds)
            => new(duration.BackingTime + milliseconds);

        public static Duration operator +(int milliseconds, Duration duration)
            => duration + milliseconds;
        
        public static Duration operator -(Duration left, Duration right)
            => new(left.BackingTime - right.BackingTime);
        
        public static Duration operator -(Duration duration, int milliseconds)
            => new(duration.BackingTime - milliseconds);
        
        public static Duration operator *(Duration duration, float scalar)
            => new(duration.BackingTime * scalar);
        
        public static Duration operator *(Duration duration, int scalar)
            => new(duration.BackingTime * scalar);
        
        public static Duration operator *(float scalar,  Duration duration)
            => duration * scalar;
        
        public static Duration operator *(int scalar,  Duration duration)
            => duration * scalar;
        
        public static Duration operator /(Duration left,  Duration right)
            => new(left.BackingTime / right.BackingTime);
        
        public static Duration operator /(Duration duration, float scalar)
            => new(Mathf.RoundToInt(duration.BackingTime / scalar));
        
        public static Duration operator /(Duration duration, int scalar)
            => new(duration.BackingTime / scalar);

    }
}