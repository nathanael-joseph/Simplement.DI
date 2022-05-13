using Simplement.DI.CoreLib.Dependencies;
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
        private readonly ContainerConfiguration _containerConfiguration;

        public ContainerBuilder(ContainerConfiguration configuration) 
        {
            _containerConfiguration = configuration;
        }

        private Dictionary<Type, DependencyBase> BuildContainerDictionary()
        {
            Dictionary<Type, DependencyBase> containerDictionary = new Dictionary<Type, DependencyBase>(_containerConfiguration.RegisteredDependencies.Count);
            Dictionary<Type, DependencyRegistration> dependenciesToAdd = new Dictionary<Type, DependencyRegistration>();

            foreach(DependencyRegistration registration in _containerConfiguration.RegisteredDependencies)
            {
                if (registration.Constructor != null) 
                {
                    containerDictionary.Add(registration.DependancyType, BuildDependency(registration));
                }
                else
                {
                    dependenciesToAdd.Add(registration.DependancyType, registration);
                }
            }
            
            while(dependenciesToAdd.Count > 0) 
            {
                AddToContainerDictionaryDFS(dependenciesToAdd.First().Value,
                                            dependenciesToAdd,
                                            containerDictionary);
            }
            
            return containerDictionary;
        }

        private DependencyBase BuildDependency(DependencyRegistration registration) 
        {
            if(registration.Constructor == null)
            {
                throw new InvalidOperationException();
            }
            return BuildDependency(registration.Lifetime, registration.Constructor);
        }

        private DependencyBase BuildDependency(DependencyLifetime lifetime, Func<object?> constructor)
        {
            switch (lifetime)
            {
                case DependencyLifetime.SCOPED:
                    return new ScopedDependency(constructor);
                case DependencyLifetime.SINGLTON:
                    return new SingletonDependency(constructor);
                case DependencyLifetime.TRANSIENT:
                default:
                    return new TransientDependency(constructor);
            }
        }

        private void AddToContainerDictionaryDFS(DependencyRegistration registration, 
                                                 Dictionary<Type, DependencyRegistration> dependenciesToAdd,
                                                 Dictionary<Type, DependencyBase> containerDictionary)
        {
            Func<object?> constructor;
            Type implementationType = registration.ImplementationType;
            ConstructorInfo[] constructorInfos = implementationType.GetConstructors();

            if (implementationType.IsValueType)
            {                
                constructor = () => Activator.CreateInstance(implementationType);
            }
            else if (implementationType == typeof(string))
            {
                constructor = () => null;
            }
            else if (constructorInfos.Length == 0)
            {
                throw new DependencyConstructorException(implementationType);
            }
            else
            {
                ConstructorInfo constructorInfo = constructorInfos
                                                    .Where(ci => ci.IsPublic)
                                                    .OrderBy(ci => ci.GetParameters().Length)
                                                    .First();
                
                if (constructorInfo == null)
                {
                    throw new DependencyConstructorException(implementationType);
                }

                Type[] paramTypes = constructorInfo.GetParameters()
                                                    .AsQueryable()
                                                    .Select(pi => pi.ParameterType)
                                                    .ToArray();

                if (paramTypes.Length == 0)
                {
                    constructor = () => constructorInfo.Invoke(null);
                }
                else 
                {
                    Func<object?>[] paramGetters = new Func<object?>[paramTypes.Length];
                    for(int i = 0; i < paramTypes.Length; i++) 
                    {
                        Type paramType = paramTypes[i];
                        if (!containerDictionary.ContainsKey(paramType))
                        {
                            if(!dependenciesToAdd.ContainsKey(paramType))
                            {
                                throw new UknownDependencyException(paramType);
                            }
                            AddToContainerDictionaryDFS(dependenciesToAdd[paramType], 
                                                        dependenciesToAdd,
                                                        containerDictionary);
                        }

                        paramGetters[i] = () => containerDictionary[paramType].Instance;
                    }

                    constructor = () => {
                        object?[] parameters = new object[paramTypes.Length];
                        for(int i = 0; i < paramTypes.Length; i++) {
                            parameters[i] = paramGetters[i]();
                        }
                        return constructorInfo.Invoke(parameters);
                    };
                }
            }

            DependencyBase dependency = BuildDependency(registration.Lifetime, constructor);
            containerDictionary.Add(registration.DependancyType, dependency);
            dependenciesToAdd.Remove(registration.DependancyType);
        }


        public Container Build()
        {
            Dictionary<Type, DependencyBase> containerDictionary = BuildContainerDictionary();
            return new Container(containerDictionary);
        }
    }
}
