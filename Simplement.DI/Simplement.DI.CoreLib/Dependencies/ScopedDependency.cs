using Simplement.DI.CoreLib.Enums;

namespace Simplement.DI.CoreLib.Dependencies
{
    internal class ScopedDependency : SingletonDependency
    {
        internal override DependencyLifetime Lifetime => DependencyLifetime.SCOPED;
        
        internal ScopedDependency(Func<object?> constructor) 
            : base(constructor)
        {
        }
    }
}