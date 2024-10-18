using AspNetCore.WebApi.Template.Infrastructure.Data;
using AspNetCore.WebApi.Template.Infrastructure.Identity;
using static AspNetCore.WebApi.Template.Web.Extensions.SwaggerServiceExtension;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddKeyVaultIfConfigured(builder.Configuration);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebServices();
builder.Services.RegisterSwagger(nameof(AspNetCore.WebApi.Template));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCustomExceptionHandler();

app.UseHealthChecks("/health");
// app.UseHttpsRedirection();
app.UseStaticFiles();

if (builder.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<ApplicationUser>();

app.MapControllers();

app.Run();

public partial class Program { }
