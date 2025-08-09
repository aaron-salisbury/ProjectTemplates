using System;

namespace DotNetFramework.Core.DependencyInjection
{
    /// <summary>
    /// Describes a service with its service type, implementation, and lifetime.
    /// </summary>
    public class ServiceDescriptor : IEquatable<ServiceDescriptor>
    {
        public ServiceLifetime Lifetime { get; set; }
        public Type ServiceType { get; set; }
        public Type ImplementationType { get; set; }
        public object ImplementationInstance { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as ServiceDescriptor);
        }

        public bool Equals(ServiceDescriptor other)
        {
            return other != null && ServiceType == other.ServiceType;
        }

        public override int GetHashCode()
        {
            return ServiceType.GetHashCode();
        }
    }
}
