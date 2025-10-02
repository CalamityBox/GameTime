using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using GameTime;
using static GameTime.Duration;

namespace Tests.Runtime
{
    public class DurationTests
    {

        [Test]
        public void TestConstants()
        {
            Assert.That(Zero.BackingTime, Is.EqualTo(0));
            Assert.That(Zero.Millisecond, Is.EqualTo(0));
            Assert.That(Zero.Second, Is.EqualTo(0));
            Assert.That(Zero.Minute, Is.EqualTo(0));
            Assert.That(Zero.Hour, Is.EqualTo(0));
            Assert.That(Zero.Day, Is.EqualTo(0));
            
            Assert.That(OneMillisecond.BackingTime, Is.EqualTo(1));
            Assert.That(OneMillisecond.Millisecond, Is.EqualTo(1));
            Assert.That(OneMillisecond.Second, Is.EqualTo(0));
            Assert.That(OneMillisecond.Minute, Is.EqualTo(0));
            Assert.That(OneMillisecond.Hour, Is.EqualTo(0));
            Assert.That(OneMillisecond.Day, Is.EqualTo(0));
            
            Assert.That(OneSecond.BackingTime, Is.EqualTo(1_000));
            Assert.That(OneSecond.Millisecond, Is.EqualTo(0));
            Assert.That(OneSecond.Second, Is.EqualTo(1));
            Assert.That(OneSecond.Minute, Is.EqualTo(0));
            Assert.That(OneSecond.Hour, Is.EqualTo(0));
            Assert.That(OneSecond.Day, Is.EqualTo(0));
            
            Assert.That(OneMinute.BackingTime, Is.EqualTo(60_000));
            Assert.That(OneMinute.Millisecond, Is.EqualTo(0));
            Assert.That(OneMinute.Second, Is.EqualTo(0));
            Assert.That(OneMinute.Minute, Is.EqualTo(1));
            Assert.That(OneMinute.Hour, Is.EqualTo(0));
            Assert.That(OneMinute.Day, Is.EqualTo(0));
            
            Assert.That(OneHour.BackingTime, Is.EqualTo(3_600_000));
            Assert.That(OneHour.Millisecond, Is.EqualTo(0));
            Assert.That(OneHour.Second, Is.EqualTo(0));
            Assert.That(OneHour.Minute, Is.EqualTo(0));
            Assert.That(OneHour.Hour, Is.EqualTo(1));
            Assert.That(OneHour.Day, Is.EqualTo(0));
            
            Assert.That(OneDay.BackingTime, Is.EqualTo(86_400_000));
            Assert.That(OneDay.Millisecond, Is.EqualTo(0));
            Assert.That(OneDay.Second, Is.EqualTo(0));
            Assert.That(OneDay.Minute, Is.EqualTo(0));
            Assert.That(OneDay.Hour, Is.EqualTo(0));
            Assert.That(OneDay.Day, Is.EqualTo(1));
        }

        [Test]
        public void TestConstantsComposition()
        {
            Duration duration = OneDay + OneHour + OneMinute + 
                                OneSecond + OneMillisecond + Zero;
            Assert.That(duration.BackingTime, Is.EqualTo(90_061_001));
        }

        [Test]
        public void TestEquality()
        {
            Assert.Throws<ArgumentException>(() => Zero.Equals(null));
            
            Assert.That(new Duration(0) == new Duration(0), Is.True);
            Assert.That(new Duration(0) == new Duration(1), Is.False);
            Assert.That(new Duration(0) == new Duration(-1), Is.False);
            Assert.That(new Duration(1_234_567) == new Duration(1_234_567), Is.True);
            Assert.That(new Duration(86_399_999) == new Duration(86_400_000), Is.False);
            
            Assert.That(new Duration(0) == Zero, Is.True);
            Assert.That(new Duration(1) == OneMillisecond, Is.True);
            Assert.That(new Duration(1_000) == OneSecond, Is.True);
            Assert.That(new Duration(60_000) == OneMinute, Is.True);
            Assert.That(new Duration(3_600_000) == OneHour, Is.True);
            Assert.That(new Duration(86_400_000) == OneDay, Is.True);
            
            Assert.That(new Duration(-1) == -1 * OneMillisecond, Is.True);
            Assert.That(new Duration(-1_000) == -1 * OneSecond, Is.True);
            Assert.That(new Duration(-60_000) == -1 * OneMinute, Is.True);
            Assert.That(new Duration(-3_600_000) == -1 * OneHour, Is.True);
            Assert.That(new Duration(-86_400_000) == -1 * OneDay, Is.True);
        }

        [Test]
        public void TestComparison()
        {
            Assert.That(Zero < Zero, Is.False);
            Assert.That(Zero <= Zero, Is.True);
            Assert.That(Zero > Zero, Is.False);
            Assert.That(Zero >= Zero, Is.True);
            
            Assert.That(Zero < OneMillisecond, Is.True);
            Assert.That(Zero <= OneMillisecond, Is.True);
            Assert.That(Zero > OneMillisecond, Is.False);
            Assert.That(Zero >= OneMillisecond, Is.False);
            
            Assert.That(Zero < -1 * OneMillisecond, Is.False);
            Assert.That(Zero <= -1 * OneMillisecond, Is.False);
            Assert.That(Zero > -1 * OneMillisecond, Is.True);
            Assert.That(Zero >= -1 * OneMillisecond, Is.True);
            
            Assert.That(OneHour < OneHour, Is.False);
            Assert.That(OneHour <= OneHour, Is.True);
            Assert.That(OneHour > OneHour, Is.False);
            Assert.That(OneHour >= OneHour, Is.True);
            
            Assert.That(-1 * OneHour < -1 * OneHour, Is.False);
            Assert.That(-1 * OneHour <= -1 * OneHour, Is.True);
            Assert.That(-1 * OneHour > -1 * OneHour, Is.False);
            Assert.That(-1 * OneHour >= -1 * OneHour, Is.True);
            
            Assert.That(OneHour < OneDay, Is.True);
            Assert.That(OneHour <= OneDay, Is.True);
            Assert.That(OneHour > OneDay, Is.False);
            Assert.That(OneHour >= OneDay, Is.False);
            
            Assert.That(-1 * OneHour < -1 * OneDay, Is.False);
            Assert.That(-1 * OneHour <= -1 * OneDay, Is.False);
            Assert.That(-1 * OneHour > -1 * OneDay, Is.True);
            Assert.That(-1 * OneHour >= -1 * OneDay, Is.True);
        }

        [Test]
        public void TestListSorting()
        {
            List<Duration> unsorted = new List<Duration>
            {
                OneMinute,
                -1 * OneDay,
                Zero,
                OneDay,
                -1 * OneMillisecond,
                -1 * OneMinute,
                OneHour,
                OneMillisecond,
                -1 * OneSecond,
                OneSecond,
                -1 * OneHour,
            };
            
            List<Duration> sorted = new List<Duration>
            {
                -1 * OneDay,
                -1 * OneHour,
                -1 * OneMinute,
                -1 * OneSecond,
                -1 * OneMillisecond,
                Zero,
                OneMillisecond,
                OneSecond,
                OneMinute,
                OneHour,
                OneDay
            };
            
            unsorted.Sort();

            Assert.That(sorted.SequenceEqual(unsorted), Is.True);
        }

        [Test]
        public void TestIsBetween()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Zero.IsBetween(OneDay, OneHour));
            
            Assert.That(OneHour.IsBetween(OneHour, OneHour), Is.True);
            Assert.That(OneDay.IsBetween(OneDay, OneDay + OneHour), Is.True);
            Assert.That(new Duration(1).IsBetween(Zero, new Duration(2)), Is.True);
            Assert.That(new Duration(-10).IsBetween(new Duration(-15), new Duration(-5)), Is.True);
            
            Assert.That(OneHour.IsBetween(OneDay, 2 * OneDay), Is.False);
            Assert.That(new Duration(-1_000).IsBetween(Zero, OneHour), Is.False);
        }

        [Test]
        public void TestTimeComponents()
        {
            Duration duration = new Duration(0);
            
            Assert.That(duration.Day, Is.EqualTo(0));
            Assert.That(duration.Hour, Is.EqualTo(0));
            Assert.That(duration.Minute, Is.EqualTo(0));
            Assert.That(duration.Second, Is.EqualTo(0));
            Assert.That(duration.Millisecond, Is.EqualTo(0));

            duration = new Duration(100_000);
            
            Assert.That(duration.Day, Is.EqualTo(0));
            Assert.That(duration.Hour, Is.EqualTo(0));
            Assert.That(duration.Minute, Is.EqualTo(1));
            Assert.That(duration.Second, Is.EqualTo(40));
            Assert.That(duration.Millisecond, Is.EqualTo(0));

            duration = new Duration(10_000_000);
            
            Assert.That(duration.Day, Is.EqualTo(0));
            Assert.That(duration.Hour, Is.EqualTo(2));
            Assert.That(duration.Minute, Is.EqualTo(46));
            Assert.That(duration.Second, Is.EqualTo(40));
            Assert.That(duration.Millisecond, Is.EqualTo(0));

            duration = new Duration(86_399_999);
            
            Assert.That(duration.Day, Is.EqualTo(0));
            Assert.That(duration.Hour, Is.EqualTo(23));
            Assert.That(duration.Minute, Is.EqualTo(59));
            Assert.That(duration.Second, Is.EqualTo(59));
            Assert.That(duration.Millisecond, Is.EqualTo(999));

            duration = new Duration(86_400_000);
            
            Assert.That(duration.Day, Is.EqualTo(1));
            Assert.That(duration.Hour, Is.EqualTo(0));
            Assert.That(duration.Minute, Is.EqualTo(0));
            Assert.That(duration.Second, Is.EqualTo(0));
            Assert.That(duration.Millisecond, Is.EqualTo(0));

            duration = new Duration(100_000_000);
            
            Assert.That(duration.Day, Is.EqualTo(1));
            Assert.That(duration.Hour, Is.EqualTo(3));
            Assert.That(duration.Minute, Is.EqualTo(46));
            Assert.That(duration.Second, Is.EqualTo(40));
            Assert.That(duration.Millisecond, Is.EqualTo(0));

            duration = new Duration(172_799_999);
            
            Assert.That(duration.Day, Is.EqualTo(1));
            Assert.That(duration.Hour, Is.EqualTo(23));
            Assert.That(duration.Minute, Is.EqualTo(59));
            Assert.That(duration.Second, Is.EqualTo(59));
            Assert.That(duration.Millisecond, Is.EqualTo(999));

            duration = new Duration(1_913_078_227);
            
            Assert.That(duration.Day, Is.EqualTo(22));
            Assert.That(duration.Hour, Is.EqualTo(3));
            Assert.That(duration.Minute, Is.EqualTo(24));
            Assert.That(duration.Second, Is.EqualTo(38));
            Assert.That(duration.Millisecond, Is.EqualTo(227));

            duration = new Duration(-1);
            
            Assert.That(duration.Day, Is.EqualTo(0));
            Assert.That(duration.Hour, Is.EqualTo(0));
            Assert.That(duration.Minute, Is.EqualTo(0));
            Assert.That(duration.Second, Is.EqualTo(0));
            Assert.That(duration.Millisecond, Is.EqualTo(-1));

            duration = new Duration(-100_000);
            
            Assert.That(duration.Day, Is.EqualTo(0));
            Assert.That(duration.Hour, Is.EqualTo(0));
            Assert.That(duration.Minute, Is.EqualTo(-1));
            Assert.That(duration.Second, Is.EqualTo(-40));
            Assert.That(duration.Millisecond, Is.EqualTo(0));

            duration = new Duration(-86_399_999);
            
            Assert.That(duration.Day, Is.EqualTo(0));
            Assert.That(duration.Hour, Is.EqualTo(-23));
            Assert.That(duration.Minute, Is.EqualTo(-59));
            Assert.That(duration.Second, Is.EqualTo(-59));
            Assert.That(duration.Millisecond, Is.EqualTo(-999));

            duration = new Duration(-86_400_000);
            
            Assert.That(duration.Day, Is.EqualTo(-1));
            Assert.That(duration.Hour, Is.EqualTo(0));
            Assert.That(duration.Minute, Is.EqualTo(0));
            Assert.That(duration.Second, Is.EqualTo(0));
            Assert.That(duration.Millisecond, Is.EqualTo(0));

            duration = new Duration(-1_035_288_463);
            
            Assert.That(duration.Day, Is.EqualTo(-11));
            Assert.That(duration.Hour, Is.EqualTo(-23));
            Assert.That(duration.Minute, Is.EqualTo(-34));
            Assert.That(duration.Second, Is.EqualTo(-48));
            Assert.That(duration.Millisecond, Is.EqualTo(-463));

        }
        
    }
}