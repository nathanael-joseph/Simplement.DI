using Simplement.DI.CoreLib.Enums;

namespace Simplement.DI.CoreLib.Dependencies
{
    internal class SingletonDependency : DependencyBase
    {
        private readonly Lazy<object?> _instance; 
        internal override DependencyLifetime Lifetime => DependencyLifetime.SINGLTON;
        
        internal override object? Instance 
        { 
            get => _instance.Value; 
        }

        internal SingletonDependency(Func<object?> constructor) 
            : base(constructor)
        {
            _instance = new Lazy<object?>(constructor);
        }
    }
}