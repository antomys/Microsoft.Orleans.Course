var builder = WebApplication.CreateBuilder(args);

builder.Host
    .UseOrleans(silo =>
    {
        silo.UseLocalhostClustering()
            .ConfigureLogging(logging => logging.AddConsole());
    });
    
var app = builder.Build();

app.Run();