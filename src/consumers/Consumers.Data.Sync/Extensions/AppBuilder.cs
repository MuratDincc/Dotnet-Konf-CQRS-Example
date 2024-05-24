using Consumers.Data.Sync.Configuration;
using Data.MongoDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Consumers.Data.Sync.Extensions;

public static class AppBuilder
{
    public static ServiceProvider InitApp()
    {
        #region Configuration

        var environmentName = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

        var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        IConfiguration config = builder.Build(); 

        #endregion

        #region Dependency Injection

        var serviceProvider = new ServiceCollection();

        serviceProvider.AddSingleton<KafkaSettings>(config.GetSection("KafkaSettings").Get<KafkaSettings>());
        
        serviceProvider.AddMongoDBServices(config);
        
        #endregion

        return serviceProvider.BuildServiceProvider();
    }
}

