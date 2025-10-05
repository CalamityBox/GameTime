using GameTime;
using GameTime.Clock;
using static GameTime.TimeOnly;
using NUnit.Framework;

namespace Tests.Runtime
{
    public class ClockTests
    {

        public class HourClockTests
        {
            
            [Test]
            public void TestFormatting()
            {
                HourClock clock = new(new TimeOnly(21, 9, 4, 80));
                Assert.That($"{clock}", Is.EqualTo("9 PM"));
                Assert.That($"{clock:HH:mm:ss:fff}", Is.EqualTo("21:09:04:080"));
                Assert.That($"{clock:hh:mm:ss:fff tt}", Is.EqualTo("09:09:04:080 PM"));
            
                clock.Format = "hh:mm:ss tt";
                Assert.That($"{clock}", Is.EqualTo("09:09:04 PM"));
            
                clock.Format = "h:mm:ss tt";
                Assert.That($"{clock}", Is.EqualTo("9:09:04 PM"));
            
                clock.Format = "H";
                Assert.That($"{clock}", Is.EqualTo("21"));
            }
            
            [Test]
            public void TestIsEqualTo()
            {
                HourClock clock = new(Time_0_00);
                Assert.That(clock.IsEqualTo(new TimeOnly(0, 17, 8, 416)), Is.True);
                Assert.That(clock.IsEqualTo(new TimeOnly(0, 59, 59, 999)), Is.True);
                Assert.That(clock.IsEqualTo(Time_0_00), Is.True);
                Assert.That(clock.IsEqualTo(Time_0_30), Is.True);
                Assert.That(clock.IsEqualTo(Time_1_00), Is.False);
                Assert.That(clock.IsEqualTo(Time_2_00), Is.False);
                Assert.That(clock.IsEqualTo(Time_3_00), Is.False);
                Assert.That(clock.IsEqualTo(Time_12_00), Is.False);
                Assert.That(clock.IsEqualTo(new TimeOnly(23, 59, 59, 999)), Is.False);
            }
            
        }

        public class MinuteClockTests
        {
            
            [Test]
            public void TestFormatting()
            {
                MinuteClock clock = new(new TimeOnly(21, 9, 4, 80));
                Assert.That($"{clock}", Is.EqualTo("9:09 PM"));
                Assert.That($"{clock:HH:mm:ss:fff}", Is.EqualTo("21:09:04:080"));
                Assert.That($"{clock:hh:mm:ss:fff tt}", Is.EqualTo("09:09:04:080 PM"));
                
                clock.Format = "hh:mm:ss tt";
                Assert.That($"{clock}", Is.EqualTo("09:09:04 PM"));
                
                clock.Format = "h:mm:ss tt";
                Assert.That($"{clock}", Is.EqualTo("9:09:04 PM"));
                
                clock.Format = "H";
                Assert.That($"{clock}", Is.EqualTo("21"));
            }

            [Test]
            public void TestIsEqualTo()
            {
                MinuteClock clock = new(Time_8_30);
                Assert.That(clock.IsEqualTo(new TimeOnly(8, 30, 50, 045)), Is.True);
                Assert.That(clock.IsEqualTo(Time_8_30), Is.True);
                Assert.That(clock.IsEqualTo(Time_0_00), Is.False);
                Assert.That(clock.IsEqualTo(Time_1_00), Is.False);
                Assert.That(clock.IsEqualTo(Time_7_00), Is.False);
                Assert.That(clock.IsEqualTo(Time_8_00), Is.False);
                Assert.That(clock.IsEqualTo(Time_9_00), Is.False);
                Assert.That(clock.IsEqualTo(Time_12_00), Is.False);
                Assert.That(clock.IsEqualTo(new TimeOnly(8, 29)),  Is.False);
                Assert.That(clock.IsEqualTo(new TimeOnly(8, 31)),  Is.False);
            }
            
        }

        public class SecondClockTests
        {
            
            [Test]
            public void TestFormatting()
            {
                SecondClock clock = new(new TimeOnly(21, 9, 4, 80));
                Assert.That($"{clock}", Is.EqualTo("9:09:04 PM"));
                Assert.That($"{clock:HH:mm:ss:fff}", Is.EqualTo("21:09:04:080"));
                Assert.That($"{clock:hh:mm:ss:fff tt}", Is.EqualTo("09:09:04:080 PM"));
                
                clock.Format = "hh:mm:ss tt";
                Assert.That($"{clock}", Is.EqualTo("09:09:04 PM"));
                
                clock.Format = "h:mm:ss tt";
                Assert.That($"{clock}", Is.EqualTo("9:09:04 PM"));
                
                clock.Format = "H";
                Assert.That($"{clock}", Is.EqualTo("21"));
            }

            [Test]
            public void TestIsEqualTo()
            {
                SecondClock clock = new(new TimeOnly(3, 28, 56, 621));
                Assert.That(clock.IsEqualTo(new TimeOnly(3, 28, 56, 812)), Is.True);
                Assert.That(clock.IsEqualTo(new TimeOnly(2, 28, 56, 812)), Is.False);
                Assert.That(clock.IsEqualTo(new TimeOnly(3, 29, 56, 812)), Is.False);
                Assert.That(clock.IsEqualTo(new TimeOnly(3, 28, 55, 812)), Is.False);
            }
            
        }

        public class MillisecondClockTests
        {
            
            [Test]
            public void TestFormatting()
            {
                MillisecondClock clock = new(new TimeOnly(21, 9, 4, 80));
                Assert.That($"{clock}", Is.EqualTo("9:09:04:080 PM"));
                Assert.That($"{clock:HH:mm:ss:fff}", Is.EqualTo("21:09:04:080"));
                Assert.That($"{clock:hh:mm:ss:fff tt}", Is.EqualTo("09:09:04:080 PM"));
                
                clock.Format = "hh:mm:ss tt";
                Assert.That($"{clock}", Is.EqualTo("09:09:04 PM"));
                
                clock.Format = "h:mm:ss tt";
                Assert.That($"{clock}", Is.EqualTo("9:09:04 PM"));
                
                clock.Format = "H";
                Assert.That($"{clock}", Is.EqualTo("21"));
            }

            [Test]
            public void TestIsEqualTo()
            {
                MillisecondClock clock = new(new TimeOnly(15, 36, 12, 073));
                Assert.That(clock.IsEqualTo(clock.Time), Is.True);
                Assert.That(clock.IsEqualTo(new TimeOnly(14, 36, 12, 073)), Is.False);
                Assert.That(clock.IsEqualTo(new TimeOnly(15, 37, 12, 073)), Is.False);
                Assert.That(clock.IsEqualTo(new TimeOnly(15, 36, 11, 073)), Is.False);
                Assert.That(clock.IsEqualTo(new TimeOnly(15, 36, 12, 072)), Is.False);
            }
            
        }
        
    }
}