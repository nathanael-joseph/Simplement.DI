using Simplement.DI.CoreLib.Enums;
using Simplement.DI.CoreLib.Exceptions;

namespace Simplement.DI.CoreLib
{
    public class Container
    {
        private readonly Dictionary<Type, DependancyLifetime> _registeredDependancies;
        private readonly Dictionary<Type, Func<object>> _constructors;

        private readonly Dictionary<Type, object> _singletonLocks = new Dictionary<Type, object>();
        private readonly Dictionary<Type, object> _singletons = new Dictionary<Type, object>();
        private readonly Dictionary<Scope, Dictionary<Type, object>> _scopedInstances = new Dictionary<Scope, Dictionary<Type, object>>();
        
        internal Container(Dictionary<Type, DependancyLifetime> registeredDependies, Dictionary<Type, Func<object>> constructors)
        {
            _registeredDependancies = registeredDependies;
            _constructors = constructors;
        }

        internal object Request(Type type, Scope? scope)
        {

            if (!_registeredDependancies.TryGetValue(type, out DependancyLifetime lifetime))
            {
                throw new UknownDependancyException(type);
            }

            object instance;
            
            switch (lifetime)
            {
                case DependancyLifetime.TRANSIENT:
                    instance = _constructors[type]();
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
                                instance = _constructors[type]();
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
                            instance = _constructors[type]();
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
            return (T)Request(typeof(T), null);
        }

        public T Request<T>(Scope scope)
        {         
            return (T)Request(typeof(T), scope);
        }
    }



}