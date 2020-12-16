using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WinXPCore.Base.DataAnnotationValidation
{
    // https://referencesource.microsoft.com/#System.ComponentModel.DataAnnotations/DataAnnotations/ValidationAttributeStore.cs
    internal class ValidationAttributeStore
    {
        private static ValidationAttributeStore _singleton = new ValidationAttributeStore();
        private Dictionary<Type, TypeStoreItem> _typeStoreItems = new Dictionary<Type, TypeStoreItem>();

        internal static ValidationAttributeStore Instance
        {
            get
            {
                return _singleton;
            }
        }

        internal IEnumerable<ValidationAttribute> GetTypeValidationAttributes(ValidationContext validationContext)
        {
            EnsureValidationContext(validationContext);
            TypeStoreItem item = this.GetTypeStoreItem(validationContext.ObjectType);
            return item.ValidationAttributes;
        }

        internal DisplayAttribute GetTypeDisplayAttribute(ValidationContext validationContext)
        {
            EnsureValidationContext(validationContext);
            TypeStoreItem item = this.GetTypeStoreItem(validationContext.ObjectType);
            return item.DisplayAttribute;
        }

        internal IEnumerable<ValidationAttribute> GetPropertyValidationAttributes(ValidationContext validationContext)
        {
            EnsureValidationContext(validationContext);
            TypeStoreItem typeItem = this.GetTypeStoreItem(validationContext.ObjectType);
            PropertyStoreItem item = typeItem.GetPropertyStoreItem(validationContext.MemberName);
            return item.ValidationAttributes;
        }

        internal DisplayAttribute GetPropertyDisplayAttribute(ValidationContext validationContext)
        {
            EnsureValidationContext(validationContext);
            TypeStoreItem typeItem = this.GetTypeStoreItem(validationContext.ObjectType);
            PropertyStoreItem item = typeItem.GetPropertyStoreItem(validationContext.MemberName);
            return item.DisplayAttribute;
        }

        internal Type GetPropertyType(ValidationContext validationContext)
        {
            EnsureValidationContext(validationContext);
            TypeStoreItem typeItem = this.GetTypeStoreItem(validationContext.ObjectType);
            PropertyStoreItem item = typeItem.GetPropertyStoreItem(validationContext.MemberName);
            return item.PropertyType;
        }

        internal bool IsPropertyContext(ValidationContext validationContext)
        {
            EnsureValidationContext(validationContext);
            TypeStoreItem typeItem = this.GetTypeStoreItem(validationContext.ObjectType);
            PropertyStoreItem item = null;
            return typeItem.TryGetPropertyStoreItem(validationContext.MemberName, out item);
        }

        private TypeStoreItem GetTypeStoreItem(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            lock (this._typeStoreItems)
            {
                TypeStoreItem item = null;
                if (!this._typeStoreItems.TryGetValue(type, out item))
                {
                    IEnumerable<Attribute> attributes =

                    System.ComponentModel.TypeDescriptor.GetAttributes(type).Cast<Attribute>();
                    item = new TypeStoreItem(type, attributes);
                    this._typeStoreItems[type] = item;
                }
                return item;
            }
        }

        private static void EnsureValidationContext(ValidationContext validationContext)
        {
            if (validationContext == null)
            {
                throw new ArgumentNullException("validationContext");
            }
        }

        private abstract class StoreItem
        {
            private static IEnumerable<ValidationAttribute> _emptyValidationAttributeEnumerable = new ValidationAttribute[0];

            private IEnumerable<ValidationAttribute> _validationAttributes;

            internal StoreItem(IEnumerable<Attribute> attributes)
            {
                this._validationAttributes = attributes.OfType<ValidationAttribute>();
                this.DisplayAttribute = attributes.OfType<DisplayAttribute>().SingleOrDefault();
            }

            internal IEnumerable<ValidationAttribute> ValidationAttributes
            {
                get
                {
                    return this._validationAttributes;
                }
            }

            internal DisplayAttribute DisplayAttribute { get; set; }
        }

        private class TypeStoreItem : StoreItem
        {
            private object _syncRoot = new object();
            private Type _type;
            private Dictionary<string, PropertyStoreItem> _propertyStoreItems;

            internal TypeStoreItem(Type type, IEnumerable<Attribute> attributes)
                : base(attributes)
            {
                this._type = type;
            }

            internal PropertyStoreItem GetPropertyStoreItem(string propertyName)
            {
                PropertyStoreItem item = null;
                if (!this.TryGetPropertyStoreItem(propertyName, out item))
                {
                    throw new ArgumentException(String.Format(System.Globalization.CultureInfo.CurrentCulture, DataAnnotationsResources.AttributeStore_Unknown_Property, this._type.Name, propertyName), "propertyName");
                }
                return item;
            }

            internal bool TryGetPropertyStoreItem(string propertyName, out PropertyStoreItem item)
            {
                if (string.IsNullOrEmpty(propertyName))
                {
                    throw new ArgumentNullException("propertyName");
                }

                if (this._propertyStoreItems == null)
                {
                    lock (this._syncRoot)
                    {
                        if (this._propertyStoreItems == null)
                        {
                            this._propertyStoreItems = this.CreatePropertyStoreItems();
                        }
                    }
                }
                if (!this._propertyStoreItems.TryGetValue(propertyName, out item))
                {
                    return false;
                }
                return true;
            }

            private Dictionary<string, PropertyStoreItem> CreatePropertyStoreItems()
            {
                Dictionary<string, PropertyStoreItem> propertyStoreItems = new Dictionary<string, PropertyStoreItem>();

                System.ComponentModel.PropertyDescriptorCollection properties = System.ComponentModel.TypeDescriptor.GetProperties(this._type);
                foreach (System.ComponentModel.PropertyDescriptor property in properties)
                {
                    PropertyStoreItem item = new PropertyStoreItem(property.PropertyType, GetExplicitAttributes(property).Cast<Attribute>());
                    propertyStoreItems[property.Name] = item;
                }

                return propertyStoreItems;
            }

            public static System.ComponentModel.AttributeCollection GetExplicitAttributes(System.ComponentModel.PropertyDescriptor propertyDescriptor)
            {
                List<Attribute> attributes = new List<Attribute>(propertyDescriptor.Attributes.Cast<Attribute>());
                IEnumerable<Attribute> typeAttributes = System.ComponentModel.TypeDescriptor.GetAttributes(propertyDescriptor.PropertyType).Cast<Attribute>();
                bool removedAttribute = false;
                foreach (Attribute attr in typeAttributes)
                {
                    for (int i = attributes.Count - 1; i >= 0; --i)
                    {
                        // We must use ReferenceEquals since attributes could Match if they are the same.
                        // Only ReferenceEquals will catch actual duplications.
                        if (object.ReferenceEquals(attr, attributes[i]))
                        {
                            attributes.RemoveAt(i);
                            removedAttribute = true;
                        }
                    }
                }
                return removedAttribute ? new System.ComponentModel.AttributeCollection(attributes.ToArray()) : propertyDescriptor.Attributes;
            }
        }

        private class PropertyStoreItem : StoreItem
        {
            private Type _propertyType;

            internal PropertyStoreItem(Type propertyType, IEnumerable<Attribute> attributes)
                : base(attributes)
            {
                this._propertyType = propertyType;
            }

            internal Type PropertyType
            {
                get
                {
                    return this._propertyType;
                }
            }
        }
    }
}
