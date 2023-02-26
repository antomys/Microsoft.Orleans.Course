using Microsoft.Extensions.Logging;
using Microsoft.Orleans.Course.Grains.Extensions;
using Microsoft.Orleans.Course.Grains.Interfaces;
using Orleans.Providers;

namespace Microsoft.Orleans.Course.Grains.Grains;

[StorageProvider]
internal sealed class Hello : Grain<HelloArchive>, IHello
{
    private readonly ILogger<Hello> _logger;

    public Hello(ILogger<Hello> logger)
    {
        _logger = logger;
    }

    public async ValueTask<string> SayHello(string greeting)
    {
        State.Greetings.Add(greeting);

        await WriteStateAsync();
        _logger.LogSay(greeting);

        var key = this.GetPrimaryKeyString();

        return $"Client with key {key} said {greeting}";
    }
}