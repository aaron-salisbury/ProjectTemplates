using Microsoft.Practices.Unity;
using System;

namespace DotNetFramework.Core.DependencyInjection
{
    /// <summary>
    /// Patterns & Practices Unity implementation of IServiceProvider.
    /// </summary>
    public class ServiceProviderPNP : IServiceProvider, IDisposable
    {
        private readonly IUnityContainer _unityProvider;

        public ServiceProviderPNP(IUnityContainer services)
        {
            _unityProvider = services;
        }

        public object GetService(Type serviceType)
        {
            return _unityProvider.Resolve(serviceType);
        }

        public T GetService<T>()
        {
            return _unityProvider.Resolve<T>();
        }

        public T GetRequiredService<T>() where T : notnull
        {
            T service = _unityProvider.Resolve<T>();

            if (service == null)
            {
                throw new InvalidOperationException("Failed to resolve type from service container.");
            }

            return service;
        }

        public void Dispose()
        {
            _unityProvider.Dispose();
        }

        internal static LifetimeManager LifetimeManagerForServiceLifetime(ServiceLifetime lifetime)
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
