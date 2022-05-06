using Simplement.DI.CoreLib.Enums;
using Simplement.DI.CoreLib.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Simplement.DI.CoreLib
{
    public class ContainerBuilder
    {
        private readonly Dictionary<Type, Func<Container, Scope?, object?>> _constructors = new Dictionary<Type, Func<Container, Scope?, object?>>();
        private readonly Dictionary<Type, DependancyLifetime> _registeredDependancies = new Dictionary<Type, DependancyLifetime>();

        public ContainerBuilder RegisterSingleton<I, T>()
        {
            return RegisterDependancy<I, T>(DependancyLifetime.SINGLTON);
        }
        public ContainerBuilder RegisterSingleton<I, T>(Func<T> constructor)
        {
            return RegisterDependancy<I, T>(DependancyLifetime.SINGLTON, constructor);
        }
        public ContainerBuilder RegisterSingleton<T>()
        {
            return RegisterDependancy<T, T>(DependancyLifetime.SINGLTON);
        }
        public ContainerBuilder RegisterSingleton<T>(Func<T> constructor)
        {
            return RegisterDependancy<T, T>(DependancyLifetime.SINGLTON, constructor);
        }

        public ContainerBuilder RegisterScoped<I, T>()
        {
            return RegisterDependancy<I, T>(DependancyLifetime.SCOPED);
        }
        public ContainerBuilder RegisterScoped<I, T>(Func<T> constructor)
        {
            return RegisterDependancy<I, T>(DependancyLifetime.SCOPED, constructor);
        }
        public ContainerBuilder RegisterScoped<T>()
        {
            return RegisterDependancy<T, T>(DependancyLifetime.SCOPED);
        }
        public ContainerBuilder RegisterScoped<T>(Func<T> constructor)
        {
            return RegisterDependancy<T, T>(DependancyLifetime.SCOPED, constructor);
        }

        public ContainerBuilder RegisterTransient<I, T>()
        {
            return RegisterDependancy<I, T>(DependancyLifetime.TRANSIENT);
        }
        public ContainerBuilder RegisterTransient<I, T>(Func<T> constructor)
        {
            return RegisterDependancy<I, T>(DependancyLifetime.TRANSIENT, constructor);
        }
        public ContainerBuilder RegisterTransient<T>()
        {
            return RegisterDependancy<T, T>(DependancyLifetime.TRANSIENT);
        }
        public ContainerBuilder RegisterTransient<T>(Func<T> constructor)
        {
            return RegisterDependancy<T, T>(DependancyLifetime.TRANSIENT, constructor);
        }

        private ContainerBuilder RegisterDependancy<I, T>(DependancyLifetime lifetime, Func<T>? constructor = null)
        {
            Type type = typeof(I);

            if (!_registeredDependancies.ContainsKey(type))
            {
                _registeredDependancies[typeof(I)] = lifetime;
                if (constructor != null)
                {
                    _constructors[type] = (container, scope) => constructor();
                }
                else
                {
                    Func<Container, Scope?, object?> _constructor = CreateConstructor<T>();
                    _constructors[type] = _constructor;
                }

                return this;
            }

            throw new DuplicateDependencyException(type);
        }

        private Func<Container, Scope?, object?> CreateConstructor<T>()
        {
            Type type = typeof(T);
            Func<Container, Scope?, object?> constructor;
            ConstructorInfo[] constructorInfos = type.GetConstructors();

            if (type.IsValueType)
            {                
                constructor = (container, scope) => Activator.CreateInstance(type);
            }
            else if (type == typeof(string))
            {
                constructor = (container, scope) => null;
            }
            else if (constructorInfos.Length == 0)
            {
                throw new DependencyConstructorException(type);
            }
            else
            {
                ConstructorInfo constructorInfo = constructorInfos
                                                    .Where(ci => ci.IsPublic)
                                                    .OrderBy(ci => ci.GetParameters().Length)
                                                    .First();

                ParameterInfo[] paramInfos = constructorInfo.GetParameters();

                if (paramInfos.Length == 0)
                {
                    constructor = (container, scope) => constructorInfo.Invoke(null);
                }
                else
                {
                    List<Type> parameterTypes = new List<Type>(paramInfos.Length);

                    foreach (var paramInfo in paramInfos)
                    {
                        parameterTypes.Add(paramInfo.ParameterType);
                    }

                    constructor = (container, scope) =>
                    {
                        object[] parameters = new object[parameterTypes.Count];
                        for (int i = 0; i < parameterTypes.Count; i++)
                        {
                            parameters[i] = container.Request(parameterTypes[i], scope);
                        }
                        return constructorInfo.Invoke(parameters);
                    };

                }
            }
            return constructor;
        }

        public Container Build()
        {
            return new Container(_registeredDependancies, _constructors);
        }
    }
}
