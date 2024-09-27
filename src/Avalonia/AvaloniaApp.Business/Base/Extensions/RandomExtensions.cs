using System;

namespace AvaloniaApp.Business.Base.Extensions;

public static class RandomExtensions
{
    /// <summary>
    /// Returns a random decimal (with decimal-place precision of a double), optionally that is within a specified range.
    /// </summary>
    public static decimal NextDecimal(this Random random, decimal minValue = decimal.MinValue, decimal maxValue = decimal.MaxValue)
    {
        decimal randomDecimal = new(random.NextDouble());

        return maxValue * randomDecimal + minValue * (1 - randomDecimal);
    }
}
