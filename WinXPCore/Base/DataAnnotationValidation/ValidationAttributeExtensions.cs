using System;
using System.ComponentModel.DataAnnotations;

namespace WinXPCore.Base.DataAnnotationValidation
{
    // https://referencesource.microsoft.com/#System.ComponentModel.DataAnnotations/DataAnnotations/ValidationAttribute.cs,e3d99db80012b91b
    public static class ValidationAttributeExtensions
    {
        public static ValidationResult IsValid(this ValidationAttribute validationAttribute, object value, ValidationContext validationContext)
        {
            //if (validationAttribute._hasBaseIsValid)
            //{
            //    // this means neither of the IsValid methods has been overriden, throw.
            //    throw new NotImplementedException(DataAnnotationsResources.ValidationAttribute_IsValid_NotImplemented);
            //}

            ValidationResult result = ValidationResult.Success;

            // call overridden method.
            if (!validationAttribute.IsValid(value))
            {
                string[] memberNames = validationContext.MemberName != null ? new string[] { validationContext.MemberName } : null;
                result = new ValidationResult(validationAttribute.FormatErrorMessage(validationContext.DisplayName), memberNames);
            }

            return result;
        }

        public static ValidationResult GetValidationResult(this ValidationAttribute validationAttribute, object value, ValidationContext validationContext)
        {
            if (validationContext == null)
            {
                throw new ArgumentNullException("validationContext");
            }

            ValidationResult result = validationAttribute.IsValid(value, validationContext);

            // If validation fails, we want to ensure we have a ValidationResult that guarantees it has an ErrorMessage
            if (result != null)
            {
                bool hasErrorMessage = (result != null) ? !string.IsNullOrEmpty(result.ErrorMessage) : false;
                if (!hasErrorMessage)
                {
                    string errorMessage = validationAttribute.FormatErrorMessage(validationContext.DisplayName);
                    result = new ValidationResult(errorMessage, result.MemberNames);
                }
            }

            return result;
        }
    }
}
