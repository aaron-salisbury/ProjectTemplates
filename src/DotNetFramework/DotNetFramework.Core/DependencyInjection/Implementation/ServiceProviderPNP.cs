using Microsoft.Practices.Unity;
using System;

namespace DotNetFramework.Core.DependencyInjection
{
    /// <summary>
    /// Patterns & Practices Unity implementation of IServiceProvider.
    /// </summary>
    public class ServiceProviderPNP : IServiceProvider
    {
        private readonly IUnityContainer _container;

        public ServiceProviderPNP()
        {
            _container = new UnityContainer();

            this.AddSingleton<IServiceProvider>(this);
        }

        public IServiceProvider RegisterType(Type serviceType, Type implementationType, ServiceLifetime lifetime)
        {
            _container.RegisterType(serviceType, implementationType, LifetimeManagerForServiceLifetime(lifetime));

            return this;
        }

        public IServiceProvider RegisterInstance(Type serviceType, object implementationInstance, ServiceLifetime lifetime)
        {
            _container.RegisterInstance(serviceType, implementationInstance, LifetimeManagerForServiceLifetime(lifetime));

            return this;
        }

        public object GetService(Type serviceType)
        {
            return _container.Resolve(serviceType);
        }

        public T GetService<T>()
        {
            return _container.Resolve<T>();
        }

        public T GetRequiredService<T>() where T : notnull
        {
            T service = _container.Resolve<T>();

            if (service == null)
            {
                throw new InvalidOperationException("Failed to resolve type from service container.");
            }

            return service;
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
