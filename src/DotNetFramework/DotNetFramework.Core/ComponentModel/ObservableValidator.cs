using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace DotNetFramework.Core.ComponentModel
{
    // WinForms needs an ErrorProvider to be created and passed the control and its message via .UpdateError().
    // WPF uses IDataErrorInfo when ValidatesOnDataErrors is set to true and the REAL INotifyDataErrorInfo when ValidatesOnNotifyDataErrors is set to true.
    // ASP.NET MVC uses a model binder (the DefaultModelBinder) to detect whether or not a class implements the IDataErrorInfo interface.
    public abstract class ObservableValidator : ObservableObject, IDataErrorInfo, INotifyDataErrorInfo
    {
        protected readonly List<string> EntityLevelErrors = []; // Non-property specific validation errors.

        protected readonly Dictionary<string, List<string>> ErrorsByPropertyNames = [];

        public void RaisePropertyChangedWithValidation(string propertyName)
        {
            PropertyIsValid(propertyName);
            base.RaisePropertyChanged(propertyName);
        }

        protected void SetErrorsForProperty(string propertyName, List<string> errors)
        {
            List<string> startingErrorsForProperty = ErrorsByPropertyNames.ContainsKey(propertyName)
                ? ErrorsByPropertyNames[propertyName]
                : [];

            if (errors.Count > 0)
            {
                ErrorsByPropertyNames[propertyName] = errors;
            }
            else
            {
                if (ErrorsByPropertyNames.ContainsKey(propertyName))
                {
                    ErrorsByPropertyNames.Remove(propertyName);
                }
            }

            if (ErrorsChanged != null && startingErrorsForProperty.SequenceEqual(errors))
            {
                ErrorsChanged.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        public List<string> GetErrorsForProperty(string propertyName)
        {
            return ErrorsByPropertyNames.ContainsKey(propertyName)
                ? ErrorsByPropertyNames[propertyName]
                : [];
        }

        #region IDataErrorInfo Members
        /// <summary>
        /// This property is intended to represent non-property specific validation errors associated with the class.
        /// For example, you might want to enforce a validation rule that depends on the values of multiple properties.
        /// In that case, you would return a validation error from the Error property.
        /// </summary>
        public string Error
        {
            get
            {
                if (EntityLevelErrors.Count == 0)
                {
                    return string.Empty;
                }
                else if (EntityLevelErrors.Count == 1)
                {
                    return EntityLevelErrors.First();
                }

                return string.Join(Environment.NewLine, [.. EntityLevelErrors]);
            }
        }

        public string this[string columnName]
        {
            get
            {
                PropertyIsValid(columnName);
                return string.Join(Environment.NewLine, [.. GetErrorsForProperty(columnName)]);
            }
        }
        #endregion

        #region INotifyDataErrorInfo Members
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            return GetErrorsForProperty(propertyName);
        }

        public bool HasErrors
        {
            get { return ErrorsByPropertyNames.Any(kv => kv.Value != null && kv.Value.Count > 0); }
        }
        #endregion

        public bool IsValid()
        {
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(this))
            {
                ValidateProperty(property);
            }

            return !HasErrors;
        }

        public bool PropertyIsValid(string propertyName)
        {
            PropertyDescriptor propertyDescriptor = null;

            if (!string.IsNullOrEmpty(propertyName))
            {
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(this))
                {
                    if (string.Equals(propertyName, property.Name))
                    {
                        propertyDescriptor = property;
                    }
                }
            }

            if (propertyDescriptor == null)
            {
                throw new ArgumentException("Property doesn't exist to validate for the name given.");
            }

            List<string> errors = ValidateProperty(propertyDescriptor);
            SetErrorsForProperty(propertyName, errors); // This must be called to keep the errors collection correct and to trigger the ErrorsChanged event as needed.

            return errors.Count == 0;
        }

        public virtual List<string> ValidateProperty(PropertyDescriptor property)
        {
            // *** PSEUDO CODE ***
            //List<string> errorsForProperty = [];

            //// Foreach validation...
            //if (false) // Property failed a validation.
            //{
            //    errorsForProperty.Add("Some validation message.");
            //}

            //return errorsForProperty;

            throw new NotImplementedException();
        }
    }
}
