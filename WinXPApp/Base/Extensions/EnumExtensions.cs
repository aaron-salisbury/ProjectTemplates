using System;
using System.Linq;
using System.Reflection;

namespace Win7App.Base.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        ///     Retrieve attribute that is applied to an `Enum`.
        /// </summary>
        public static TAttribute GetAttribute<TAttribute>(this Enum enumValue) where TAttribute : Attribute
        {
            MemberInfo info = enumValue
                .GetType()
                .GetMember(enumValue.ToString())
                .First();

            return (TAttribute)Attribute.GetCustomAttribute(info, typeof(TAttribute));
        }
    }
}
