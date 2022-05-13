using Simplement.DI.CoreLib.Enums;
using Simplement.DI.CoreLib.Exceptions;

namespace Simplement.DI.CoreLib
{
    public class ContainerConfiguration
    {
        private Dictionary<Type, DependencyRegistration> _registeredDependencies;
        internal List<DependencyRegistration> RegisteredDependencies 
        {
            get => _registeredDependencies.Values.ToList();
        }
        public  ContainerConfiguration()
        {
            _registeredDependencies = new Dictionary<Type, DependencyRegistration>();
        }

         public ContainerConfiguration RegisterSingleton<I, T>()
        {
            return RegisterDependency<I, T>(DependencyLifetime.SINGLTON);
        }
        public ContainerConfiguration RegisterSingleton<I, T>(Func<T> constructor)
        {
            return RegisterDependency<I, T>(DependencyLifetime.SINGLTON, constructor);
        }
        public ContainerConfiguration RegisterSingleton<T>()
        {
            return RegisterDependency<T, T>(DependencyLifetime.SINGLTON);
        }
        public ContainerConfiguration RegisterSingleton<T>(Func<T> constructor)
        {
            return RegisterDependency<T, T>(DependencyLifetime.SINGLTON, constructor);
        }

        public ContainerConfiguration RegisterScoped<I, T>()
        {
            return RegisterDependency<I, T>(DependencyLifetime.SCOPED);
        }
        public ContainerConfiguration RegisterScoped<I, T>(Func<T> constructor)
        {
            return RegisterDependency<I, T>(DependencyLifetime.SCOPED, constructor);
        }
        public ContainerConfiguration RegisterScoped<T>()
        {
            return RegisterDependency<T, T>(DependencyLifetime.SCOPED);
        }
        public ContainerConfiguration RegisterScoped<T>(Func<T> constructor)
        {
            return RegisterDependency<T, T>(DependencyLifetime.SCOPED, constructor);
        }

        public ContainerConfiguration RegisterTransient<I, T>()
        {
            return RegisterDependency<I, T>(DependencyLifetime.TRANSIENT);
        }
        public ContainerConfiguration RegisterTransient<I, T>(Func<T> constructor)
        {
            return RegisterDependency<I, T>(DependencyLifetime.TRANSIENT, constructor);
        }
        public ContainerConfiguration RegisterTransient<T>()
        {
            return RegisterDependency<T, T>(DependencyLifetime.TRANSIENT);
        }
        public ContainerConfiguration RegisterTransient<T>(Func<T> constructor)
        {
            return RegisterDependency<T, T>(DependencyLifetime.TRANSIENT, constructor);
        }

        private ContainerConfiguration RegisterDependency<I, T>(DependencyLifetime lifetime, Func<T>? constructor = null) 
        {
            Type dependencyType = typeof(I);
            Type implementationType = typeof(T);

            if(_registeredDependencies.ContainsKey(dependencyType))
            {
                throw new DuplicateDependencyException(dependencyType);
            }

            DependencyRegistration registration;

            if (constructor != null) 
            {
                Func<object?> _constructor = () => constructor();
                registration = new DependencyRegistration(dependencyType, implementationType, lifetime, _constructor);
            }
            else
            {
                registration = new DependencyRegistration(dependencyType, implementationType, lifetime);
            }
            
            _registeredDependencies.Add(dependencyType, registration);
            return this;
        }
    }
}