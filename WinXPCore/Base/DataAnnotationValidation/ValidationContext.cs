using System;
using System.Collections.Generic;
using System.Globalization;

namespace WinXPCore.Base.DataAnnotationValidation
{
    // https://referencesource.microsoft.com/#System.ComponentModel.DataAnnotations/DataAnnotations/ValidationContext.cs
    public sealed class ValidationContext : IServiceProvider
    {
        private Func<Type, object> _serviceProvider;
        private object _objectInstance;
        private string _memberName;
        private string _displayName;
        private Dictionary<object, object> _items;

        public ValidationContext(object instance)
            : this(instance, null, null)
        {
        }

        public ValidationContext(object instance, IDictionary<object, object> items)
            : this(instance, null, items)
        {
        }

        public ValidationContext(object instance, IServiceProvider serviceProvider, IDictionary<object, object> items)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            if (serviceProvider != null)
            {
                this.InitializeServiceProvider(serviceType => serviceProvider.GetService(serviceType));
            }

            System.ComponentModel.Design.IServiceContainer container = serviceProvider as System.ComponentModel.Design.IServiceContainer;

            if (container != null)
            {
                this._serviceContainer = new ValidationContextServiceContainer(container);
            }
            else
            {
                this._serviceContainer = new ValidationContextServiceContainer();
            }

            if (items != null)
            {
                this._items = new Dictionary<object, object>(items);
            }
            else
            {
                this._items = new Dictionary<object, object>();
            }

            this._objectInstance = instance;
        }

        public object ObjectInstance
        {
            get
            {
                return this._objectInstance;
            }
        }

        public Type ObjectType
        {
            get
            {
                return this.ObjectInstance.GetType();
            }
        }

        public string DisplayName
        {
            get
            {
                if (string.IsNullOrEmpty(this._displayName))
                {
                    this._displayName = this.GetDisplayName();

                    if (string.IsNullOrEmpty(this._displayName))
                    {
                        this._displayName = this.MemberName;

                        if (string.IsNullOrEmpty(this._displayName))
                        {
                            this._displayName = this.ObjectType.Name;
                        }
                    }
                }
                return this._displayName;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("value");
                }
                this._displayName = value;
            }
        }

        public string MemberName
        {
            get
            {
                return this._memberName;
            }
            set
            {
                this._memberName = value;
            }
        }

        public IDictionary<object, object> Items
        {
            get
            {
                return this._items;
            }
        }

        private string GetDisplayName()
        {
            string displayName = null;
            ValidationAttributeStore store = ValidationAttributeStore.Instance;
            DisplayAttribute displayAttribute = null;

            if (string.IsNullOrEmpty(this._memberName))
            {
                displayAttribute = store.GetTypeDisplayAttribute(this);
            }
            else if (store.IsPropertyContext(this))
            {
                displayAttribute = store.GetPropertyDisplayAttribute(this);
            }

            if (displayAttribute != null)
            {
                displayName = displayAttribute.GetName();
            }

            return displayName ?? this.MemberName;
        }

        public void InitializeServiceProvider(Func<Type, object> serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public object GetService(Type serviceType)
        {
            object service = null;

            if (this._serviceContainer != null)
            {
                service = this._serviceContainer.GetService(serviceType);
            }

            if (service == null && this._serviceProvider != null)
            {
                service = this._serviceProvider(serviceType);
            }

            return service;
        }

        private System.ComponentModel.Design.IServiceContainer _serviceContainer;

        public System.ComponentModel.Design.IServiceContainer ServiceContainer
        {
            get
            {
                if (this._serviceContainer == null)
                {
                    this._serviceContainer = new ValidationContextServiceContainer();
                }

                return this._serviceContainer;
            }
        }

        private class ValidationContextServiceContainer : System.ComponentModel.Design.IServiceContainer
        {
            private System.ComponentModel.Design.IServiceContainer _parentContainer;
            private Dictionary<Type, object> _services = new Dictionary<Type, object>();
            private readonly object _lock = new object();

            internal ValidationContextServiceContainer()
            {
            }

            internal ValidationContextServiceContainer(System.ComponentModel.Design.IServiceContainer parentContainer)
            {
                this._parentContainer = parentContainer;
            }

            public void AddService(Type serviceType, System.ComponentModel.Design.ServiceCreatorCallback callback, bool promote)
            {
                if (promote && this._parentContainer != null)
                {
                    this._parentContainer.AddService(serviceType, callback, promote);
                }
                else
                {
                    lock (this._lock)
                    {
                        if (this._services.ContainsKey(serviceType))
                        {
                            throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, DataAnnotationsResources.ValidationContextServiceContainer_ItemAlreadyExists, serviceType), "serviceType");
                        }

                        this._services.Add(serviceType, callback);
                    }
                }
            }

            public void AddService(Type serviceType, System.ComponentModel.Design.ServiceCreatorCallback callback)
            {
                this.AddService(serviceType, callback, true);
            }

            public void AddService(Type serviceType, object serviceInstance, bool promote)
            {
                if (promote && this._parentContainer != null)
                {
                    this._parentContainer.AddService(serviceType, serviceInstance, promote);
                }
                else
                {
                    lock (this._lock)
                    {
                        if (this._services.ContainsKey(serviceType))
                        {
                            throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, DataAnnotationsResources.ValidationContextServiceContainer_ItemAlreadyExists, serviceType), "serviceType");
                        }

                        this._services.Add(serviceType, serviceInstance);
                    }
                }
            }

            public void AddService(Type serviceType, object serviceInstance)
            {
                this.AddService(serviceType, serviceInstance, true);
            }

            public void RemoveService(Type serviceType, bool promote)
            {
                lock (this._lock)
                {
                    if (this._services.ContainsKey(serviceType))
                    {
                        this._services.Remove(serviceType);
                    }
                }

                if (promote && this._parentContainer != null)
                {
                    this._parentContainer.RemoveService(serviceType);
                }
            }

            public void RemoveService(Type serviceType)
            {
                this.RemoveService(serviceType, true);
            }

            public object GetService(Type serviceType)
            {
                if (serviceType == null)
                {
                    throw new ArgumentNullException("serviceType");
                }

                object service = null;
                this._services.TryGetValue(serviceType, out service);

                if (service == null && this._parentContainer != null)
                {
                    service = this._parentContainer.GetService(serviceType);
                }

                System.ComponentModel.Design.ServiceCreatorCallback callback = service as System.ComponentModel.Design.ServiceCreatorCallback;

                if (callback != null)
                {
                    service = callback(this, serviceType);
                }

                return service;
            }
        }
    }
}
