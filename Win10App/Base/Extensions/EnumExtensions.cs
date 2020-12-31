using System;
using System.Linq;
using System.Reflection;

namespace Win10App.Base.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        ///     Retrieve attribute that is applied to an `Enum`.
        /// </summary>
        public static TAttribute GetAttribute<TAttribute>(this Enum enumValue) where TAttribute : Attribute
        {
            return enumValue
                .GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<TAttribute>();
        }
    }
}
