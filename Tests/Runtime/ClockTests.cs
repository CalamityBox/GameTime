using GameTime;
using GameTime.Clock;
using NUnit.Framework;

namespace Tests.Runtime
{
    public class ClockTests
    {

        [Test]
        public void TestHourClockFormatting()
        {
            HourClock hourClock = new(new TimeOnly(21, 9, 4, 80));
            Assert.That($"{hourClock}", Is.EqualTo("9 PM"));
            Assert.That($"{hourClock:HH:mm:ss:fff}", Is.EqualTo("21:09:04:080"));
            Assert.That($"{hourClock:hh:mm:ss:fff tt}", Is.EqualTo("09:09:04:080 PM"));
            hourClock.Format = "hh:mm:ss tt";
            Assert.That($"{hourClock}", Is.EqualTo("09:09:04 PM"));
        }
        
        [Test]
        public void TestMinuteClockFormatting()
        {
            
        }
        
        [Test]
        public void TestSecondClockFormatting()
        {
            
        }
        
        [Test]
        public void TestMillisecondClockFormatting()
        {
            
        }
        
    }
}