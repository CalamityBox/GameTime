using System;
using UnityEngine;

namespace GameTime.Clock
{
    public abstract partial class BaseClock
    {
        
        public override string ToString() => Time.ToString(Format);

        public string ToString(string format) => ToString(format, null);

        public string ToString(string format, IFormatProvider provider)
        {
            format ??= Format;
            return Time.ToString(format, provider);
        }

    }
}