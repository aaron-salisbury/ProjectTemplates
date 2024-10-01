using System;

namespace DotNetFramework.Core.DependencyInjection
{
    public interface IServiceProvider : System.IServiceProvider
    {
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
