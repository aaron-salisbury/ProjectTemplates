using System.ComponentModel.DataAnnotations;

namespace WinXPCore.Base.ValidationAttributes
{
    public class StringExactLength : ValidationAttribute
    {
        public int Length { get; set; }

        public StringExactLength(int length)
        {
            Length = length;
        }

        public override bool IsValid(object value)
        {
            bool result = true;

            if (value != null)
            {
                result = value.ToString().Length == Length;
            }

            return result;
        }
    }
}
