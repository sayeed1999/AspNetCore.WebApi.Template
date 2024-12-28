using System.Data.Common;

namespace AspNetCore.WebApi.Template.Application.FunctionalTests.Common;

public interface ITestDatabase
{
    Task InitializeAsync();
    DbConnection GetConnection();
    string GetConnectionString();
    Task ResetAsync();
    Task DisposeAsync();
}
