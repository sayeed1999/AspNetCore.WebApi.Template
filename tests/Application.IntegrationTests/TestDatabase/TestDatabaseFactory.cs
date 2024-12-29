namespace Application.IntegrationTests.TestDatabase;

public static class TestDatabaseFactory
{
    public static async Task<ITestDatabase> CreateAsync()
    {
        PostgreSqlTestcontainersTestDatabase testdb = new();
        await testdb.InitializeAsync();
        return testdb;
    }
}
