using Microsoft.Practices.Unity;
using System;

namespace DotNetFramework.Core.DependencyInjection
{
    /// <summary>
    /// Patterns & Practices Unity implementation of IServiceCollection.
    /// </summary>
    public class ServiceCollectionPNP : IServiceCollection
    {
        private readonly IUnityContainer _unityCollection;

        public ServiceCollectionPNP()
        {
            _unityCollection = new UnityContainer();
        }

        public IServiceCollection Add(Type serviceType, Type implementationType, ServiceLifetime lifetime)
        {
            LifetimeManager lifetimeManager = ServiceProviderPNP.LifetimeManagerForServiceLifetime(lifetime);

            _unityCollection.RegisterType(serviceType, implementationType, lifetimeManager);

            return this;
        }

        public IServiceCollection AddInstance(Type serviceType, object implementationInstance, ServiceLifetime lifetime)
        {
            LifetimeManager lifetimeManager = ServiceProviderPNP.LifetimeManagerForServiceLifetime(lifetime);

            _unityCollection.RegisterInstance(serviceType, implementationInstance, lifetimeManager);

            return this;
        }

        public IServiceProvider BuildServiceProvider()
        {
            return new ServiceProviderPNP(_unityCollection);
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
