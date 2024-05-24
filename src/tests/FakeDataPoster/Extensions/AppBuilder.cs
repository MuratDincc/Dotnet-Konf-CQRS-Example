using System.Text.Json;
using System.Text.Json.Serialization;
using FakeDataPoster.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace FakeDataPoster.Extensions;

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

        IConfiguration configuration = builder.Build(); 

        #endregion

        #region Dependency Injection

        var serviceProvider = new ServiceCollection();

        // Services
        var refitSettings = new RefitSettings
        {
            ContentSerializer = new SystemTextJsonContentSerializer(new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNameCaseInsensitive = true
            })
        };
        
        var transactionServiceUrl = configuration.GetValue<string>("ServiceUrls:Transaction");
        serviceProvider.AddRefitClient<ITransactionService>(refitSettings).ConfigureHttpClient(c =>
        {
            c.BaseAddress = new Uri(transactionServiceUrl);
            c.Timeout = TimeSpan.FromSeconds(20.0);
        });
        
        #endregion

        return serviceProvider.BuildServiceProvider();
    }
}