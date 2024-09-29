using System;

namespace DotNetFramework.Core.DependencyInjection
{
    public interface IServiceProvider
    {
        /// <summary>
        /// For the given lifetime, adds a service of the type specified in <paramref name="serviceType"/> with an
        /// implementation of the type specified in <paramref name="implementationType"/>.
        /// </summary>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="implementationType">The implementation type of the service.</param>
        /// <param name="lifetime">The lifetime of the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        IServiceProvider RegisterType(Type serviceType, Type implementationType, ServiceLifetime lifetime);

        /// <summary>
        /// For the given lifetime and instance specified, adds a service of the type specified in <paramref name="serviceType"/>.
        /// </summary>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="implementationInstance">The instance of the service.</param>
        /// <param name="lifetime">The lifetime of the service.</param>
        /// <returns></returns>
        IServiceProvider RegisterInstance(Type serviceType, object implementationInstance, ServiceLifetime lifetime);

        /// <summary>
        /// Gets the service object of the specified type.
        /// </summary>
        /// <param name="serviceType">An object that specifies the type of service object to get.</param>
        /// <returns>A service object of type serviceType. -or- null if there is no service object of type serviceType.</returns>
        object GetService(Type serviceType);

        /// <summary>
        /// Get service of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of service object to get.</typeparam>
        /// <returns>A service object of type <typeparamref name="T"/> or null if there is no such service.</returns>
        T GetService<T>();

        /// <summary>
        /// Get service of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of service object to get.</typeparam>
        /// <returns>A service object of type <typeparamref name="T"/>.</returns>
        /// <exception cref="System.InvalidOperationException">There is no service of type <typeparamref name="T"/>.</exception>
        T GetRequiredService<T>() where T : notnull;
    }
}
