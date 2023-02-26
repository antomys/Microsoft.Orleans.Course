using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Orleans.Course.Grains.Interfaces;

try
{
    using var host = await StartClientAsync();

    var client = host.Services.GetRequiredService<IClusterClient>();

    var input = Console.ReadLine();
        
    await DoClientWorkAsync(client, input!);
    
    Console.ReadKey();

    await host.StopAsync();

    return 0;
}
catch (Exception e)
{
    Console.WriteLine($$"""
        Exception while trying to run client: {{e.Message}}
        Make sure the silo the client is trying to connect to is running.
        Press any key to exit.
        """);
    
    Console.ReadKey();
    return 1;
}

static async Task<IHost> StartClientAsync()
{
    var builder = new HostBuilder()
        .UseOrleansClient(client =>
        {
            client.UseLocalhostClustering();
        })
        .ConfigureLogging(logging => logging.AddConsole());

    var host = builder.Build();
    await host.StartAsync();

    Console.WriteLine("Client successfully connected to silo host \n");

    return host;
}

static async Task DoClientWorkAsync(
    IGrainFactory client,
    string greeting)
{
    var friend = client.GetGrain<IHello>(Guid.NewGuid());
    var response = await friend.SayHello(greeting);

    Console.WriteLine($"\n\n{response}\n\n");
}