using System;
using System.Globalization;
using System.Reflection;

namespace WinXPCore.Base.DataAnnotationValidation
{
    // https://referencesource.microsoft.com/#System.ComponentModel.DataAnnotations/DataAnnotations/LocalizableString.cs
    internal class LocalizableString
    {
        private string _propertyName;
        private string _propertyValue;
        private Type _resourceType;

        private Func<string> _cachedResult;

        public LocalizableString(string propertyName)
        {
            this._propertyName = propertyName;
        }

        public string Value
        {
            get
            {
                return this._propertyValue;
            }
            set
            {
                if (this._propertyValue != value)
                {
                    this.ClearCache();
                    this._propertyValue = value;
                }
            }
        }

        public Type ResourceType
        {
            get
            {
                return this._resourceType;
            }
            set
            {
                if (this._resourceType != value)
                {
                    this.ClearCache();
                    this._resourceType = value;
                }
            }
        }

        private void ClearCache()
        {
            this._cachedResult = null;
        }

        public string GetLocalizableValue()
        {
            if (this._cachedResult == null)
            {
                // If the property value is null, then just cache that value
                // If the resource type is null, then property value is literal, so cache it
                if (this._propertyValue == null || this._resourceType == null)
                {
                    this._cachedResult = () => this._propertyValue;
                }
                else
                {
                    // Get the property from the resource type for this resource key
                    PropertyInfo property = this._resourceType.GetProperty(this._propertyValue);

                    // We need to detect bad configurations so that we can throw exceptions accordingly
                    bool badlyConfigured = false;

                    // Make sure we found the property and it's the correct type, and that the type itself is public
                    if (!this._resourceType.IsVisible || property == null || property.PropertyType != typeof(string))
                    {
                        badlyConfigured = true;
                    }
                    else
                    {
                        // Ensure the getter for the property is available as public static
                        MethodInfo getter = property.GetGetMethod();

                        if (getter == null || !(getter.IsPublic && getter.IsStatic))
                        {
                            badlyConfigured = true;
                        }
                    }

                    // If the property is not configured properly, then throw a missing member exception
                    if (badlyConfigured)
                    {
                        string exceptionMessage = String.Format(CultureInfo.CurrentCulture,
                            DataAnnotationsResources.LocalizableString_LocalizationFailed,
                            this._propertyName, this._resourceType.FullName, this._propertyValue);
                        this._cachedResult = () => { throw new InvalidOperationException(exceptionMessage); };
                    }
                    else
                    {
                        // We have a valid property, so cache the resource
                        this._cachedResult = () => (string)property.GetValue(null, null);
                    }
                }
            }

            // Return the cached result
            return this._cachedResult();
        }
    }
}
