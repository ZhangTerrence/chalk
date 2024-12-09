using System.Data.Common;
using chalk.Server.Data;
using Microsoft.EntityFrameworkCore;
using Respawn;
using Testcontainers.PostgreSql;

namespace chalk.IntegrationTests.Helpers;

public class DatabaseFixture : IAsyncLifetime
{
    private readonly PostgreSqlContainer _databaseContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("chalk.database.test")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    public string ConnectionString => _databaseContainer.GetConnectionString();

    private DatabaseContext _databaseContext = null!;
    private Respawner _respawner = null!;
    private DbConnection _databaseConnection = null!;

    public async Task InitializeAsync()
    {
        await _databaseContainer.StartAsync();

        var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>().UseNpgsql(ConnectionString);
        _databaseContext = new DatabaseContext(optionsBuilder.Options);
        _databaseConnection = _databaseContext.Database.GetDbConnection();

        await _databaseConnection.OpenAsync();

        await _databaseContext.Database.EnsureCreatedAsync();

        _respawner = await Respawner.CreateAsync(_databaseConnection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = ["public"]
        });
    }

    public async Task DisposeAsync()
    {
        await _databaseConnection.CloseAsync();
        await _databaseContext.DisposeAsync().AsTask();
    }

    public async Task ResetDatabaseAsync()
    {
        await _respawner.ResetAsync(_databaseConnection);
    }
}

[CollectionDefinition("IntegrationTests")]
public class DatabaseCollection : ICollectionFixture<DatabaseFixture>;