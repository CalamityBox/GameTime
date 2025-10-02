using static GameTime.TimeConversions;
using NUnit.Framework;

namespace Tests.Runtime
{
    public class TimeConversionTests
    {
        
        [Test]
        public void TestTimeConversionConstants()
        {
            Assert.That(NUM_MILLISECONDS_PER_SECOND, Is.EqualTo(1_000));
            Assert.That(NUM_MILLISECONDS_PER_MINUTE, Is.EqualTo(60_000));
            Assert.That(NUM_MILLISECONDS_PER_HOUR, Is.EqualTo(3_600_000));
            Assert.That(NUM_MILLISECONDS_PER_DAY, Is.EqualTo(86_400_000));
            Assert.That(NUM_SECONDS_PER_MINUTE, Is.EqualTo(60));
            Assert.That(NUM_SECONDS_PER_HOUR, Is.EqualTo(3_600));
            Assert.That(NUM_SECONDS_PER_DAY, Is.EqualTo(86_400));
            Assert.That(NUM_MINUTES_PER_HOUR, Is.EqualTo(60));
            Assert.That(NUM_MINUTES_PER_DAY, Is.EqualTo(1_440));
            Assert.That(NUM_HOURS_PER_DAY, Is.EqualTo(24));
        }

        [Test]
        public void TestConvert24HourTo12Hour()
        {
            Assert.That(Convert24HourTo12Hour(0), Is.EqualTo(12));
            Assert.That(Convert24HourTo12Hour(1), Is.EqualTo(1));
            Assert.That(Convert24HourTo12Hour(2), Is.EqualTo(2));
            Assert.That(Convert24HourTo12Hour(3), Is.EqualTo(3));
            Assert.That(Convert24HourTo12Hour(4), Is.EqualTo(4));
            Assert.That(Convert24HourTo12Hour(5), Is.EqualTo(5));
            Assert.That(Convert24HourTo12Hour(6), Is.EqualTo(6));
            Assert.That(Convert24HourTo12Hour(7), Is.EqualTo(7));
            Assert.That(Convert24HourTo12Hour(8), Is.EqualTo(8));
            Assert.That(Convert24HourTo12Hour(9), Is.EqualTo(9));
            Assert.That(Convert24HourTo12Hour(10), Is.EqualTo(10));
            Assert.That(Convert24HourTo12Hour(11), Is.EqualTo(11));
            Assert.That(Convert24HourTo12Hour(12), Is.EqualTo(12));
            Assert.That(Convert24HourTo12Hour(13), Is.EqualTo(1));
            Assert.That(Convert24HourTo12Hour(14), Is.EqualTo(2));
            Assert.That(Convert24HourTo12Hour(15), Is.EqualTo(3));
            Assert.That(Convert24HourTo12Hour(16), Is.EqualTo(4));
            Assert.That(Convert24HourTo12Hour(17), Is.EqualTo(5));
            Assert.That(Convert24HourTo12Hour(18), Is.EqualTo(6));
            Assert.That(Convert24HourTo12Hour(19), Is.EqualTo(7));
            Assert.That(Convert24HourTo12Hour(20), Is.EqualTo(8));
            Assert.That(Convert24HourTo12Hour(21), Is.EqualTo(9));
            Assert.That(Convert24HourTo12Hour(22), Is.EqualTo(10));
            Assert.That(Convert24HourTo12Hour(23), Is.EqualTo(11));
        }
        
    }
}
