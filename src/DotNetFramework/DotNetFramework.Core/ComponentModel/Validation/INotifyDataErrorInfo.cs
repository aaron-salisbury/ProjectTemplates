using System;
using System.Collections;

namespace DotNetFramework.Core.ComponentModel
{
    public interface INotifyDataErrorInfo
    {
        bool HasErrors { get; }

        event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        IEnumerable GetErrors(string propertyName);
    }
}
