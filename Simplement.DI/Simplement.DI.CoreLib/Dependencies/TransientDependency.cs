using Simplement.DI.CoreLib.Enums;

namespace Simplement.DI.CoreLib.Dependencies
{
    internal class TransientDependency : DependencyBase
    {
        internal override DependencyLifetime Lifetime => DependencyLifetime.TRANSIENT;
        
        protected internal override object? Instance 
        { 
            get => Constructor(); 
        }

        internal TransientDependency(Func<object?> constructor) 
            : base(constructor)
        {
        }
    }
}