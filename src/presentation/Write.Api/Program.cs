using Data.PostgreSQL;
using Microsoft.OpenApi.Models;
using Write.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = $"Transaction Write Api",
        Version = "v1",
    });
    
});

builder.Services.AddPostgreSQLServices(builder.Configuration);

var app = builder.Build();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(x =>
{
    x.DefaultModelsExpandDepth(-1);
});

app.UseMiddleware<DatabaseInstallerMiddleware>();
app.UseStaticFiles();
app.MapControllers();
app.Run();
