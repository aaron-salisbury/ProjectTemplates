﻿using System.Text;
using System.Text.RegularExpressions;

namespace DotNetFramework.Core.ExtensionHelpers
{
    public static class StringExtensions
    {
        /// <summary>
        /// Convert to byte array.
        /// </summary>
        public static byte[] ToBytes(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }

        /// <summary>
        /// Split a PascalCase string into separate words.
        /// </summary>
        public static string SplitPascalCase(string str)
        {
            // ref: https://stackoverflow.com/a/3216204

            return Regex.Replace(str, @"(?<=[A-Za-z])(?=[A-Z][a-z])|(?<=[a-z0-9])(?=[0-9]?[A-Z])", " "); ;
        }

        /// <summary>
        /// Return first word within a string.
        /// </summary>
        public static string GetFirstWord(string str)
        {
            // ref: https://stackoverflow.com/a/3607316

            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            str = SplitPascalCase(str);

            return str.IndexOf(" ") > -1 ? str.Substring(0, str.IndexOf(" ")) : str;
        }
    }
}
