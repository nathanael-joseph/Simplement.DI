using Simplement.DI.CoreLib.Enums;

namespace Simplement.DI.CoreLib.Dependencies
{
    internal abstract class DependencyBase
    {
        internal abstract DependencyLifetime Lifetime { get; }
        internal abstract object? Instance { get; }
        internal Func<object?> Constructor { get; private set; }

        internal DependencyBase(Func<object?> constructor) 
        {
            Constructor = constructor;
        }
    }
}