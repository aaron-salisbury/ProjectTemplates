using System;

namespace DotNetFramework.Core.ExtensionHelpers
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Convert DateTime to a standard time-stamp for inclusion in a file-name.
        /// </summary>
        public static string ToTimeStamp(DateTime dateTime)
        {
            return dateTime.ToString("yyyy.MM.dd.mm.ss");
        }
    }
}
