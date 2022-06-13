using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using WorkerDemo;
using WorkerDemo.Services;
using WorkerDemo.Services.Interfaces;

# region Configuration
static void BuildConfiguration(IConfigurationBuilder builder)
{
    builder.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{GetDotNetEnvironment()}.json", optional: true)
        .AddEnvironmentVariables();
}

static string GetDotNetEnvironment()
{
    return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
}
#endregion

var builder = new ConfigurationBuilder();
BuildConfiguration(builder);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Build())
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        services.AddTransient<IService, Service>();
        services.AddHostedService<Worker>();
    })
    .UseSerilog()
    .Build();

await host.RunAsync();