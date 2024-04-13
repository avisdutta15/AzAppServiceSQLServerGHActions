namespace App.TestingToolbox;

using App.Data;
using App.Data.Migrations;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Testcontainers.PostgreSql;

public class WithAppContextContainer : IAsyncLifetime
{
    public AppDbContext AppDbContext { get; set; } = null!;
    private readonly PostgreSqlContainer _postgresSqlContainer = new PostgreSqlBuilder().Build();

    public virtual async Task InitializeAsync()
    {
        await _postgresSqlContainer.StartAsync();
        AppDbContext = await GetAppDbContext();
    }

    private async Task<AppDbContext> GetAppDbContext()
    {
        var connectionString = _postgresSqlContainer.GetConnectionString();
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString)
            .EnableDynamicJson()
            .Build();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .EnableServiceProviderCaching(false)
            .UseNpgsql(dataSourceBuilder, sql =>
                sql.MigrationsAssembly(typeof(AppDbContextFactory).Assembly.FullName))
            .Options;

        var context = new AppDbContext(options);
        await context.Database.MigrateAsync();
        return context;
    }

    public virtual async Task DisposeAsync()
    {
        await _postgresSqlContainer.DisposeAsync();
    }
}