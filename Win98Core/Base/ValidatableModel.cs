using System;
using System.Collections.Generic;

namespace Win98Core.Base
{
    public class ValidatableModel
    {
        public List<string> Errors { get; set; } = new List<string>();
        public string ErrorMessage { get => string.Join(Environment.NewLine, Errors.ToArray()); }
        public bool IsValid { get => Validate(); }

        public virtual bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}
