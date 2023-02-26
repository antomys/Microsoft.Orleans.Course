using Microsoft.Extensions.Logging;
using Microsoft.Orleans.Course.Grains.Extensions;
using Microsoft.Orleans.Course.Grains.Interfaces;

namespace Microsoft.Orleans.Course.Grains.Grains;

public sealed class Hello : Grain, IHello
{
    private readonly ILogger<Hello> _logger;

    public Hello(ILogger<Hello> logger)
    {
        _logger = logger;
    }

    public ValueTask<string> SayHello(string greeting)
    {
        _logger.LogSay(greeting);

        return ValueTask.FromResult($"Client {IdentityString} said {greeting}");
    }
}