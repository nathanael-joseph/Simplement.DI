using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplement.DI.CoreLib
{
    public static class ContainerFactory
    {
        public static ContainerBuilder CreateBuilder(ContainerConfiguration containerConfiguration)
        {
            return new ContainerBuilder(containerConfiguration);
        }

        public static ContainerBuilder CreateBuilder(Action<ContainerConfiguration> configure)
        {
            ContainerConfiguration configuration = new ContainerConfiguration();
            configure(configuration);
            ContainerBuilder builder = new ContainerBuilder(configuration);
        
            return builder;
        }


    }
}
