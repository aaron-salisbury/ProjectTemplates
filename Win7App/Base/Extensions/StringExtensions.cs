using System.Text.RegularExpressions;

namespace Win7App.Base.Extensions
{
    internal static class StringExtensions
    {
        public static string SplitCamelCase(this string str)
        {
            // stackoverflow.com/questions/5796383/insert-spaces-between-words-on-a-camel-cased-token

            return Regex.Replace(
                Regex.Replace(str, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"),
                @"(\p{Ll})(\P{Ll})", "$1 $2");
        }

        public static string GetFirstWord(this string str)
        {
            // stackoverflow.com/questions/3607310/left-string-function-in-c-sharp

            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            str = str.SplitCamelCase();

            return str.IndexOf(" ") > -1 ? str.Substring(0, str.IndexOf(" ")) : str;
        }
    }
}
