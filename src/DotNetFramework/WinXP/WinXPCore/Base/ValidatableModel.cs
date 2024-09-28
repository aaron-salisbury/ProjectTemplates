using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using WinXPCore.Base.DataAnnotationValidation;
using WinXPCore.Base.Logging;

namespace WinXPCore.Base
{
    // http://www.reza-aghaei.com/dataannotations-validation-attributes-in-windows-forms/

    public class ValidatableModel : LoggableObject, IDataErrorInfo
    {
        [Browsable(false)]
        public string this[string property]
        {
            get
            {
                var propertyDescriptor = TypeDescriptor.GetProperties(this)[property];
                if (propertyDescriptor == null)
                    return string.Empty;

                var results = new List<ValidationResult>();
                var result = Validator.TryValidateProperty(
                                          propertyDescriptor.GetValue(this),
                                          new ValidationContext(this, null, null)
                                          { MemberName = property },
                                          results);
                if (!result)
                    return string.Join("\n", results.Select(x => x.ErrorMessage).ToArray());
                return string.Empty;
            }
        }

        [Browsable(false)]
        public string Error
        {
            get
            {
                var results = new List<ValidationResult>();
                var result = Validator.TryValidateObject(this,
                    new ValidationContext(this, null, null), results, true);
                if (!result)
                    return string.Join("\n", results.Select(x => x.ErrorMessage).ToArray());
                else
                    return null;
            }
        }
    }
}
