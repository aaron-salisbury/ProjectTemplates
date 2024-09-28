using System;
using System.Collections.Generic;

namespace Win98Core.Base
{
    public class ValidatableModel
    {
        private List<string> _errors;
        public List<string> Errors
        {
            get { return _errors; }
            set { _errors = value; }
        }

        public string ErrorMessage
        {
            get
            {
                if (Errors != null)
                {
                    return string.Join(Environment.NewLine, Errors.ToArray());
                }
                return null;
            }
        }

        public bool IsValid
        {
            get { return Validate(); }
        }

        public virtual bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}
