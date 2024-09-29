using System;
using System.Collections;
using System.Collections.Generic;

namespace DotNetFramework.Core.DependencyInjection
{
    public interface IServiceCollection
    {
        /// <summary>
        /// For the given lifetime, adds a service of the type specified in <paramref name="serviceType"/> with an
        /// implementation of the type specified in <paramref name="implementationType"/>.
        /// </summary>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="implementationType">The implementation type of the service.</param>
        /// <param name="lifetime">The lifetime of the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        IServiceCollection Add(Type serviceType, Type implementationType, ServiceLifetime lifetime);

        /// <summary>
        /// For the given lifetime and instance specified, adds a service of the type specified in <paramref name="serviceType"/>.
        /// </summary>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="implementationInstance">The instance of the service.</param>
        /// <param name="lifetime">The lifetime of the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        IServiceCollection AddInstance(Type serviceType, object implementationInstance, ServiceLifetime lifetime);

        /// <summary>
        /// Creates a <see cref="IServiceProvider"/> containing services from the provided <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> containing services.</param>
        /// <returns>The <see cref="IServiceProvider"/>.</returns>
        IServiceProvider BuildServiceProvider();
    }
}
