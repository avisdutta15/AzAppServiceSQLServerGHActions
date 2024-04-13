namespace App.Data.Migrations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(configuration.GetConnectionString("AppDb"), sql =>
            {
                sql.MigrationsHistoryTable("__efmigrations_appdb", AppDbContextConstants.Schemas.Organization);
                sql.MigrationsAssembly(typeof(AppDbContextFactory).Assembly.FullName);
            });

        return new AppDbContext(optionsBuilder.Options);
    }
}