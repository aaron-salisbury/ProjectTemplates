using System;

namespace DotNetFramework.Core.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Convert DateTime to a standard time-stamp for inclusion in a file-name.
        /// </summary>
        public static string ToTimeStamp(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy.MM.dd.mm.ss");
        }
    }
}
