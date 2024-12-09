using chalk.Server.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace chalk.IntegrationTests.Helpers;

public class IntegrationTestFactory<TProgram>(DatabaseFixture databaseFixture) : WebApplicationFactory<TProgram>
    where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");

        Environment.SetEnvironmentVariable("Jwt:Issuer", "*");
        Environment.SetEnvironmentVariable("Jwt:Audience", "*");
        Environment.SetEnvironmentVariable("Jwt:Key", "yt1Piu9iM9wxQjc7Vufwl5lSUltHPQue");

        builder.ConfigureServices(services =>
        {
            var contextDescriptor =
                services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<DatabaseContext>));
            if (contextDescriptor is not null)
            {
                services.Remove(contextDescriptor);
            }

            var context = services.SingleOrDefault(s => s.ServiceType == typeof(DatabaseContext));
            if (context is not null)
            {
                services.Remove(context);
            }

            services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(databaseFixture.ConnectionString));
        });
    }
}