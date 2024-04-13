namespace App.Service; 

using App.Services.Repositories;
using Azure.Core;
using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

public static class ServiceCollectionExtensions
{
    private static readonly DefaultAzureCredential tokenProvider = new();
    private static readonly TokenRequestContext tokenRequestContext = new(
        [
           "https://ossrdbms-aad.database.windows.net/.default"
        ]);

    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        //Add service registrations in DI here.
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        
        return services;
    }

    public static IServiceCollection AddPostgresDbContext<TContext>(this IServiceCollection services, string? connectionString)
        where TContext : DbContext
    {
        var dataSource = CreateDataSource(connectionString);

        services.AddDbContext<TContext>(options => 
        {
            options.UseNpgsql(dataSource);
        });

        return services;
    }

    //https://www.npgsql.org/doc/security.html#auth-token-rotation-and-dynamic-password
    private static NpgsqlDataSource CreateDataSource(string? connectionString)
    {
        return new NpgsqlDataSourceBuilder(connectionString)
                  .UsePeriodicPasswordProvider(
                     async (settings, ct) =>
                     {
                         var accessToken = await tokenProvider.GetTokenAsync(tokenRequestContext, ct);
                         return accessToken.Token;
                     }
                   ,  TimeSpan.FromMinutes(55)  // Interval for refreshing the token
                   ,  TimeSpan.FromSeconds(5)   // Interval for retrying after a refresh failure)
                   )  
                  .EnableDynamicJson()
                  .Build();
    }
}
