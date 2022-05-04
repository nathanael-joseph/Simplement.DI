using Simplement.DI.CoreLib;
using System;
using Xunit;

namespace Simplement.DI.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Assert.Throws<NotImplementedException>(() =>
               {
                   Container container = ContainerFactory.CreateBuilder(configuration =>
                   {
                       configuration.RegisterScoped<string>()
                                   .RegisterScoped<object>()
                                   .RegisterScoped<Int32>();
                   }).Build();
               }
            );
            
        }
    }
}