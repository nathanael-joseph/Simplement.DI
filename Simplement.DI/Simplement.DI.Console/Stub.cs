using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplement.DI.Console
{
    internal class Stub
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Stub() 
        {
            Name = "Stub";
            Age = 0;
        }
    }
}
