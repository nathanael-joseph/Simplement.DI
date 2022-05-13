using Simplement.DI.CoreLib.Dependencies;
using Simplement.DI.CoreLib.Enums;
using Simplement.DI.CoreLib.Exceptions;

namespace Simplement.DI.CoreLib
{
    public class Container : IDisposable
    {
        private readonly Dictionary<Type, DependencyBase> _containerDictionary;
        private bool _disposed;
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

        public Container CreateScope()
        {
            Dictionary<Type, DependencyBase> containerDictionary = new Dictionary<Type, DependencyBase>(_containerDictionary.Count);
            
            foreach(var kvp in _containerDictionary)
            {
                if (kvp.Value.Lifetime == DependencyLifetime.SCOPED)
                {
                    containerDictionary.Add(kvp.Key, new ScopedDependency(kvp.Value.Constructor));
                }
                else
                {
                    containerDictionary.Add(kvp.Key, kvp.Value);
                }
            }

            Container scopedContainer = new Container(containerDictionary);
            scopedContainer.IsScoped = true;

            return scopedContainer;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    foreach(DependencyBase dependency in _containerDictionary.Values)
                    {
                        
                        if(!IsScoped || dependency.Lifetime == DependencyLifetime.SCOPED)
                        {
                            IDisposable? disposable = dependency as IDisposable;
                            disposable?.Dispose();
                        }
                    }
                }

                _disposed = true;
            }
        }
    }
}