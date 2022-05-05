using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplement.DI.CoreLib
{
    public static class ContainerFactory
    {
        public static ContainerBuilder CreateBuilder()
        {
            return new ContainerBuilder();
        }

        public static ContainerBuilder CreateBuilder(Action<ContainerBuilder> configure)
        {
            ContainerBuilder builder = new ContainerBuilder();
            configure(builder);
            return builder;
        }


    }
}
