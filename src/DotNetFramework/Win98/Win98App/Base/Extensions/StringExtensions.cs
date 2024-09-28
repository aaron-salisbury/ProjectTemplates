using System.Text.RegularExpressions;

namespace Win98App.Base.Extensions
{
    public static class StringExtensions
    {
        public static string SplitPascalCase(string value)
        {
            return Regex.Replace(value, @"(?<=[A-Za-z])(?=[A-Z][a-z])|(?<=[a-z0-9])(?=[0-9]?[A-Z])", " "); ;
        }
    }
}
