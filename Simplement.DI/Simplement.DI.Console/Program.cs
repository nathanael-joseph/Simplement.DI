
using Simplement.DI.CoreLib;


Console.WriteLine( typeof(string).IsValueType); 

Container container = ContainerFactory.CreateBuilder(builder =>
{
    builder.RegisterTransient<string>();
}).Build();

string s = container.Request<string>(); 


