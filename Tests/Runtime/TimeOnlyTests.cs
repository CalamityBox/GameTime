using System.Collections.Generic;
using System.Linq;
using GameTime;
using NUnit.Framework;
using static GameTime.TimeOnly;
using static GameTime.Duration;

namespace Tests.Runtime
{
    public class TimeOnlyTests
    {
        [Test]
        public void TestConstants()
        {
            Assert.That(Time_0_00 == new TimeOnly(0), Is.True);
            Assert.That(Time_0_30 == new TimeOnly(1_800_000), Is.True);
            Assert.That(Time_1_00 == new TimeOnly(3_600_000), Is.True);
            Assert.That(Time_1_30 == new TimeOnly(5_400_000), Is.True);
            Assert.That(Time_2_00 == new TimeOnly(7_200_000), Is.True);
            Assert.That(Time_2_30 == new TimeOnly(9_000_000), Is.True);
            Assert.That(Time_3_00 == new TimeOnly(10_800_000), Is.True);
            Assert.That(Time_3_30 == new TimeOnly(12_600_000), Is.True);
            Assert.That(Time_4_00 == new TimeOnly(14_400_000), Is.True);
            Assert.That(Time_4_30 == new TimeOnly(16_200_000), Is.True);
            Assert.That(Time_5_00 == new TimeOnly(18_000_000), Is.True);
            Assert.That(Time_5_30 == new TimeOnly(19_800_000), Is.True);
            Assert.That(Time_6_00 == new TimeOnly(21_600_000), Is.True);
            Assert.That(Time_6_30 == new TimeOnly(23_400_000), Is.True);
            Assert.That(Time_7_00 == new TimeOnly(25_200_000), Is.True);
            Assert.That(Time_7_30 == new TimeOnly(27_000_000), Is.True);
            Assert.That(Time_8_00 == new TimeOnly(28_800_000), Is.True);
            Assert.That(Time_8_30 == new TimeOnly(30_600_000), Is.True);
            Assert.That(Time_9_00 == new TimeOnly(32_400_000), Is.True);
            Assert.That(Time_9_30 == new TimeOnly(34_200_000), Is.True);
            Assert.That(Time_10_00 == new TimeOnly(36_000_000), Is.True);
            Assert.That(Time_10_30 == new TimeOnly(37_800_000), Is.True);
            Assert.That(Time_11_00 == new TimeOnly(39_600_000), Is.True);
            Assert.That(Time_11_30 == new TimeOnly(41_400_000), Is.True);
            Assert.That(Time_12_00 == new TimeOnly(43_200_000), Is.True);
            Assert.That(Time_12_30 == new TimeOnly(45_000_000), Is.True);
            Assert.That(Time_13_00 == new TimeOnly(46_800_000), Is.True);
            Assert.That(Time_13_30 == new TimeOnly(48_600_000), Is.True);
            Assert.That(Time_14_00 == new TimeOnly(50_400_000), Is.True);
            Assert.That(Time_14_30 == new TimeOnly(52_200_000), Is.True);
            Assert.That(Time_15_00 == new TimeOnly(54_000_000), Is.True);
            Assert.That(Time_15_30 == new TimeOnly(55_800_000), Is.True);
            Assert.That(Time_16_00 == new TimeOnly(57_600_000), Is.True);
            Assert.That(Time_16_30 == new TimeOnly(59_400_000), Is.True);
            Assert.That(Time_17_00 == new TimeOnly(61_200_000), Is.True);
            Assert.That(Time_17_30 == new TimeOnly(63_000_000), Is.True);
            Assert.That(Time_18_00 == new TimeOnly(64_800_000), Is.True);
            Assert.That(Time_18_30 == new TimeOnly(66_600_000), Is.True);
            Assert.That(Time_19_00 == new TimeOnly(68_400_000), Is.True);
            Assert.That(Time_19_30 == new TimeOnly(70_200_000), Is.True);
            Assert.That(Time_20_00 == new TimeOnly(72_000_000), Is.True);
            Assert.That(Time_20_30 == new TimeOnly(73_800_000), Is.True);
            Assert.That(Time_21_00 == new TimeOnly(75_600_000), Is.True);
            Assert.That(Time_21_30 == new TimeOnly(77_400_000), Is.True);
            Assert.That(Time_22_00 == new TimeOnly(79_200_000), Is.True);
            Assert.That(Time_22_30 == new TimeOnly(81_000_000), Is.True);
            Assert.That(Time_23_00 == new TimeOnly(82_800_000), Is.True);
            Assert.That(Time_23_30 == new TimeOnly(84_600_000), Is.True);
        }

        [Test]
        public void TestEquality()
        {
            Assert.That(new TimeOnly(0) == new TimeOnly(0), Is.True);
            Assert.That(new TimeOnly(0) == new TimeOnly(1), Is.False);
            Assert.That(new TimeOnly(-1) == new TimeOnly(23, 59, 59, 999), Is.True);
            Assert.That(new TimeOnly(0) - new Duration(1) == new TimeOnly(23, 59, 59, 999), Is.True);
            
            Assert.That(Time_0_00 + Zero == Time_0_00, Is.True);
            Assert.That(Time_0_00 + OneDay == Time_0_00, Is.True);
            Assert.That(Time_3_30 + Zero == Time_3_30, Is.True);
            Assert.That(Time_3_30 + OneDay == Time_3_30, Is.True);
            Assert.That(Time_14_00 + Zero == Time_14_00, Is.True);
            Assert.That(Time_14_00 + OneDay == Time_14_00, Is.True);
            
            Assert.That(Time_23_00 + OneHour == Time_0_00, Is.True);
            Assert.That(Time_0_00 - OneHour == Time_23_00, Is.True);
            
            Assert.That(Time_22_00 + 12 * OneHour == Time_10_00, Is.True);
            Assert.That(Time_2_00 - new Duration(5, 30) == Time_20_30, Is.True);
            
            Assert.That(Time_13_30 - Time_6_00 == new Duration(7, 30), Is.True);
            Assert.That(Time_0_00 - Time_23_00 == OneHour, Is.True);
            Assert.That(Time_2_30 - Time_22_00 == 4 * OneHour + 30 * OneMinute, Is.True);
        }

        [Test]
        public void TestIsBetween()
        {
            Assert.That(Time_1_00.IsBetween(Time_0_00, Time_2_00), Is.True);
            Assert.That(Time_9_30.IsBetween(Time_9_30, Time_9_30),  Is.True);
            Assert.That(Time_13_00.IsBetween(Time_12_00, Time_13_00), Is.True);
            Assert.That(Time_15_30.IsBetween(Time_15_30, Time_20_30), Is.True);
            Assert.That(Time_23_00.IsBetween(Time_20_00, Time_0_00), Is.True);
            Assert.That(Time_15_00.IsBetween(Time_10_30, Time_1_00), Is.True);
            Assert.That(Time_23_30.IsBetween(Time_23_00, Time_0_00), Is.True);
            
            Assert.That(Time_0_00.IsBetween(Time_1_00, Time_2_00), Is.False);
            Assert.That(Time_12_00.IsBetween(Time_12_30, Time_11_30), Is.False);
            Assert.That(Time_22_00.IsBetween(Time_23_00, Time_0_00), Is.False);
        }

        [Test]
        public void TestListSorting()
        {
            List<TimeOnly> sorted =  new List<TimeOnly>
            {
                Time_0_00,
                Time_1_00,
                Time_1_30,
                Time_2_00,
                Time_2_30,
                Time_3_00,
                Time_3_30,
                Time_4_00,
                Time_4_30,
                Time_5_00,
                Time_5_30,
                Time_6_00,
                Time_6_30,
                Time_7_00,
                Time_7_30,
                Time_8_00,
                Time_8_30,
                Time_9_00,
                Time_9_30,
                Time_10_00,
                Time_10_30,
                Time_11_00,
                Time_11_30,
                Time_12_00,
                Time_12_30,
                Time_13_00,
                Time_13_30,
                Time_14_00,
                Time_14_30,
                Time_15_00,
                Time_15_30,
                Time_16_00,
                Time_16_30,
                Time_17_00,
                Time_17_30,
                Time_18_00,
                Time_18_30,
                Time_19_00,
                Time_19_30,
                Time_20_00,
                Time_20_30,
                Time_21_00,
                Time_21_30,
                Time_22_00,
                Time_22_30,
                Time_23_00,
                Time_23_30,
            };
            
            List<TimeOnly> unsorted =  new List<TimeOnly>
            {
                Time_2_00,
                Time_4_30,
                Time_3_30,
                Time_2_30,
                Time_22_30,
                Time_5_00,
                Time_20_30,
                Time_8_00,
                Time_6_30,
                Time_7_00,
                Time_3_00,
                Time_9_30,
                Time_7_30,
                Time_8_30,
                Time_12_00,
                Time_9_00,
                Time_10_00,
                Time_0_00,
                Time_13_30,
                Time_10_30,
                Time_11_30,
                Time_11_00,
                Time_23_00,
                Time_13_00,
                Time_20_00,
                Time_15_00,
                Time_14_00,
                Time_4_00,
                Time_17_00,
                Time_16_00,
                Time_14_30,
                Time_15_30,
                Time_16_30,
                Time_1_00,
                Time_18_30,
                Time_5_30,
                Time_17_30,
                Time_18_00,
                Time_22_00,
                Time_23_30,
                Time_12_30,
                Time_6_00,
                Time_21_00,
                Time_19_00,
                Time_21_30,
                Time_19_30,
                Time_1_30,
            };
            
            unsorted.Sort();
            
            Assert.That(sorted.SequenceEqual(unsorted), Is.True);
        }

        [Test]
        public void TestGetDayProgress()
        {
            float tolerance = 0.00001f;
            
            Assert.That(Time_0_00.GetDayProgress(), Is.EqualTo(0f));
            Assert.That(Time_1_00.GetDayProgress(), Is.EqualTo(0.04167f).Within(tolerance));
            Assert.That(Time_3_30.GetDayProgress(), Is.EqualTo(0.14583f).Within(tolerance));
            Assert.That(Time_7_00.GetDayProgress(), Is.EqualTo(0.29167f).Within(tolerance));
            Assert.That(Time_12_00.GetDayProgress(), Is.EqualTo(0.5f).Within(tolerance));
            Assert.That(Time_16_30.GetDayProgress(), Is.EqualTo(0.6875f).Within(tolerance));
            Assert.That(Time_19_00.GetDayProgress(), Is.EqualTo(0.79167f).Within(tolerance));
            Assert.That(Time_23_30.GetDayProgress(), Is.EqualTo(0.97917f).Within(tolerance));
        }

        [Test]
        public void TestStringFormatting()
        {
            Assert.That($"{Time_0_00:HH:mm:ss:fff}", Is.EqualTo("00:00:00:000"));
            Assert.That($"{Time_0_00:HH:mm:ss}", Is.EqualTo("00:00:00"));
            Assert.That($"{Time_0_00:HH:mm}", Is.EqualTo("00:00"));
            Assert.That($"{Time_0_00:hh:mm:ss:fff tt}", Is.EqualTo("12:00:00:000 AM"));
            Assert.That($"{Time_0_00:hh:mm:ss tt}", Is.EqualTo("12:00:00 AM"));
            Assert.That($"{Time_0_00:hh:mm tt}", Is.EqualTo("12:00 AM"));
            Assert.That($"{Time_0_00:H:m:s:fff}", Is.EqualTo("0:0:0:000"));
            Assert.That($"{Time_0_00:h:m:s:fff tt}", Is.EqualTo("12:0:0:000 AM"));
            
            Assert.That($"{Time_12_00:HH:mm:ss:fff}", Is.EqualTo("12:00:00:000"));
            Assert.That($"{Time_12_00:HH:mm:ss}", Is.EqualTo("12:00:00"));
            Assert.That($"{Time_12_00:HH:mm}", Is.EqualTo("12:00"));
            Assert.That($"{Time_12_00:hh:mm:ss:fff tt}", Is.EqualTo("12:00:00:000 PM"));
            Assert.That($"{Time_12_00:hh:mm:ss tt}", Is.EqualTo("12:00:00 PM"));
            Assert.That($"{Time_12_00:hh:mm tt}", Is.EqualTo("12:00 PM"));
            Assert.That($"{Time_12_00:H:m:s:fff}", Is.EqualTo("12:0:0:000"));
            Assert.That($"{Time_12_00:h:m:s:fff tt}", Is.EqualTo("12:0:0:000 PM"));

            TimeOnly timeOnly = new(9, 32, 18, 45);
            
            Assert.That($"{timeOnly:HH:mm:ss:fff}", Is.EqualTo("09:32:18:045"));
            Assert.That($"{timeOnly:HH:mm:ss}", Is.EqualTo("09:32:18"));
            Assert.That($"{timeOnly:HH:mm}", Is.EqualTo("09:32"));
            Assert.That($"{timeOnly:hh:mm:ss:fff tt}", Is.EqualTo("09:32:18:045 AM"));
            Assert.That($"{timeOnly:hh:mm:ss tt}", Is.EqualTo("09:32:18 AM"));
            Assert.That($"{timeOnly:hh:mm tt}", Is.EqualTo("09:32 AM"));
            Assert.That($"{timeOnly:H:m:s:fff}", Is.EqualTo("9:32:18:045"));
            Assert.That($"{timeOnly:h:m:s:fff tt}", Is.EqualTo("9:32:18:045 AM"));

            timeOnly = new(20, 5, 8, 652);
            
            Assert.That($"{timeOnly:HH:mm:ss:fff}", Is.EqualTo("20:05:08:652"));
            Assert.That($"{timeOnly:HH:mm:ss}", Is.EqualTo("20:05:08"));
            Assert.That($"{timeOnly:HH:mm}", Is.EqualTo("20:05"));
            Assert.That($"{timeOnly:hh:mm:ss:fff tt}", Is.EqualTo("08:05:08:652 PM"));
            Assert.That($"{timeOnly:hh:mm:ss tt}", Is.EqualTo("08:05:08 PM"));
            Assert.That($"{timeOnly:hh:mm tt}", Is.EqualTo("08:05 PM"));
            Assert.That($"{timeOnly:H:m:s:fff}", Is.EqualTo("20:5:8:652"));
            Assert.That($"{timeOnly:h:m:s:fff tt}", Is.EqualTo("8:5:8:652 PM"));

            timeOnly = new(18, 3, 1, 14);
            
            Assert.That($"{timeOnly}", Is.EqualTo("06:03:01:014 PM"));
            
        }
    }
}