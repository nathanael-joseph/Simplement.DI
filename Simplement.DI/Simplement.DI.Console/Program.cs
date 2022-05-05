
using Simplement.DI.CoreLib;
using Simplement.DI.Console;
using System.Diagnostics;

Stopwatch stopwatch = new Stopwatch();

Console.WriteLine("registering Stub with () => new Stub() ");
Container container = ContainerFactory.CreateBuilder(builder =>
{
    builder.RegisterTransient<Stub>(() => new Stub());
}).Build();

const int AMOUNT = 10000;

Stub[] stubs = new Stub[AMOUNT];
for (int i = 0; i < stubs.Length; i++)
{
    stubs[i] = null;
}

stopwatch.Start();
for(int i = 0; i < stubs.Length; i++)
{
    stubs[i] = new Stub();
}
stopwatch.Stop();
Console.WriteLine($"{stopwatch.Elapsed} to create {AMOUNT} stubs with new() ...");

stopwatch.Start();
for (int i = 0; i < stubs.Length; i++)
{
    stubs[i] = container.Request<Stub>();
}
stopwatch.Stop();
Console.WriteLine($"{stopwatch.Elapsed} to create {AMOUNT} stubs with container.Request<Stub>() ...");


Console.WriteLine("registering Stub with Invoke(null) ");
container = ContainerFactory.CreateBuilder(builder =>
{
    builder.RegisterTransient<Stub>(() => new Stub());
}).Build();


stopwatch.Restart();
for (int i = 0; i < stubs.Length; i++)
{
    stubs[i] = new Stub();
}
stopwatch.Stop();
Console.WriteLine($"{stopwatch.Elapsed} to create {AMOUNT} stubs with new() ...");

stopwatch.Start();
for (int i = 0; i < stubs.Length; i++)
{
    stubs[i] = container.Request<Stub>();
}
stopwatch.Stop();
Console.WriteLine($"{stopwatch.Elapsed} to create {AMOUNT} stubs with container.Request<Stub>() ...");
Console.ReadKey();