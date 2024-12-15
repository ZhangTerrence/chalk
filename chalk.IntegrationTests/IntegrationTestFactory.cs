using System.Data.Common;
using System.Text;
using chalk.Server.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Respawn;
using Testcontainers.PostgreSql;

namespace chalk.IntegrationTests;

public class IntegrationTestFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _databaseContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("chalk.database.test")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    public const string Issuer = "SUT";
    public const string Audience = "IntegrationTests";
    public const string Key = "________________________________";

    public DatabaseContext DatabaseContext = null!;
    private DbConnection _databaseConnection = null!;
    private string ConnectionString => _databaseContainer.GetConnectionString();

    private Respawner _respawner = null!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");

        Environment.SetEnvironmentVariable("Jwt:Issuer", Issuer);
        Environment.SetEnvironmentVariable("Jwt:Audience", Audience);
        Environment.SetEnvironmentVariable("Jwt:Key", Key);

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

            services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(ConnectionString));
        });
    }

    public async Task InitializeAsync()
    {
        await _databaseContainer.StartAsync();

        var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>().UseNpgsql(ConnectionString);
        DatabaseContext = new DatabaseContext(optionsBuilder.Options);
        _databaseConnection = DatabaseContext.Database.GetDbConnection();

        await DatabaseContext.Database.EnsureCreatedAsync();
        await _databaseConnection.OpenAsync();

        _respawner = await Respawner.CreateAsync(_databaseConnection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = ["public"],
            TablesToIgnore = ["__EFMigrationsHistory"],
        });
    }

    public new async Task DisposeAsync()
    {
        await _databaseConnection.CloseAsync();
        await DatabaseContext.DisposeAsync().AsTask();
    }

    public async Task ResetDatabaseAsync()
    {
        await _respawner.ResetAsync(_databaseConnection);
    }
}