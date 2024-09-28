using System;
using System.Collections.Generic;

namespace WinXPCore.Base.DataAnnotationValidation
{
    // https://referencesource.microsoft.com/#System.ComponentModel.DataAnnotations/DataAnnotations/ValidationResult.cs
    public class ValidationResult
    {
        private IEnumerable<string> _memberNames;
        private string _errorMessage;

        public static readonly ValidationResult Success;

        public ValidationResult(string errorMessage) : this(errorMessage, null) { }

        public ValidationResult(string errorMessage, IEnumerable<string> memberNames)
        {
            this._errorMessage = errorMessage;
            this._memberNames = memberNames ?? new string[0];
        }

        protected ValidationResult(ValidationResult validationResult)
        {
            if (validationResult == null)
            {
                throw new ArgumentNullException("validationResult");
            }

            this._errorMessage = validationResult._errorMessage;
            this._memberNames = validationResult._memberNames;
        }

        public IEnumerable<string> MemberNames
        {
            get
            {
                return this._memberNames;
            }
        }

        public string ErrorMessage
        {
            get
            {
                return this._errorMessage;
            }
            set
            {
                this._errorMessage = value;
            }
        }

        public override string ToString()
        {
            return this.ErrorMessage ?? base.ToString();
        }
    }
}
