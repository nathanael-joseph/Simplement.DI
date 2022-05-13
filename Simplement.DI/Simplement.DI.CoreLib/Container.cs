using Simplement.DI.CoreLib.Dependencies;
using Simplement.DI.CoreLib.Enums;
using Simplement.DI.CoreLib.Exceptions;

namespace Simplement.DI.CoreLib
{
    public class Container
    {
        private readonly Dictionary<Type, DependencyBase> _containerDictionary;
        public bool IsScoped { get; private set; }
        internal Container(Dictionary<Type, DependencyBase> containerDictionary)
        {
            _containerDictionary = containerDictionary;
            IsScoped = false;
        }

        internal object? Request(Type type)
        {
            if (!_containerDictionary.ContainsKey(type))
            {
                throw new UknownDependencyException(type);
            }
            
            DependencyBase dependency = _containerDictionary[type];

            if (IsScoped || dependency.Lifetime != DependencyLifetime.SCOPED)
            {
                return dependency.Instance;
            }

            throw new InvalidScopedDependencyRequestException(type);
        }
        public T Request<T>()
        {
            return (T)Request(typeof(T));
        }

    }
}