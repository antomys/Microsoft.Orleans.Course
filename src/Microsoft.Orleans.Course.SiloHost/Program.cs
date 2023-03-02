using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Orleans.Course.SiloHost.Extensions;
using Microsoft.Orleans.Course.SiloHost.Filters;
using Microsoft.Orleans.Course.SiloHost.Options;
using Orleans.Configuration;
using Orleans.EventSourcing;
using Orleans.Runtime;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<PersistenceOptions>(
    builder.Configuration.GetRequiredSection(PersistenceOptions.SectionName));

builder.Host
    .UseOrleans(silo =>
    {
        //Workaround for a bug https://github.com/dotnet/orleans/issues/8157
        silo.Services.TryAddSingleton<Factory<IGrainContext, ILogConsistencyProtocolServices>>(serviceProvider =>
        {
            var factory = ActivatorUtilities.CreateFactory(typeof(ProtocolServices), new[] { typeof(IGrainContext) });
           
            return grainContext => (ILogConsistencyProtocolServices)factory(serviceProvider, new object[] { grainContext });
        });
        
        silo
            .Configure<ClusterOptions>(options => 
            {
                options.ClusterId = "Dev";
                options.ServiceId = "Course.Orleans";
            })
            .UseDashboard(options => options.HostSelf = true)
            .UseLocalhostClustering()
            .AddStateStorageBasedLogConsistencyProvider(name: "StateStorage")
            .AddCustomStorageBasedLogConsistencyProvider(name: "EventSource")
            .AddIncomingGrainCallFilter<LoggingFilter>()
            .ConfigureLogging(logging => logging.AddConsole())
            .AddPersistence(builder.Configuration);
    }); 
    
var app = builder.Build();

app.Map("/dashboard", applicationBuilder => applicationBuilder.UseOrleansDashboard());

app.Run();