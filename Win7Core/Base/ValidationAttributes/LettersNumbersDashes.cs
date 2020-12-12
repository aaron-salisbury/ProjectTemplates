using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Win7Core.Base.ValidationAttributes
{
    public class LettersNumbersDashes : ValidationAttribute
    {
        //Learn more about validation attributes: code.msdn.microsoft.com/windowsdesktop/Validation-in-MVVM-using-12dafef3

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
