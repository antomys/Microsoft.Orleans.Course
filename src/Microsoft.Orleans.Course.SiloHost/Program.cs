using Microsoft.Orleans.Course.SiloHost.Extensions;
using Microsoft.Orleans.Course.SiloHost.Options;
using Orleans.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<PersistenceOptions>(
    builder.Configuration.GetRequiredSection(PersistenceOptions.SectionName));

builder.Host
    .UseOrleans(silo =>
    {
        silo
            .Configure<ClusterOptions>(options => 
            {
                options.ClusterId = "Dev";
                options.ServiceId = "Course.Orleans";
            })
            .UseLocalhostClustering()
            .ConfigureLogging(logging => logging.AddConsole())
            .AddPersistence(builder.Configuration);
    }); 
    
var app = builder.Build();

app.Run();