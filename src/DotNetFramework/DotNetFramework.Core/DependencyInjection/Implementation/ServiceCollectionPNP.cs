using Microsoft.Practices.Unity;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DotNetFramework.Core.DependencyInjection
{
    /// <summary>
    /// Patterns & Practices Unity implementation of IServiceCollection.
    /// </summary>
    public class ServiceCollectionPNP : IServiceCollection
    {
        private readonly List<ServiceDescriptor> _descriptors;

        public ServiceCollectionPNP()
        {
            _descriptors = [];
        }

        public IServiceCollection Add(Type serviceType, Type implementationType, ServiceLifetime lifetime)
        {
            Add(new ServiceDescriptor()
            {
                Lifetime = lifetime,
                ServiceType = serviceType,
                ImplementationType = implementationType,
                ImplementationInstance = null
            });

            return this;
        }

        public IServiceCollection AddInstance(Type serviceType, object implementationInstance, ServiceLifetime lifetime)
        {
            Add(new ServiceDescriptor()
            {
                Lifetime = lifetime,
                ServiceType = serviceType,
                ImplementationType = null,
                ImplementationInstance = implementationInstance
            });

            return this;
        }

        public IServiceProvider BuildServiceProvider()
        {
            IUnityContainer container = new UnityContainer();

            foreach (ServiceDescriptor descriptor in _descriptors)
            {
                LifetimeManager lifetimeManager = LifetimeManagerForServiceLifetime(descriptor.Lifetime);

                if (descriptor.ImplementationInstance != null)
                {
                    container.RegisterInstance(descriptor.ServiceType, descriptor.ImplementationInstance, lifetimeManager);
                }
                else
                {
                    container.RegisterType(descriptor.ServiceType, descriptor.ImplementationType, lifetimeManager);
                }
            }

            return new ServiceProviderPNP(container);
        }

        public IEnumerator<ServiceDescriptor> GetEnumerator()
        {
            return _descriptors.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(ServiceDescriptor item)
        {
            Insert(_descriptors.Count, item);
        }

        public void Clear()
        {
            _descriptors.Clear();
        }

        public bool Contains(ServiceDescriptor item)
        {
            return _descriptors.Contains(item);
        }

        public void CopyTo(ServiceDescriptor[] array, int arrayIndex)
        {
            _descriptors.CopyTo(array, arrayIndex);
        }

        public bool Remove(ServiceDescriptor item)
        {
            return _descriptors.Remove(item);
        }

        public int Count => _descriptors.Count;

        public bool IsReadOnly => false;

        public ServiceDescriptor this[int index]
        {
            get
            {
                return _descriptors[index];
            }
            set
            {
                _descriptors[index] = value;
            }
        }

        public int IndexOf(ServiceDescriptor item)
        {
            return _descriptors.IndexOf(item);
        }

        public void Insert(int index, ServiceDescriptor item)
        {
            // Subsequent attempts to add the same type to replace the previous addition.
            int existingIndex = IndexOf(item);
            if (existingIndex >= 0)
            {
                RemoveAt(existingIndex);

                if (existingIndex < index)
                {
                    --index;
                }
            }

            if (index < 0 || index > _descriptors.Count - 1)
            {
                _descriptors.Add(item);
            }
            else
            {
                _descriptors.Insert(index, item);
            }
        }

        public void RemoveAt(int index)
        {
            _descriptors.RemoveAt(index);
        }

        private static LifetimeManager LifetimeManagerForServiceLifetime(ServiceLifetime lifetime)
        {
            return lifetime switch
            {
                ServiceLifetime.Transient => new TransientLifetimeManager(),
                ServiceLifetime.Scoped => new PerThreadLifetimeManager(),
                ServiceLifetime.Singleton => new ContainerControlledLifetimeManager(),
                _ => new TransientLifetimeManager(),
            };
        }
    }
}
