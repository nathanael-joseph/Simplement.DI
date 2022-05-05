using Simplement.DI.CoreLib;
using System;
using Xunit;

namespace Simplement.DI.Tests
{
    public class UnitTest1
    {
        
        
        [Fact]
        public void test0()
        {
            Type t = typeof(int);
            Assert.True(t.IsValueType);
        }

        [Fact]
        public void ContainerSanityCheck()
        {
            Container container = ContainerFactory.CreateBuilder(builder =>
            {
                builder.RegisterTransient<string>();
            }).Build(); 

            Assert.Null(container.Request<string>());
        }
    }
}