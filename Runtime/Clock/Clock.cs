using System;
using UnityEngine;

namespace GameTime.Clock
{
    public abstract partial class BaseClock : IFormattable
    {
        
        public class OnTimeChangedEventArgs : EventArgs
        {
            
            /// <summary>
            /// The <c>TimeOnly</c> value of the clock when the event was invoked.
            /// </summary>
            public TimeOnly Time;
            
            /// <summary>
            /// The previous <c>TimeOnly</c> value of the clock before the event was invoked.
            /// </summary>
            public TimeOnly PreviousTime;
            
            /// <summary>
            /// The formatted <c>TimeOnly</c> string according to the default time format of the
            /// clock that invoked the event.
            /// </summary>
            public string TimeString;
            
            /// <summary>
            /// The formatted <c>TimeOnly</c> string of the previous time of clock according to
            /// the default time format of the clock that invoked the event
            /// </summary>
            public string PreviousTimeString;
            
            /// <summary>
            /// The <c>Duration</c> representing the timespan between the <c>Time</c> and
            /// <c>PreviousTime</c> values of this event. Note that if time is moving forward,
            /// then <c>DeltaTime</c> will be a positive timespan. If time is moving backwards,
            /// <c>DeltaTime</c> will be a negative timespan.
            /// </summary>
            public Duration DeltaTime;
            
            /// <summary>
            /// The proportion of the day elapsed relative to the start of the day, midnight. Note that
            /// because midnight is both the start and end time of the day, the <c>DayProgress</c> is
            /// bound in the range <c>[0,1)</c>.
            /// </summary>
            public float DayProgress;

            /// <summary>
            /// Creates a new instance of <c>OnTimeChangedEventArgs</c>.
            /// </summary>
            /// <param name="time">
            /// The <c>TimeOnly</c> value of the clock when the event was invoked.
            /// </param>
            /// <param name="previousTime">
            /// The previous <c>TimeOnly</c> value of the clock before the event was invoked.
            /// </param>
            /// <param name="format">
            /// The format to apply to the <c>TimeOnly</c> values. By default, the format of
            /// the <c>Clock</c> that is invoking this event should be passed.
            /// </param>
            public OnTimeChangedEventArgs(TimeOnly time, TimeOnly previousTime, string format)
            {
                Time = time;
                PreviousTime = previousTime;
                TimeString = time.ToString();
                PreviousTimeString = previousTime.ToString(format);
                DeltaTime = time - previousTime;
                DayProgress = time.GetDayProgress();
            }
            
        }
        
        /// <summary>
        /// The <c>OnTimeChanged</c> event invokes whenever the <c>Time</c> recorded by the <c>Clock</c>
        /// changes, down to the <c>Clock</c> object's level of specificity. Every <c>Clock</c> object
        /// will track the full <c>TimeOnly</c> struct's level of precision behind the scenes, but a
        /// <c>MinuteClock</c>, for example, will only invoke <c>OnTimeChanged</c> when the <c>Hour</c>
        /// or <c>Minute</c> components of the <c>TimeOnly</c> struct have changed.
        /// </summary>
        public event EventHandler<OnTimeChangedEventArgs> OnTimeChanged;
        
        /// <summary>
        /// The current time of the <c>Clock</c>.
        /// </summary>
        public TimeOnly Time
        {
            get => _time;
            protected set
            {
                HandleSetterEvents(value, out bool isTimeChanged);
                if (isTimeChanged) OnTimeChanged?.Invoke(this, new OnTimeChangedEventArgs(value, _time, Format));
                _time = value;
            }
        }

        /// <summary>
        /// The <c>Format</c> property controls the <c>Clock</c> object's default time
        /// formatting behavior when the <c>ToString</c> function is called without a
        /// format parameter, or when the format is <c>null</c> in the case
        /// of a template string, such as <c>$"{clock}"</c>. Any <c>Clock</c> object
        /// can be set with to use any valid time format.
        /// </summary>
        public string Format { get; set; }
        
        protected TimeOnly _time;
        
        /// <summary>
        /// The <c>AdvanceTime</c> method advances the <c>Time</c> property linearly by a factor
        /// of <c>UnityEngine.Time.deltaTime</c>.
        /// </summary>
        /// <param name="scalar">
        /// An optional parameter to adjust the scale at which time progresses. The default value of
        /// <c>1</c> means that time will advance at a 1:1 scale with the real world. A value of
        /// <c>2</c> would double the rate of progression meaning that one real-world hour would be
        /// equal to two in-game hours.
        /// </param>
        public void AdvanceTime(float scalar = 1)
            => Time += scalar * new Duration(TimeConversions.SecondsToMilliseconds(UnityEngine.Time.deltaTime));

        /// <summary>
        /// Determines if the <c>Time</c> property of the clock is equal to another time at the
        /// level of precision specified by the inheriting class. Note that the comparison is made
        /// using the 24-hour time format. Thus, 8 AM is distinct from 8 PM, even though the
        /// hour components in 12-hour time are equivalent. This is only relevant if the inheriting
        /// class is making comparisons at the hour-component level.
        /// </summary>
        /// <param name="time">The time to compare against the clock's <c>Time</c></param>
        /// <returns>
        /// <c>true</c> if the <c>Time</c> stored by the clock is equal to the input time, at the level
        /// of precision specified by the inheriting class. Returns <c>false</c> otherwise.
        /// </returns>
        public abstract bool IsEqualTo(TimeOnly time);
        
        /// <summary>
        /// Method to invoke the <c>BaseClock</c> class' <c>OnTimeChanged</c> event. The event should
        /// invoke only once when any of the <c>TimeOnly</c> components changes according to the
        /// specification in the inheriting class.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InvokeOnTimeChanged(object sender, OnTimeChangedEventArgs e)
            => OnTimeChanged?.Invoke(sender, e);

        /// <summary>
        /// The <c>HandleSetterEvents</c> method is intended to evaluate the incoming time in the
        /// <c>set</c> method of <c>BaseClock</c> for changes and invoke component-level time changed
        /// events such as <c>OnMinuteChanged</c>. Finally, it returns an out parameter <c>isTimeChanged</c>
        /// that is used by the <c>set</c> method to invoke the base class' <c>OnTimeChanged</c> event.
        /// </summary>
        /// <param name="value">The new <c>TimeOnly</c> value of the clock.</param>
        /// <param name="isTimeChanged">
        /// Out parameter that is <c>true</c> when the new time <c>value</c> is different from the
        /// <c>Time</c> currently stored by the <c>Clock</c> at the specified level of precision by the
        /// inheriting class. Returns <c>false</c> when the times are equal at the specified level of
        /// precision.
        /// </param>
        protected virtual void HandleSetterEvents(TimeOnly value, out bool isTimeChanged)
            => isTimeChanged = false;

    }
    
    /// <summary>
    /// An <c>HourClock</c> is a clock whose default time format is set to a precision level
    /// of the <c>Hour</c> component. By default, an <c>HourClock</c> will format
    /// time as "8 PM". The <c>OnTimeChanged</c> event of an <c>HourClock</c> will only invoke
    /// when the <c>Hour</c> component of <c>Time</c> has changed.
    /// </summary>
    public class HourClock : BaseClock
    {
        
        /// <summary>
        /// The <c>OnHourChanged</c> event invokes if and only if the <c>Hour</c> component of
        /// <c>Time</c> has changed.
        /// </summary>
        public event EventHandler<OnTimeChangedEventArgs> OnHourChanged;

        /// <summary>
        /// Creates a new instance of an <c>HourClock</c>.
        /// </summary>
        /// <param name="time">The initial time of the clock.</param>
        /// <param name="format">The default format of the clock.</param>
        public HourClock(TimeOnly time, string format = "h tt")
        {
            _time = time;
            Format = format;
        }

        /// <summary>
        /// Determines if the <c>Time</c> property of the clock is equal to another time at the
        /// level of precision of the <c>Hour</c> component. Note that the comparison is made
        /// using the 24-hour time format. Thus, 8 AM is distinct from 8 PM, even though the
        /// hour components in 12-hour time are equivalent.
        /// </summary>
        /// <param name="time">The time to compare against the clock's <c>Time</c></param>
        /// <returns>
        /// <c>true</c> if the <c>Hour</c> of the clock is equal to the <c>Hour</c> of the input.
        /// Returns <c>false</c> otherwise.
        /// </returns>
        public override bool IsEqualTo(TimeOnly time)
            => Time.Hour == time.Hour;
        
        protected override void HandleSetterEvents(TimeOnly value, out bool isTimeChanged)
        {
            
            isTimeChanged = false;
            
            if (!_time.IsHourEqual(value))
            {
                OnHourChanged?.Invoke(this, new OnTimeChangedEventArgs(value, _time, Format));
                isTimeChanged = true;
            }
            
        }

        protected void InvokeOnHourChanged(object sender, OnTimeChangedEventArgs e)
            => OnHourChanged?.Invoke(sender, e);

    }

    /// <summary>
    /// A <c>MinuteClock</c> is a clock whose default time format is set to a precision level
    /// of the <c>Hour</c> and <c>Minute</c> components. By default, a <c>MinuteClock</c> will format
    /// time as "8:00 PM". The <c>OnTimeChanged</c> event of a <c>MinuteClock</c> will only invoke
    /// when the <c>Hour</c> or <c>Minute</c> components of <c>Time</c> have changed.
    /// </summary>
    public class MinuteClock : HourClock
    {
        
        /// <summary>
        /// The <c>OnMinuteChanged</c> event invokes if and only if the <c>Minute</c> component of
        /// <c>Time</c> has changed.
        /// </summary>
        public event EventHandler<OnTimeChangedEventArgs> OnMinuteChanged;

        /// <summary>
        /// Creates a new instance of a <c>MinuteClock</c>.
        /// </summary>
        /// <param name="time">The initial time of the clock.</param>
        /// <param name="format">The default format of the clock.</param>
        public MinuteClock(TimeOnly time, string format = "h:mm tt") : base(time, format) {}
        
        /// <summary>
        /// Determines if the <c>Time</c> property of the clock is equal to another time at the
        /// level of precision of the <c>Hour</c> and <c>Minute</c> components. Note that the
        /// comparison is made using the 24-hour time format. Thus, 8:00 AM is distinct from 8:00 PM,
        /// even though the hour components in 12-hour time are equivalent.
        /// </summary>
        /// <param name="time">The time to compare against the clock's <c>Time</c></param>
        /// <returns>
        /// <c>true</c> if the <c>Hour</c> and <c>Minute</c> of the clock are equal to the <c>Hour</c>
        /// and <c>Minute</c> of the input. Returns <c>false</c> otherwise.
        /// </returns>
        public override bool IsEqualTo(TimeOnly time)
            => Time.Hour == time.Hour &
               Time.Minute == time.Minute;
        
        protected override void HandleSetterEvents(TimeOnly value, out bool isTimeChanged)
        {
            
            isTimeChanged = false;
            
            OnTimeChangedEventArgs eventArgs = new(value, _time, Format);
            
            if (!_time.IsHourEqual(value))
            {
                InvokeOnHourChanged(this, eventArgs);
                isTimeChanged = true;
            }
            
            if (!_time.IsMinuteEqual(value))
            {
                OnMinuteChanged?.Invoke(this, eventArgs);
                isTimeChanged = true;
            }
            
        }

        protected void InvokeOnMinuteChanged(object sender, OnTimeChangedEventArgs e)
            => OnMinuteChanged?.Invoke(this, e);
        
    }

    /// <summary>
    /// A <c>SecondClock</c> is a clock whose default time format is set to a precision level
    /// of the <c>Hour</c>, <c>Minute</c>, and <c>Second</c> components. By default, a
    /// <c>SecondClock</c> will format time as "8:00:00 PM". The <c>OnTimeChanged</c> event of a
    /// <c>SecondClock</c> will only invoke when the <c>Hour</c>, <c>Minute</c>, or <c>Second</c>
    /// components of <c>Time</c> have changed.
    /// </summary>
    public class SecondClock : MinuteClock
    {
        
        /// <summary>
        /// The <c>OnSecondChanged</c> event invokes if and only if the <c>Second</c> component of
        /// <c>Time</c> has changed.
        /// </summary>
        public event EventHandler<OnTimeChangedEventArgs> OnSecondChanged;

        /// <summary>
        /// Creates a new instance of a <c>SecondClock</c>.
        /// </summary>
        /// <param name="time">The initial time of the clock.</param>
        /// <param name="format">The default format of the clock.</param>
        public SecondClock(TimeOnly time, string format = "h:mm:ss tt") : base(time, format) {}
        
        /// <summary>
        /// Determines if the <c>Time</c> property of the clock is equal to another time at the
        /// level of precision of the <c>Hour</c>, <c>Minute</c>, and <c>Second</c> components.
        /// Note that the comparison is made using the 24-hour time format. Thus, 8:00:00 AM is
        /// distinct from 8:00:00 PM, even though the hour components in 12-hour time are equivalent.
        /// </summary>
        /// <param name="time">The time to compare against the clock's <c>Time</c></param>
        /// <returns>
        /// <c>true</c> if the <c>Hour</c>, <c>Minute</c>, and <c>Second</c> components of the clock
        /// are equal to the <c>Hour</c>, <c>Minute</c>, and <c>Second</c> of the input. Returns
        /// <c>false</c> otherwise.
        /// </returns>
        public override bool IsEqualTo(TimeOnly time)
            => Time.Hour == time.Hour &
               Time.Minute == time.Minute &
               Time.Second == time.Second;
        
        protected override void HandleSetterEvents(TimeOnly value, out bool isTimeChanged)
        {
            isTimeChanged = false;
            
            OnTimeChangedEventArgs eventArgs = new(value, _time, Format);

            if (!_time.IsHourEqual(value))
            {
                InvokeOnHourChanged(this, eventArgs);
                isTimeChanged = true;
            }

            if (!_time.IsMinuteEqual(value))
            {
                InvokeOnMinuteChanged(this, eventArgs);
                isTimeChanged = true;
            }

            if (!_time.IsSecondEqual(value))
            {
                OnSecondChanged?.Invoke(this, eventArgs);
                isTimeChanged = true;
            }
            
        }
        
        protected void InvokeOnSecondChanged(object sender, OnTimeChangedEventArgs e)
            => OnSecondChanged?.Invoke(this, e);
        
    }

    /// <summary>
    /// A <c>MillisecondClock</c> is a clock whose default time format is set to a precision level
    /// of the <c>Hour</c>, <c>Minute</c>, <c>Second</c>, and <c>Millisecond</c> components. By
    /// default, a <c>MillisecondClock</c> will format time as "8:00:00:000 PM". The
    /// <c>OnTimeChanged</c> event of a <c>MillisecondClock</c> will only invoke when the <c>Hour</c>,
    /// <c>Minute</c>, <c>Second</c>, or <c>Millisecond</c> components of <c>Time</c> have changed.
    /// </summary>
    public class MillisecondClock : SecondClock
    {

        /// <summary>
        /// The <c>OnMillisecondChanged</c> event invokes if and only if the <c>Millisecond</c>
        /// component of <c>Time</c> has changed.
        /// </summary>
        public event EventHandler<OnTimeChangedEventArgs> OnMillisecondChanged;

        /// <summary>
        /// Creates a new instance of a <c>MillisecondClock</c>.
        /// </summary>
        /// <param name="time">The initial time of the clock.</param>
        /// <param name="format">The default format of the clock.</param>
        public MillisecondClock(TimeOnly time, string format = "h:mm:ss:fff tt") : base(time, format) {}
        
        /// <summary>
        /// Determines if the <c>Time</c> property of the clock is equal to another time at the
        /// level of precision of the <c>Hour</c>, <c>Minute</c>, <c>Second</c>, and
        /// <c>Millisecond</c> components. Note that the comparison is made using the 24-hour time format.
        /// Thus, 8:00:00:000 AM is distinct from 8:00:00:000 PM, even though the hour components in 12-hour
        /// time are equivalent. This level of precision matches the precision of the <c>TimeOnly</c>
        /// struct itself. Therefore in a <c>MillisecondClock</c>, this method is equivalent to an
        /// equality check on the <c>Time</c> component itself:
        /// <c>MillisecondClock.Time == time</c>.
        /// </summary>
        /// <param name="time">The time to compare against the clock's <c>Time</c></param>
        /// <returns>
        /// <c>true</c> if the <c>Hour</c>, <c>Minute</c>, <c>Second</c>, and <c>Millisecond</c>
        /// components of the clock are equal to the <c>Hour</c>, <c>Minute</c>, <c>Second</c>,
        /// and <c>Millisecond</c> of the input. Returns <c>false</c> otherwise.
        /// </returns>
        public override bool IsEqualTo(TimeOnly time) => Time == time;
        
        protected override void HandleSetterEvents(TimeOnly value, out  bool isTimeChanged)
        {
            isTimeChanged = false;
            
            OnTimeChangedEventArgs eventArgs = new(value, _time, Format);

            if (!_time.IsHourEqual(value))
            {
                InvokeOnHourChanged(this, eventArgs);
                isTimeChanged = true;
            }

            if (!_time.IsMinuteEqual(value))
            {
                InvokeOnMinuteChanged(this, eventArgs);
                isTimeChanged = true;
            }

            if (!_time.IsSecondEqual(value))
            {
                InvokeOnSecondChanged(this, eventArgs);
                isTimeChanged = true;
            }

            if (!_time.IsMillisecondEqual(value))
            {
                OnMillisecondChanged?.Invoke(this, eventArgs);
                isTimeChanged = true;
            }
            
        }

    }
    
}