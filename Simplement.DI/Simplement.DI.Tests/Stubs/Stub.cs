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
        public string Name {get; set;}

        public DependantStup(Stub stub)
        {
            _stub = stub;
            Name = "Dependant Stub";
        }

    }

    public interface IDoer
    {
        public void DoStuff();
    }

    public class Doer : IDoer
    {
        public void DoStuff()
        {
            throw new NotImplementedException();
        }
    }

    public class DoerUser
    {
        private IDoer _doer;
        public DoerUser(IDoer doer)
        {
            _doer = doer;
        }
    }
}
