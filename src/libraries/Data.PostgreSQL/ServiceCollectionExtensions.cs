using Data.PostgreSQL.Business;
using Data.PostgreSQL.Business.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data.PostgreSQL;

public static class ServiceCollectionExtensions
{
    public static void AddPostgreSQLServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Business
        services.AddScoped<ITransactionBusiness, TransactionBusiness>();
        
        // Entity Framework & Repository
        services.AddDbContext<TransactionDbContext>(
            (serviceProvider, dbContextBuilder) =>
            {
                dbContextBuilder.UseNpgsql(configuration.GetConnectionString("WriteDbConnection"),
                                optionsBuilder =>
                                {
                                    
                                    optionsBuilder.MigrationsAssembly("Write.Api");
                                    optionsBuilder.MigrationsHistoryTable("__EFMigrationsHistory", "public");
                                }).UseSnakeCaseNamingConvention();
            });
    }
}
