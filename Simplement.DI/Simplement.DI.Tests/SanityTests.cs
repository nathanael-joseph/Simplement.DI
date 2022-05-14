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
            Container container = ContainerFactory.CreateBuilder(configuration =>
            {
                configuration.RegisterTransient<Stub>()
                            .RegisterScoped<DependantStup>();
            }).Build();

            Assert.Throws<InvalidScopedDependencyRequestException>(container.Request<DependantStup>);

            using (Container scopedContainer = container.CreateScope())
            {
                Assert.NotNull(scopedContainer.Request<DependantStup>());
            }
            
        }

        [Fact]
        public void AssertCanRequestInterface() 
        {
            Container container = ContainerFactory.CreateBuilder(configuration =>
            {
                configuration.RegisterTransient<IDoer, Doer>();
            }).Build();

            IDoer doer = container.Request<IDoer>();

            Assert.NotNull(doer);
        }

        [Fact]
        public void AssertCanInjectInterface() 
        {
            Container container = ContainerFactory.CreateBuilder(configuration =>
            {
                configuration.RegisterTransient<IDoer, Doer>()
                            .RegisterTransient<DoerUser>();
            }).Build();   

            DoerUser du = container.Request<DoerUser>();
            
            Assert.NotNull(du);
        }

        [Fact]
        public void AssertTransientIsTransient()
        {
            Container container = ContainerFactory.CreateBuilder(configuration =>
            {
                configuration.RegisterTransient<Stub>();
            }).Build();  

            Stub s1 = container.Request<Stub>();
            Stub s2 = container.Request<Stub>();

            Assert.NotEqual(s1, s2);

            using (Container scopedContainer = container.CreateScope())
            {
                Stub s3 = scopedContainer.Request<Stub>();

                Assert.NotEqual(s2, s3);
            }
        }

        [Fact]
        public void AssertSingletonIsSingleton()
        {
            Container container = ContainerFactory.CreateBuilder(configuration =>
            {
                configuration.RegisterTransient<Stub>()
                            .RegisterSingleton<DependantStup>();
            }).Build();   

            DependantStup ds1 = container.Request<DependantStup>();
            DependantStup ds2 = container.Request<DependantStup>();

            Assert.Equal(ds1, ds2);

            using (Container scopedContainer = container.CreateScope())
            {
                DependantStup ds3 = scopedContainer.Request<DependantStup>();

                Assert.Equal(ds2, ds3);
            }

        }

        [Fact]
        public void AssertScopedIsScoped()
        {
            Container container = ContainerFactory.CreateBuilder(configuration =>
            {
                configuration.RegisterTransient<Stub>()
                            .RegisterScoped<DependantStup>();
            }).Build();

            using (Container scopedContainer1 = container.CreateScope())
            {
                Assert.NotNull(scopedContainer1.Request<DependantStup>());

                DependantStup ds1 = scopedContainer1.Request<DependantStup>();
                DependantStup ds2 = scopedContainer1.Request<DependantStup>();

                Assert.Equal(ds1, ds2);

                using(Container scopedContainer2 = container.CreateScope())
                {
                    DependantStup ds3 = scopedContainer2.Request<DependantStup>();

                    Assert.NotEqual(ds2, ds3);
                }

            }
        }
    }
}