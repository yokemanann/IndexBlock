// create hosting object and DI layer
using IndexBlock;
using IndexBlock.Common.Config;
using IndexBlock.Common.Extensions;
using IndexBlock.Common.Logger;
using IndexBlock.Models;
using IndexBlock.Repositories;
using IndexBlock.Repositories.Base;
using IndexBlock.Services;
using IndexBlock.Services.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var configBuilder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json").Build();

using IHost host = CreateHostBuilder(args, configBuilder).Build();

// create a service scope
using var scope = host.Services.CreateScope();

var services = scope.ServiceProvider;

try
{
    var db = services.GetRequiredService<IndexBlock_DBContext>();
    db.Database.Migrate(); // Run Migration to create database and tables
    await services.GetRequiredService<App>().Run(args);
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

IHostBuilder CreateHostBuilder(string[] strings, IConfiguration configBuilder)
{
    return Host.CreateDefaultBuilder()
        .ConfigureServices((_, services) =>
        {
            services.AddScoped<App>();
            services.Configure<AlchemyAPI>(configBuilder.GetSection("AlchemyAPI"));
            services.Configure<CustomLoggerOptions>(configBuilder.GetSection("Logging").GetSection("CustomFileLogger"));

            services.AddDbContext<IndexBlock_DBContext>(
                options => options.UseMySql(
                    configBuilder.GetConnectionString("Default"), 
                    ServerVersion.AutoDetect(configBuilder.GetConnectionString("Default"))),
                ServiceLifetime.Scoped);

            services.AddHttpClient();

            services.AddScoped<IndexBlock_DBContext>();
            services.AddScoped<IRepository<Blocks>, Repository<Blocks>>();
            services.AddScoped<IRepository<Transactions>, Repository<Transactions>>();

            services.AddTransient<IEthClient, AlchemyClient>();
            services.AddTransient<IEthRpcJsonService, AlchemyService>();
        })
        .ConfigureLogging((hostBuilderContext, logging) =>
        {
            logging.AddCustomFileLogger(options =>
            {
                hostBuilderContext.Configuration.GetSection("Logging").GetSection("CustomFileLogger").GetSection("Options").Bind(options);
            });

            logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
            logging.AddFilter("System.Net.Http.HttpClient", LogLevel.Warning);
        });
}