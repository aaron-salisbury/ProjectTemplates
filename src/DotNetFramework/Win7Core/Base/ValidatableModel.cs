using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Win7Core.Base
{
    public class ValidatableModel : ObservableObject, INotifyDataErrorInfo
    {
        private readonly ConcurrentDictionary<string, List<string>> _errorsByProperty = new ConcurrentDictionary<string, List<string>>();

        public override void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            base.RaisePropertyChanged(propertyName);
            Validate();
        }

        public void RaisePropertyChangedNoValidation(string propertyName)
        {
            base.RaisePropertyChanged(propertyName);
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public void OnErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
            {
                ErrorsChanged.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        public IEnumerable GetErrors(string propertyName)
        {
            List<string> errorsForName;
            _errorsByProperty.TryGetValue(propertyName, out errorsForName);
            return errorsForName;
        }

        public bool HasErrors
        {
            get { return _errorsByProperty.Any(kv => kv.Value != null && kv.Value.Count > 0); }
        }

        public void AddError(string propertyName, string errorMsg)
        {
            if (_errorsByProperty.ContainsKey(propertyName))
            {
                if (!_errorsByProperty[propertyName].Contains(errorMsg))
                {
                    _errorsByProperty[propertyName].Add(errorMsg);
                }
            }
            else
            {
                _errorsByProperty.TryAdd(propertyName, new List<string>() { errorMsg });
            }
            OnErrorsChanged(propertyName);
        }

        public void ClearErrors(string propertyName)
        {
            List<string> removedErrors;
            _errorsByProperty.TryRemove(propertyName, out removedErrors);
            OnErrorsChanged(propertyName);
        }

        /// <returns>True if model is valid.</returns>
        public bool Validate()
        {
            ValidationContext validationContext = new ValidationContext(this, null, null);
            List<ValidationResult> validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(this, validationContext, validationResults, true);

            foreach (KeyValuePair<string, List<string>> error in _errorsByProperty.ToList())
            {
                if (validationResults.All(r => r.MemberNames.All(m => m != error.Key)))
                {
                    List<string> outs;
                    _errorsByProperty.TryRemove(error.Key, out outs);
                    OnErrorsChanged(error.Key);
                }
            }

            var properties = from result in validationResults
                             from memberName in result.MemberNames
                             group result by memberName into resultByNameGroup
                             select resultByNameGroup;

            foreach (var prop in properties)
            {
                var messages = prop.Select(r => r.ErrorMessage).ToList();

                if (_errorsByProperty.ContainsKey(prop.Key))
                {
                    List<string> outs;
                    _errorsByProperty.TryRemove(prop.Key, out outs);
                }

                _errorsByProperty.TryAdd(prop.Key, messages);
                OnErrorsChanged(prop.Key);
            }

            return !HasErrors;
        }

        public bool Validate(string propertyName)
        {
            ValidationContext validationContext = new ValidationContext(this, null, null) { MemberName = propertyName };
            List<ValidationResult> validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(GetType().GetProperty(propertyName).GetValue(this), validationContext, validationResults);
            bool newErrorsWillBeAdded = false;
            List<string> existingErrorsForProperty;
            bool retreivedExistingList = _errorsByProperty.TryGetValue(propertyName, out existingErrorsForProperty);
            List<string> errorMessages = new List<string>();

            byte newErrorsCount = 0;

            if (existingErrorsForProperty == null)
            {
                existingErrorsForProperty = new List<string>();
            }

            foreach (ValidationResult validationResult in validationResults)
            {
                if (!newErrorsWillBeAdded && !existingErrorsForProperty.Contains(validationResult.ErrorMessage))
                {
                    newErrorsWillBeAdded = true;
                    newErrorsCount++;
                }

                errorMessages.Add(validationResult.ErrorMessage);
            }

            // If error list changed, update list and call OnErrorsChanged()
            if (!errorMessages.Any())
            {
                if (existingErrorsForProperty.Any())
                {
                    List<string> outs;
                    _errorsByProperty.TryRemove(propertyName, out outs);
                    OnErrorsChanged(propertyName);
                }
            }
            else if (existingErrorsForProperty.Count > errorMessages.Count - newErrorsCount)
            {
                _errorsByProperty.TryUpdate(propertyName, errorMessages, existingErrorsForProperty);
                OnErrorsChanged(propertyName);
            }
            else if (newErrorsCount > 0)
            {
                _errorsByProperty.AddOrUpdate(propertyName, errorMessages, (key, value) => errorMessages);
                OnErrorsChanged(propertyName);
            }

            return !_errorsByProperty.ContainsKey(propertyName);
        }
    }
}
