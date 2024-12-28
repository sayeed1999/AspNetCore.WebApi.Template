namespace AspNetCore.WebApi.Template.Application.FunctionalTests.Common;

public class TestDatabaseFactory
{
    /// <summary>
    ///     Adopting factory pattern, you can decide which database to create from here giving support to multi-database types.
    /// </summary>
    /// <returns></returns>
    public static async Task<ITestDatabase> CreateAsync()
    {
        // Testcontainers requires Docker. To use a local PostgreSQL database instead,
        // switch to `PostgreSQLTestDatabase` and update appsettings.json.
        PostgreSqlTestcontainersTestDatabase? database = new();

        await database.InitializeAsync();

        return database;
    }
}
