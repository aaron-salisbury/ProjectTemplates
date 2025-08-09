using System;

namespace DotNetFramework.Core.DependencyInjection
{
    public static class ServiceProviderExtensions
    {
        /// <summary>
        /// Get service of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of service object to get.</typeparam>
        /// <returns>A service object of type <typeparamref name="T"/> or null if there is no such service.</returns>
        public static T GetService<T>(this IServiceProvider serviceProvider)
        {
            return (T)serviceProvider.GetService(typeof(T));
        }

        /// <summary>
        /// Get service of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of service object to get.</typeparam>
        /// <returns>A service object of type <typeparamref name="T"/>.</returns>
        /// <exception cref="System.InvalidOperationException">There is no service of type <typeparamref name="T"/>.</exception>
        public static T GetRequiredService<T>(this IServiceProvider serviceProvider) where T : notnull
        {
            T service = serviceProvider.GetService<T>();

            if (service == null)
            {
                throw new InvalidOperationException($"There is no service of type {typeof(T)}");
            }

            return service;
        }
    }
}
