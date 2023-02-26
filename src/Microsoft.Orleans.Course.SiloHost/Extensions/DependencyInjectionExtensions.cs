using Microsoft.Orleans.Course.SiloHost.Enums;
using Microsoft.Orleans.Course.SiloHost.Options;

namespace Microsoft.Orleans.Course.SiloHost.Extensions;

public static class DependencyInjectionExtensions
{
    public static ISiloBuilder AddPersistence(
        this ISiloBuilder builder,
        IConfiguration configuration)
    {
        var persistenceOptions = configuration
            .GetRequiredSection(PersistenceOptions.SectionName)
            .Get<PersistenceOptions>();

        if (persistenceOptions is null 
            || persistenceOptions.IsEnabled is false
            || persistenceOptions.Type is null)
        {
            return builder;
        }

        if (persistenceOptions.Type is not null or PersistenceType.None or PersistenceType.InMemory
            && string.IsNullOrEmpty(persistenceOptions.ConnectionString))
        {
            throw new Exception("Please, assign a connection string");
        }

        builder.AddPersistence(persistenceOptions);

        return builder;
    }

    private static void AddPersistence(this ISiloBuilder builder,
        PersistenceOptions options)
    {
        switch (options.Type)
        {
            case PersistenceType.Redis:
                builder.AddRedisPersistence(options);
                break;
            case PersistenceType.InMemory:
                builder.AddInMemoryPersistence();
                break;
            case PersistenceType.MySql:
                builder.AddMySqlPersistence(options);
                break;
            case PersistenceType.None:
                break;
            case null:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static void AddInMemoryPersistence(this ISiloBuilder builder)
    {
        builder.AddMemoryGrainStorageAsDefault();
    }
    
    private static void AddRedisPersistence(this ISiloBuilder builder,
        PersistenceOptions options)
    {
        builder.AddRedisGrainStorageAsDefault(storageOptions =>
        {
            storageOptions.ConnectionString = options.ConnectionString;
        });
    }
    
    private static void AddMySqlPersistence(this ISiloBuilder builder,
        PersistenceOptions options)
    {
        builder.AddAdoNetGrainStorageAsDefault(storageOptions =>
        {
            storageOptions.Invariant = AdoNetInvariants.InvariantNameMySql;
            storageOptions.ConnectionString = options.ConnectionString;
        });
    }
}