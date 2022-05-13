
using Simplement.DI.CoreLib.Enums;

namespace Simplement.DI.CoreLib
{
    internal class DependencyRegistration
    {
        internal Type DependancyType { get; set; }
        internal Type ImplementationType { get; set; }
        internal DependencyLifetime Lifetime { get; }
        internal Func<object?>? Constructor { get; set; }

        public DependencyRegistration(Type dependencyType, 
                                      Type implementationType,
                                      DependencyLifetime lifetime)
        {
            DependancyType = dependencyType;
            ImplementationType = implementationType;
            Lifetime = lifetime;
        }
        
        public DependencyRegistration(Type dependencyType, 
                                      Type implementationType,
                                      DependencyLifetime lifetime,
                                      Func<object?>? constructor)
           : this (dependencyType, implementationType, lifetime)
        {
            Constructor = constructor;
        }
    }
}