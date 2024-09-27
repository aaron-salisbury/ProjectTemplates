using System;

namespace AvaloniaApp.Business.Base.Extensions;

public static class DecimalExtensions
{
    /// <summary>
    /// Truncate to specified number of decimal places.
    /// </summary>
    public static decimal PrecisionTruncate(this decimal d, byte precision)
    {
        // ref: https://stackoverflow.com/a/43639947

        decimal r = Math.Round(d, precision);

        if (d > 0 && r > d)
        {
            return r - new decimal(1, 0, 0, false, precision);
        }
        else if (d < 0 && r < d)
        {
            return r + new decimal(1, 0, 0, false, precision);
        }

        return r;
    }
}
