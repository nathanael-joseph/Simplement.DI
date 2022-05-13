using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplement.DI.Tests.Stubs
{
    public class Stub
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Stub() 
        {
            Name = "Stub";
            Age = 0;
        }
    }

    public class DependantStup
    {
        private Stub _stub;


        public DependantStup(Stub stub)
        {
            _stub = stub;
        }
    }
}
