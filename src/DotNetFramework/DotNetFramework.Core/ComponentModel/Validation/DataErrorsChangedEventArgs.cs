using System;

namespace DotNetFramework.Core.ComponentModel
{
    public class DataErrorsChangedEventArgs : EventArgs
    {
        private readonly string _propertyName;
        public virtual string PropertyName
        { 
            get { return _propertyName; }
        }

        public DataErrorsChangedEventArgs(string propertyName)
        { 
            _propertyName = propertyName;
        }
    }
}
