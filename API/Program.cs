using Microsoft.EntityFrameworkCore;
using Taggy.API.Helpers;
using Taggy.Infrastructure.Data;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("API/appsettings.json", optional: false, reloadOnChange: false)
    .AddJsonFile($"API/appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: false)
    .AddEnvironmentVariables();

ConfigurationHelper.ConfigureServices(builder);
ConfigurationHelper.ConfigureAuthentication(builder);

WebApplication app = builder.Build();

ConfigurationHelper.HandleEnvironment(app);

// Automatizar a inserção do código SQL gerado pelo entity framework (Migrations)
using (var scope = app.Services.CreateScope())
{
    Console.Write("Running migrations...");
    AppDbContext db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
}

// Adiciona os Middlewares (devo colocar em uma função depois)
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
