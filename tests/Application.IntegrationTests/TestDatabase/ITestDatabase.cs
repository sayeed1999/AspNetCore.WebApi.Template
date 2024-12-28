using System.Data.Common;

namespace Application.IntegrationTests.TestDatabase;

public interface ITestDatabase
{
    Task InitializeAsync();
    DbConnection GetConnection();
    string GetConnectionString();
    Task ResetAsync();
    Task DisposeAsync();
}
