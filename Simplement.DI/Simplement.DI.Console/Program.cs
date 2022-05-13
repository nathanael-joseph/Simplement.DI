
using Simplement.DI.CoreLib;
using Simplement.DI.Console;
using System.Diagnostics;

Stopwatch stopwatch = new Stopwatch();

Console.WriteLine("registering Stub with () => new Stub() ");
Container container = ContainerFactory.CreateBuilder(config =>
    {
        config.RegisterTransient<Stub>(() => new Stub())
              .RegisterTransient<DependantStup>()
              .RegisterTransient<int>();
    }).Build();

const int AMOUNT = 100000;

Stub[] stubs = new Stub[AMOUNT];


stopwatch.Start();
for(int i = 0; i < stubs.Length; i++)
{
    stubs[i] = new Stub();
}
stopwatch.Stop();
Console.WriteLine($"{stopwatch.Elapsed} to create {AMOUNT} stubs with new() ...");

stopwatch.Restart();
for (int i = 0; i < stubs.Length; i++)
{
    stubs[i] = container.Request<Stub>();
}
stopwatch.Stop();
Console.WriteLine($"{stopwatch.Elapsed} to create {AMOUNT} stubs with container.Request<Stub>() ...");
Console.WriteLine("###############################################################################");
Console.WriteLine("registering Stub with Invoke(null) ");
container = ContainerFactory.CreateBuilder(builder =>
{
    builder.RegisterTransient<Stub>()
            .RegisterTransient<DependantStup>()
            .RegisterTransient<int>();
}).Build();


stopwatch.Restart();
for (int i = 0; i < stubs.Length; i++)
{
    stubs[i] = new Stub();
}
stopwatch.Stop();
Console.WriteLine($"{stopwatch.Elapsed} to create {AMOUNT} stubs with new() ...");

stopwatch.Restart();
for (int i = 0; i < stubs.Length; i++)
{
    stubs[i] = container.Request<Stub>();
}
stopwatch.Stop();
Console.WriteLine($"{stopwatch.Elapsed} to create {AMOUNT} stubs with container.Request<Stub>() ...");

DependantStup[] dstubs = new DependantStup[AMOUNT];
stopwatch.Restart();
for (int i = 0; i < stubs.Length; i++)
{
    dstubs[i] = new DependantStup(new Stub(), 0);
}
stopwatch.Stop();
Console.WriteLine($"{stopwatch.Elapsed} to create {AMOUNT} DependantStups with new() ...");

stopwatch.Restart();
for (int i = 0; i < stubs.Length; i++)
{
    dstubs[i] = container.Request<DependantStup>();
}
stopwatch.Stop();
Console.WriteLine($"{stopwatch.Elapsed} to create {AMOUNT} DependantStups with container.Request<Stub>() ...");


stopwatch.Restart();
for (int i = 0; i < stubs.Length; i++)
{
    stubs[i] = container.Request<Stub>();
}
stopwatch.Stop();
Console.WriteLine($"{stopwatch.Elapsed} to create {AMOUNT} stubs with container.Request<Stub>() ...");

stopwatch.Restart();
for (int i = 0; i < stubs.Length; i++)
{
    stubs[i] = container.Request<Stub>();
}
stopwatch.Stop();
Console.WriteLine($"{stopwatch.Elapsed} to create {AMOUNT} stubs with container.Request<Stub>() ...");

stopwatch.Restart();
for (int i = 0; i < stubs.Length; i++)
{
    stubs[i] = container.Request<Stub>();
}
stopwatch.Stop();
Console.WriteLine($"{stopwatch.Elapsed} to create {AMOUNT} stubs with container.Request<Stub>() ...");
Console.ReadKey();