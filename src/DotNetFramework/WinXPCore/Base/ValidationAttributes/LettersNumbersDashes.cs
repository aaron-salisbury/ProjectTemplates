using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WinXPCore.Base.ValidationAttributes
{
    public class LettersNumbersDashes : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            bool result = true;

            if (value != null)
            {
                result = Regex.IsMatch(value.ToString(), @"^[a-zA-Z0-9-]+$");
            }

            return result;
        }
    }
}
