using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Orleans.Course.Grains.Interfaces;
using Orleans.Configuration;
using Orleans.Networking.Shared;
using Orleans.Runtime;
using Orleans.Runtime.Messaging;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Retry;

try
{
    using var host = await StartClientAsync();

    var client = host.Services.GetRequiredService<IClusterClient>();
    
    while (Console.ReadKey().Key != ConsoleKey.C)
    {
        var input = Console.ReadLine();
    
        await DoClientWorkAsync(client, input!);
    
        Console.ReadKey();
    }

    await host.StopAsync();

    return 0;
}
catch (Exception e)
{
    Console.WriteLine($$"""
        Exception while trying to run client: {{e}}
        Make sure the silo the client is trying to connect to is running.
        Press any key to exit.
        """);
    
    Console.ReadKey();
    return 1;
}

static async Task<IHost> StartClientAsync()
{
    var policy = BuildPolicy();

    var host = await policy.ExecuteAsync(async () =>
    {
        var builder = new HostBuilder()
            .UseOrleansClient(client =>
            {
                client          
                    .Configure<ClusterOptions>(options => 
                    {
                        options.ClusterId = "Dev";
                        options.ServiceId = "Course.Orleans";
                    })
                    .UseLocalhostClustering();
            })
            .ConfigureLogging(logging => logging.AddConsole());

        var host = builder.Build();
        await host.StartAsync();

        Console.WriteLine("Client successfully connected to silo host \n");

        return host;
    });

    return host;
}

static AsyncRetryPolicy<IHost> BuildPolicy()
{
    var delay = Backoff.DecorrelatedJitterBackoffV2(medianFirstRetryDelay: TimeSpan.FromSeconds(1), retryCount: 5);
   
    return Policy<IHost>
        .Handle<SiloUnavailableException>()
        .Or<OrleansMessageRejectionException>()
        .Or<SocketConnectionException>()
        .Or<ConnectionFailedException>()
        .WaitAndRetryAsync(delay);
}

static async Task DoClientWorkAsync(
    IGrainFactory client,
    string greeting)
{
    var friend = client.GetGrain<IHello>(Guid.NewGuid());
    var response = await friend.SayHello(greeting);

    Console.WriteLine($"\n\n{response}\n\n");
}