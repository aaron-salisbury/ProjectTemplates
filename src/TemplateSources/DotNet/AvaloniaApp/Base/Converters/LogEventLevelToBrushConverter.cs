using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media;
using Serilog.Events;
using System;
using System.Globalization;

namespace AvaloniaApp.Presentation.Desktop.Base.Converters;

public class LogEventLevelToBrushConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        object? defaultValue = new SolidColorBrush(Colors.Gray);

        object? systemBaseHighResource = null;
        Application.Current?.TryGetResource("SystemBaseHighColor", Application.Current.ActualThemeVariant, out systemBaseHighResource);
        if (systemBaseHighResource != null)
        {
            defaultValue = new SolidColorBrush((Color)systemBaseHighResource);
        }

        if (value == null)
        {
            return defaultValue;
        }

        object? yellowAccent = null;
        Application.Current?.TryGetResource("YellowAccent", null, out yellowAccent);

        object? redAccent = null;
        Application.Current?.TryGetResource("RedAccent", null, out redAccent);

        return (LogEventLevel)value switch
        {
            LogEventLevel.Warning => yellowAccent ?? new SolidColorBrush(Colors.Yellow),
            LogEventLevel.Error or LogEventLevel.Fatal => redAccent ?? new SolidColorBrush(Colors.Red),
            _ => defaultValue,
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
