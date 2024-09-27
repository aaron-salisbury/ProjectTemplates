using System;

namespace AvaloniaApp.Business.Base.Extensions;

public static class ByteArrayExtensions
{
    public static bool EqualsByteArray(this byte[] array1, byte[] array2)
    {
        // ref: https://stackoverflow.com/a/2138588

        if (array1.Length != array2.Length)
        {
            return false;
        }

        for (int i = 0; i < array1.Length; i++)
        {
            if (array1[i] != array2[i])
            {
                return false;
            }
        }

        return true;
    }
}
