using System;

namespace DotNetFramework.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToTimeStamp(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy.MM.dd.mm.ss");
        }
    }
}
