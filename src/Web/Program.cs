using Infrastructure.Identity;
using static Web.DependencyInjection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", true, true)
    .AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddKeyVaultIfConfigured(builder.Configuration);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebServices(builder.Environment);

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (builder.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!builder.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
    app.UseHsts();
}

app.MapStaticAssets(); // tries to map static assets from /wwwroot folder

app.MapHealthChecks("/health").DisableHttpMetrics();

app.UseAuthorization();

app.UseExceptionHandler();

app.MapControllers();

app.MapIdentityApi<ApplicationUser>();

app.Run();

public partial class Program
{
}
