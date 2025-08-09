using Avalonia.Data;
using Avalonia.Data.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace AvaloniaApp.Presentation.Desktop.Base.Converters;

public class EnumToDisplayNameConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null)
        {
            return null;
        }
        else if (value is IEnumerable enums)
        {
            List<string> enumDisplayNames = [];

            foreach (Enum e in enums)
            {
                enumDisplayNames.Add(GetAttribute<DisplayAttribute>(e)?.Name ?? e.ToString());
            }

            return enumDisplayNames;
        }
        else
        {
            return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
        }
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }

    private static TAttribute? GetAttribute<TAttribute>(Enum enumValue) where TAttribute : Attribute
    {
        return enumValue
            .GetType()
            .GetMember(enumValue.ToString())
            .First()
            .GetCustomAttribute<TAttribute>();
    }
}
