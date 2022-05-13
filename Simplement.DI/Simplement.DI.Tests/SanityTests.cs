using Simplement.DI.CoreLib;
using Simplement.DI.CoreLib.Exceptions;
using Simplement.DI.Tests.Stubs;
using System;
using Xunit;

namespace Simplement.DI.Tests
{
    public class SanityTests
    {
        


        [Fact]
        public void AssertDefaultValueReturnedForValueTypes()
        {
            Container container = ContainerFactory.CreateBuilder(configuration =>
            {
                configuration.RegisterTransient<int>()
                            .RegisterTransient<string>()
                            .RegisterTransient<bool>();
            }).Build();

            Assert.Equal(0, container.Request<int>());
            Assert.Null(container.Request<string>());
            Assert.False(container.Request<bool>());
        }

        [Fact]
        public void AssertContainerScopedDependencyException()
        {
            Container c = ContainerFactory.CreateBuilder(configuration =>
            {
                configuration.RegisterTransient<Stub>()
                            .RegisterScoped<DependantStup>();
            }).Build();

            Assert.Throws<InvalidScopedDependencyRequestException>(c.Request<DependantStup>);
        }


    }
}