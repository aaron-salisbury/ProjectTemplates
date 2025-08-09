using Microsoft.Practices.Unity;
using System;

namespace DotNetFramework.Core.DependencyInjection
{
    /// <summary>
    /// Patterns & Practices Unity implementation of IServiceProvider.
    /// </summary>
    public class ServiceProviderPNP : IServiceProvider
    {
        private readonly IUnityContainer _unityProvider;

        public ServiceProviderPNP(IUnityContainer services)
        {
            services.RegisterInstance<IServiceProvider>(this);
            //TODO: Register a IServiceScopeFactory that creates a IServiceScope, which has this IServiceProvider and is disposable.
            //      Which once dispose is called, any scoped services that have been resolved will be disposed.

            _unityProvider = services;
        }

        public object GetService(Type serviceType)
        {
            return _unityProvider.Resolve(serviceType);
        }
    }
}
