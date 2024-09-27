using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace AvaloniaApp.Presentation.Desktop.Base.Converters;

public class DateFormatConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is DateTimeOffset dto)
        {
            value = dto.DateTime;
        }

        if (value is DateTime dt)
        {
            if (parameter is string parameterText)
            {
                return dt.ToString(parameterText);
            }
            else
            {
                return dt.ToString();
            }
        }

        return null;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null)
        {
            return null;
        }

        if (targetType == typeof(DateTimeOffset))
        {
            if (parameter != null && 
                DateTimeOffset.TryParseExact(value.ToString(), parameter?.ToString() ?? "dd/MM/yyyy", null, DateTimeStyles.None, out DateTimeOffset dtoExact))
            {
                return dtoExact;
            }

            if (DateTimeOffset.TryParse(value.ToString(), out DateTimeOffset dto))
            {
                return dto;
            }
        }

        if (targetType == typeof(DateTime))
        {
            if (parameter != null && 
                DateTime.TryParseExact(value.ToString(), parameter?.ToString() ?? "dd/MM/yyyy", null, DateTimeStyles.None, out DateTime dtExact))
            {
                return dtExact;
            }

            if (DateTime.TryParse(value.ToString(), out DateTime dt))
            {
                return dt;
            }
        }

        return null;
    }
}
