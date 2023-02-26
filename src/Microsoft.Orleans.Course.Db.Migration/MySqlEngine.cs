using System.Reflection;
using DbUp;
using DbUp.Engine;

namespace Microsoft.Orleans.Course.Db.Migration;

internal static class MySqlEngine
{
    internal static UpgradeEngine BuildMySqlEngine(this string connectionString)
    {
        var upgradeEngine =
            DeployChanges.To
                .MySqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogToConsole()
                .Build();

        return upgradeEngine;
    }
}