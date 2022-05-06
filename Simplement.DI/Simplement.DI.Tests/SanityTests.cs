using Simplement.DI.CoreLib;
using System;
using Xunit;

namespace Simplement.DI.Tests
{
    public class SanityTests
    {
        


        [Fact]
        public void AssertDefaultValueReturnedForValueTypes()
        {
            Container container = ContainerFactory.CreateBuilder(builder =>
            {
                builder.RegisterTransient<int>()
                        .RegisterTransient<string>()
                        .RegisterTransient<bool>();
            }).Build();

            Assert.Equal(0, container.Request<int>());
            Assert.Null(container.Request<string>());
            Assert.False(container.Request<bool>());
        }


    }
}