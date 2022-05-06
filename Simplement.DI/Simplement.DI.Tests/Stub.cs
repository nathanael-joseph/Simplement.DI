using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplement.DI.Tests
{
    internal class Stub : IFoo, IBar
    {
        public void Bar()
        {
            Console.WriteLine("Bar");
        }

        public void Foo()
        {
            Console.WriteLine("Foo");
        }
    }

    internal interface IFoo
    {
        public abstract void Foo(); 
    }

    internal interface IBar
    {
        public abstract void Bar();
    }
}
