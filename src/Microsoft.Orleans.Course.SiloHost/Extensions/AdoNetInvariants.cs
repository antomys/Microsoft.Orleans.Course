namespace Microsoft.Orleans.Course.SiloHost.Extensions;

internal static class AdoNetInvariants
{
    /// <summary>
    /// Microsoft SQL Server invariant name string.
    /// </summary>
    internal const string InvariantNameSqlServer = "System.Data.SqlClient";

    /// <summary>
    /// Oracle Database server invariant name string.
    /// </summary>
    internal const string InvariantNameOracleDatabase = "Oracle.DataAccess.Client";

    /// <summary>
    /// SQLite invariant name string.
    /// </summary>
    internal const string InvariantNameSqlLite = "System.Data.SQLite";

    /// <summary>
    /// MySql invariant name string.
    /// </summary>
    internal const string InvariantNameMySql = "MySql.Data.MySqlClient";

    /// <summary>
    /// PgSql invariant name string.
    /// </summary>
    internal const string InvariantNamePgSql = "Npgsql";

    /// <summary>
    /// Dotnet core Microsoft SQL Server invariant name string.
    /// </summary>
    internal const string InvariantNameSqlServerDotnetCore = "Microsoft.Data.SqlClient";

    /// <summary>
    /// An open source implementation of the MySQL connector library.
    /// </summary>
    internal const string InvariantNameMySqlConnector = "MySql.Data.MySqlConnector";
}