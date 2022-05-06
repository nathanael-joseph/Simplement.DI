using Simplement.DI.CoreLib.Enums;
using Simplement.DI.CoreLib.Exceptions;

namespace Simplement.DI.CoreLib
{
    public class Container
    {
        private readonly Dictionary<Type, DependancyLifetime> _registeredDependancies;

        private readonly Dictionary<Type, Func<Scope?, object?>> _constructors = new Dictionary<Type, Func<Scope?, object?>>();
        private readonly Dictionary<Type, object> _singletonLocks = new Dictionary<Type, object>();
        private readonly Dictionary<Type, object?> _singletons = new Dictionary<Type, object?>();
        private readonly Dictionary<Scope, Dictionary<Type, object?>> _scopedInstances = new Dictionary<Scope, Dictionary<Type, object?>>();
        
        public IEnumerable<Type> SingletonTypes => _registeredDependancies.Keys
                                            .Where(x => _registeredDependancies[x] == DependancyLifetime.SINGLTON)
                                            .ToArray();
        public IEnumerable<Type> ScopedTypes => _registeredDependancies.Keys
                                            .Where(x => _registeredDependancies[x] == DependancyLifetime.SCOPED)
                                            .ToArray();
        public IEnumerable<Type> TransientTypes => _registeredDependancies.Keys
                                            .Where(x => _registeredDependancies[x] == DependancyLifetime.TRANSIENT)
                                            .ToArray();
        internal Container(Dictionary<Type, DependancyLifetime> registeredDependies, Dictionary<Type, Func<Container, Scope?, object?>> constructors)
        {
            _registeredDependancies = registeredDependies;
            foreach (Type type in constructors.Keys)
            {
                _constructors[type] = (scope) => constructors[type](this, scope);
            }
            
        }

        internal object? Request(Type type, Scope? scope = null)
        {

            if (!_registeredDependancies.TryGetValue(type, out DependancyLifetime lifetime))
            {
                throw new UknownDependancyException(type);
            }

            object? instance;
            
            switch (lifetime)
            {
                case DependancyLifetime.TRANSIENT:
                    instance = _constructors[type](scope);
                    break;
                case DependancyLifetime.SCOPED:
                    if(scope == null)
                    {
                        throw new NotImplementedException();
                    }
                    else if (!_scopedInstances.ContainsKey(scope))
                    {
                        throw new UnknownScopeException(type);
                    }
                    else
                    {
                        var instances = _scopedInstances[scope];
                        lock (scope.GetLock(type))
                        {
                            if (!instances.ContainsKey(type))
                            {
                                instance = _constructors[type](scope);
                                instances[type] = instance;
                            }
                            else
                            {
                                instance = instances[type];
                            }
                        }
                    }
                    break;
                case DependancyLifetime.SINGLTON:
                    
                    lock (_singletonLocks[type])
                    {
                        if (!_singletons.ContainsKey(type))
                        {
                            instance = _constructors[type](scope);
                            _singletons[type] = instance;
                        }
                        else
                        {
                            instance = _singletons[type];
                        }                      
                    }
                    break;
                default: throw new NotImplementedException();
            }

            return instance;
        }
        public T Request<T>()
        {
            return (T)Request(typeof(T));
        }

        public T Request<T>(Scope scope)
        {         
            return (T)Request(typeof(T), scope);
        }

        public Scope CreateScope ()
        {
            List<Type> scopedTypes = _registeredDependancies.Keys
                                            .Where(x => _registeredDependancies[x] == DependancyLifetime.SCOPED)
                                            .ToList();
            return new Scope(this, scopedTypes);
        }

        internal void RemoveScope (Scope scope)
        {
            foreach (var instance in _scopedInstances[scope].Values)
            {
                IDisposable? disposable = instance as IDisposable;
                disposable?.Dispose();
            }
            
            _scopedInstances.Remove(scope);
        }
    }



}