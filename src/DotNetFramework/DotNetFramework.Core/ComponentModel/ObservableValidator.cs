using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace DotNetFramework.Core.ComponentModel
{
    public abstract class ObservableValidator : ObservableObject, INotifyDataErrorInfo, IDataErrorInfo
    {
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private readonly Dictionary<string, List<string>> _errorsByPropertyNames = [];

        public void RaisePropertyChangedWithValidation(string propertyName)
        {
            PropertyIsValid(propertyName);
            base.RaisePropertyChanged(propertyName);
        }

        protected void SetErrorsForProperty(string propertyName, List<string> errors)
        {
            List<string> startingErrorsForProperty = _errorsByPropertyNames.ContainsKey(propertyName)
                ? _errorsByPropertyNames[propertyName]
                : [];

            if (errors.Count > 0)
            {
                _errorsByPropertyNames[propertyName] = errors;
            }
            else
            {
                if (_errorsByPropertyNames.ContainsKey(propertyName))
                {
                    _errorsByPropertyNames.Remove(propertyName);
                }
            }

            if (ErrorsChanged != null && startingErrorsForProperty.SequenceEqual(errors))
            {
                ErrorsChanged.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        public bool HasErrors
        {
            get { return _errorsByPropertyNames.Any(kv => kv.Value != null && kv.Value.Count > 0); }
        }

        public List<string> GetErrorsForProperty(string propertyName)
        {
            return _errorsByPropertyNames.ContainsKey(propertyName)
                ? _errorsByPropertyNames[propertyName]
                : [];
        }

        public IEnumerable GetErrors(string propertyName)
        {
            return GetErrorsForProperty(propertyName);
        }

        public string Error 
        { 
            get 
            { 
                IsValid();
                return string.Join(Environment.NewLine, _errorsByPropertyNames.Values.SelectMany(e => e).ToArray());
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

        public virtual bool IsValid()
        {
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(this))
            {
                PropertyIsValid(property.Name);
            }

            return !HasErrors;
        }

        public virtual bool PropertyIsValid(string propertyName)
        {
            //TODO:
            // *** PSEUDO CODE ***
            //List<string> newErrorsForProperty = [];

            //// Foreach validation...
            //if (false) // Property failed a validation.
            //{
            //    newErrorsForProperty.Add("Some validation message.");
            //}

            //SetErrorsForProperty(propertyName, newErrorsForProperty);

            //return newErrorsForProperty.Count == 0;

            throw new NotImplementedException();
        }
    }
}
