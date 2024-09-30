using System.Collections.Generic;
using System.ComponentModel;

namespace DotNetFramework.Core.ComponentModel
{
    public abstract class ObservableValidator : ObservableObject, IDataErrorInfo
    {
        private string _error;
        public string Error
        {
            get { return _error; }
            protected set { _error = value; }
        }

        public string this[string columnName]
        {
            get
            {
                if (Errors != null && Errors.ContainsKey(columnName))
                {
                    return Errors[columnName];
                }

                return string.Empty;
            }
        }

        protected Dictionary<string, string> Errors { get; set; } = [];
    }
}
