using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplement.DI.CoreLib
{
    public class ContainerBuilder
    {
        public ContainerConfiguration Configuration { get; set; }

        public ContainerBuilder() 
        {
            Configuration = new ContainerConfiguration();
        }
        public ContainerBuilder(ContainerConfiguration configuration) 
        {
           Configuration = configuration;
        }

        public Container Build()
        {
            throw new NotImplementedException();
        }
    }
}
