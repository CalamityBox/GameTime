namespace GameTime
{
    public partial struct TimeOnly
    {
        
        private const string MORNING_SUFFIX = "AM";
        private const string AFTERNOON_SUFFIX = "PM";
        
        /// <summary>
        /// Represents midnight, or 12:00:00:000 AM.
        /// </summary>
        public static TimeOnly Time_0_00 = new(0);
        
        /// <summary>
        /// Represents 12:30:00:000 AM.
        /// </summary>
        public static TimeOnly Time_0_30 = new(0, 30);
        
        /// <summary>
        /// Represents 1:00:00:000 AM.
        /// </summary>
        public static TimeOnly Time_1_00 = new(1, 0);
        
        /// <summary>
        /// Represents 1:30:00:000 AM.
        /// </summary>
        public static TimeOnly Time_1_30 = new(1, 30);
        
        /// <summary>
        /// Represents 2:00:00:000 AM.
        /// </summary>
        public static TimeOnly Time_2_00 = new(2, 0);
        
        /// <summary>
        /// Represents 2:30:00:000 AM.
        /// </summary>
        public static TimeOnly Time_2_30 = new(2, 30);
        
        /// <summary>
        /// Represents 3:00:00:000 AM.
        /// </summary>
        public static TimeOnly Time_3_00 = new(3, 0);
        
        /// <summary>
        /// Represents 3:30:00:000 AM.
        /// </summary>
        public static TimeOnly Time_3_30 = new(3, 30);
        
        /// <summary>
        /// Represents 4:00:00:000 AM.
        /// </summary>
        public static TimeOnly Time_4_00 = new(4, 0);
        
        /// <summary>
        /// Represents 4:30:00:000 AM.
        /// </summary>
        public static TimeOnly Time_4_30 = new(4, 30);
        
        /// <summary>
        /// Represents 5:00:00:000 AM.
        /// </summary>
        public static TimeOnly Time_5_00 = new(5, 0);
        
        /// <summary>
        /// Represents 5:30:00:000 AM.
        /// </summary>
        public static TimeOnly Time_5_30 = new(5, 30);
        
        /// <summary>
        /// Represents 6:00:00:000 AM.
        /// </summary>
        public static TimeOnly Time_6_00 = new(6, 0);
        
        /// <summary>
        /// Represents 6:30:00:000 AM.
        /// </summary>
        public static TimeOnly Time_6_30 = new(6, 30);
        
        /// <summary>
        /// Represents 7:00:00:000 AM.
        /// </summary>
        public static TimeOnly Time_7_00 = new(7, 0);
        
        /// <summary>
        /// Represents 7:30:00:000 AM.
        /// </summary>
        public static TimeOnly Time_7_30 = new(7, 30);
        
        /// <summary>
        /// Represents 8:00:00:000 AM.
        /// </summary>
        public static TimeOnly Time_8_00 = new(8, 0);
        
        /// <summary>
        /// Represents 8:30:00:000 AM.
        /// </summary>
        public static TimeOnly Time_8_30 = new(8, 30);
        
        /// <summary>
        /// Represents 9:00:00:000 AM.
        /// </summary>
        public static TimeOnly Time_9_00 = new(9, 0);
        
        /// <summary>
        /// Represents 9:30:00:000 AM.
        /// </summary>
        public static TimeOnly Time_9_30 = new(9, 30);
        
        /// <summary>
        /// Represents 10:00:00:000 AM.
        /// </summary>
        public static TimeOnly Time_10_00 = new(10, 0);
        
        /// <summary>
        /// Represents 10:30:00:000 AM.
        /// </summary>
        public static TimeOnly Time_10_30 = new(10, 30);
        
        /// <summary>
        /// Represents 11:00:00:000 AM.
        /// </summary>
        public static TimeOnly Time_11_00 = new(11, 0);
        
        /// <summary>
        /// Represents 11:30:00:000 AM.
        /// </summary>
        public static TimeOnly Time_11_30 = new(11, 30);
        
        /// <summary>
        /// Represents 12:00:00:000 PM.
        /// </summary>
        public static TimeOnly Time_12_00 = new(12, 0);
        
        /// <summary>
        /// Represents 12:30:00:000 PM.
        /// </summary>
        public static TimeOnly Time_12_30 = new(12, 30);
        
        /// <summary>
        /// Represents 1:00:00:000 PM.
        /// </summary>
        public static TimeOnly Time_13_00 = new(13, 0);
        
        /// <summary>
        /// Represents 1:30:00:000 PM.
        /// </summary>
        public static TimeOnly Time_13_30 = new(13, 30);
        
        /// <summary>
        /// Represents 2:00:00:000 PM.
        /// </summary>
        public static TimeOnly Time_14_00 = new(14, 0);
        
        /// <summary>
        /// Represents 2:30:00:000 PM.
        /// </summary>
        public static TimeOnly Time_14_30 = new(14, 30);
        
        /// <summary>
        /// Represents 3:00:00:000 PM.
        /// </summary>
        public static TimeOnly Time_15_00 = new(15, 0);
        
        /// <summary>
        /// Represents 3:30:00:000 PM.
        /// </summary>
        public static TimeOnly Time_15_30 = new(15, 30);
        
        /// <summary>
        /// Represents 4:00:00:000 PM.
        /// </summary>
        public static TimeOnly Time_16_00 = new(16, 0);
        
        /// <summary>
        /// Represents 4:30:00:000 PM.
        /// </summary>
        public static TimeOnly Time_16_30 = new(16, 30);
        
        /// <summary>
        /// Represents 5:00:00:000 PM.
        /// </summary>
        public static TimeOnly Time_17_00 = new(17, 0);
        
        /// <summary>
        /// Represents 5:30:00:000 PM.
        /// </summary>
        public static TimeOnly Time_17_30 = new(17, 30);
        
        /// <summary>
        /// Represents 6:00:00:000 PM.
        /// </summary>
        public static TimeOnly Time_18_00 = new(18, 0);
        
        /// <summary>
        /// Represents 6:30:00:000 PM.
        /// </summary>
        public static TimeOnly Time_18_30 = new(18, 30);
        
        /// <summary>
        /// Represents 7:00:00:000 PM.
        /// </summary>
        public static TimeOnly Time_19_00 = new(19, 0);
        
        /// <summary>
        /// Represents 7:30:00:000 PM.
        /// </summary>
        public static TimeOnly Time_19_30 = new(19, 30);
        
        /// <summary>
        /// Represents 8:00:00:000 PM.
        /// </summary>
        public static TimeOnly Time_20_00 = new(20, 0);
        
        /// <summary>
        /// Represents 8:30:00:000 PM.
        /// </summary>
        public static TimeOnly Time_20_30 = new(20, 30);
        
        /// <summary>
        /// Represents 9:00:00:000 PM.
        /// </summary>
        public static TimeOnly Time_21_00 = new(21, 0);
        
        /// <summary>
        /// Represents 9:30:00:000 PM.
        /// </summary>
        public static TimeOnly Time_21_30 = new(21, 30);
        
        /// <summary>
        /// Represents 10:00:00:000 PM.
        /// </summary>
        public static TimeOnly Time_22_00 = new(22, 0);
        
        /// <summary>
        /// Represents 10:30:00:000 PM.
        /// </summary>
        public static TimeOnly Time_22_30 = new(22, 30);
        
        /// <summary>
        /// Represents 11:00:00:000 PM.
        /// </summary>
        public static TimeOnly Time_23_00 = new(23, 0);
        
        /// <summary>
        /// Represents 11:30:00:000 PM.
        /// </summary>
        public static TimeOnly Time_23_30 = new(23, 30);
        
    }
}