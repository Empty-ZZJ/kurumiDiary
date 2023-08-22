using System;

namespace API
{
    /// <summary>
    /// 该类提供时间戳与DateTime类的转换。
    /// </summary>
    public static class TimestampConverter
    {
        /// <summary>
        /// 将DateTime转换为时间戳。
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long DateTimeToTimestamp (DateTime dateTime)
        {
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan timeSpan = dateTime.ToUniversalTime() - unixEpoch;
            return (long)timeSpan.TotalSeconds;
        }

        /// <summary>
        /// 将时间戳转换为DateTime。
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime TimestampToDateTime (long timestamp)
        {
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return unixEpoch.AddSeconds(timestamp).ToLocalTime();
        }
    }
}