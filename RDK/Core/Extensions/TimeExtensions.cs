using System;
using System.Text;

namespace RDK.Core.Extensions
{
    public static class TimeExtensions
    {
        private static readonly DateTime UnixRefereceDataTime = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToUniversalTime();

        public static long GetUnixTimeStampLong(this DateTime date) => (long)(date.ToUniversalTime() - UnixRefereceDataTime).TotalMilliseconds;

        public static double GetUnixTimeStampDouble(this DateTime date) => (date.ToUniversalTime() - UnixRefereceDataTime).TotalMilliseconds;

        public static int GetUnixTimeStamp(this DateTime date) => (int)(date.ToUniversalTime() - UnixRefereceDataTime).TotalSeconds;

        /// <summary>
        /// Time passed since 1st, January 1970
        /// </summary>
        /// <param name="dateTime">Value from to which calculate unix reference</param>
        /// <returns>Total seconds password since 1st, January 1970</returns>
        public static long GetUnixTime(this DateTime dateTime) => (long)(dateTime.ToUniversalTime() - UnixRefereceDataTime).TotalSeconds;

        /// <summary>
        /// Time passed since specified value in user friendly string e.g '3 mins ago'
        /// </summary>
        /// <param name="dateTime">Value to convert in user friendly string</param>
        /// <returns>User friendly datetime string e.g '3 mins ago'</returns>
        public static string When(this DateTime dateTime) => dateTime.When(DateTime.Now);

        /// <summary>
        /// Time passed since specified value in user friendly string e.g '3 days ago'
        /// </summary>
        /// <param name="dateTime">Value to convert in user friendly string</param>
        /// <param name="currentTime">Value to take reference as current time when converting to user friendly string</param>
        /// <returns>User friendly datetime string e.g '3 days ago'</returns>
        public static string When(this DateTime dateTime, DateTime currentTime)
        {
            var timespan = currentTime - dateTime;

            if (timespan.Days > 365)
                return string.Format("{0} year{1} ago", timespan.Days / 365, (timespan.Days / 365) > 1 ? "s" : "");

            if (timespan.Days > 30)
                return string.Format("{0} month{1} ago", timespan.Days / 30, (timespan.Days / 30) > 1 ? "s" : "");

            if (timespan.Days > 0)
                return string.Format("{0} day{1} ago", timespan.Days, timespan.Days > 1 ? "s" : "");

            if (timespan.Hours > 0)
                return string.Format("{0} hour{1} ago", timespan.Hours, timespan.Hours > 1 ? "s" : "");

            if (timespan.Minutes > 0)
                return string.Format("{0} minute{1} ago", timespan.Minutes, timespan.Minutes > 1 ? "s" : "");

            return "A moment ago";
        }

        public static string ToPrettyFormat(this TimeSpan span)
        {
            if (span == TimeSpan.Zero)
            {
                return "0 Seconds";
            }

            StringBuilder sBuilder = new();

            if (span.Hours > 0)
            {
                sBuilder.AppendFormat("{0} hour{1} ", span.Hours, span.Hours > 1 ? "s" : string.Empty);
            }

            if (span.Minutes > 0)
            {
                sBuilder.AppendFormat("{0} minute{1} ", span.Minutes, span.Minutes > 1 ? "s" : string.Empty);
            }

            if (span.Seconds > 0)
            {
                sBuilder.AppendFormat("{0} second{1} ", span.Seconds, span.Seconds > 1 ? "s" : string.Empty);
            }

            return sBuilder.ToString();
        }
    }
}
