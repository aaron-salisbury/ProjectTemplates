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
            services.RegisterInstance<IServiceProvider>(this, new ContainerControlledLifetimeManager());

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
    }
}
