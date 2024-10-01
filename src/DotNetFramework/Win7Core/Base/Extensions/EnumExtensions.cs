using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Win7Core.Base.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Retrieve attribute that is applied to an `Enum`.
        /// </summary>
        public static TAttribute GetAttribute<TAttribute>(this Enum enumValue) where TAttribute : Attribute
        {
            return enumValue
                .GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<TAttribute>();
        }

        /// <summary>
        /// Use DisplayAttribute if exists. Otherwise, use the standard string representation.
        /// </summary>
        public static string GetDisplayName(this Enum enumValue)
        {
            DisplayAttribute displayAttribute = enumValue.GetAttribute<DisplayAttribute>();

            if (displayAttribute != null && displayAttribute.Name != null)
            {
                return displayAttribute.Name;
            }

            return enumValue.ToString();
        }
    }
}
