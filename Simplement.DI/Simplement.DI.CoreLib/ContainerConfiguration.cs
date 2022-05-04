using Simplement.DI.CoreLib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplement.DI.CoreLib
{
    public class ContainerConfiguration
    {
        private readonly Dictionary<Type, DependancyLifetime> _registeredDependancies = new Dictionary<Type, DependancyLifetime>();
        private readonly Dictionary<Type, Func<Container,object>> _constructors = new Dictionary<Type, Func<Container,object>>();
        
        public ContainerConfiguration RegisterSingleton<I, T>()
        {
            return RegisterDependancy<I, T>(DependancyLifetime.SINGLTON);
        }
        public ContainerConfiguration RegisterSingleton<I, T>(Func<T> constructor)
        {
            return RegisterDependancy<I, T>(DependancyLifetime.SINGLTON, constructor);
        }
        public ContainerConfiguration RegisterSingleton<T>()
        {
            return RegisterDependancy<T, T>(DependancyLifetime.SINGLTON);
        }
        public ContainerConfiguration RegisterSingleton<T>(Func<T> constructor)
        {
            return RegisterDependancy<T, T>(DependancyLifetime.SINGLTON, constructor);
        }

        public ContainerConfiguration RegisterScoped<I, T>()
        {
            return RegisterDependancy<I, T>(DependancyLifetime.SCOPED);
        }
        public ContainerConfiguration RegisterScoped<I, T>(Func<T> constructor)
        {
            return RegisterDependancy<I, T>(DependancyLifetime.SCOPED, constructor);
        }
        public ContainerConfiguration RegisterScoped<T>()
        {
            return RegisterDependancy<T, T>(DependancyLifetime.SCOPED);
        }
        public ContainerConfiguration RegisterScoped<T>(Func<T> constructor)
        {
            return RegisterDependancy<T, T>(DependancyLifetime.SCOPED, constructor);
        }

        public ContainerConfiguration RegisterTransient<I, T>()
        {
            return RegisterDependancy<I, T>(DependancyLifetime.TRANSIENT);
        }
        public ContainerConfiguration RegisterTransient<I, T>(Func<T> constructor)
        {
            return RegisterDependancy<I, T>(DependancyLifetime.TRANSIENT, constructor);
        }
        public ContainerConfiguration RegisterTransient<T>()
        {
            return RegisterDependancy<T,T>( DependancyLifetime.TRANSIENT);
        }
        public ContainerConfiguration RegisterTransient<T>(Func<T> constructor)
        {
            return RegisterDependancy<T, T>(DependancyLifetime.TRANSIENT, constructor);
        }

        private ContainerConfiguration RegisterDependancy<I,T>(DependancyLifetime lifetime, Func<T>? constructor = null)
        {
            throw new NotImplementedException();
        }
    }
}
