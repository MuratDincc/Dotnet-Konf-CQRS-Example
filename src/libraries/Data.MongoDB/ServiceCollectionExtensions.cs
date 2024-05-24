using Data.MongoDB.Business;
using Data.MongoDB.Business.Abstracts;
using Data.MongoDB.Configuration;
using Data.MongoDB.Repository;
using Data.MongoDB.Repository.Abstracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data.MongoDB;

public static class ServiceCollectionExtensions
{
    public static void AddMongoDBServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Business
        services.AddScoped<ITransactionBusiness, TransactionBusiness>();
        
        // MongoDB
        services.AddSingleton<MongoDbSettings>(configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>());
        services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));
    }
}
