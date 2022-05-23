using System;
using Azure.Storage.Blobs;
using Azure.Storage.Files.Shares;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParserFunc.AzureServices.Interfaces;
using ParserFunc.AzureServices.Interfaces.Wrappers;
using ParserFunc.AzureServices.Models.Wrappers;
using ParserFunc.AzureServices.Services;
using ParserFunc.FunctionApp;
using ParserFunc.FunctionApp.Models;
using ParserFunc.JsonParserService;
using ParserFunc.JsonParserService.Interfaces;

[assembly: FunctionsStartup(typeof(Startup))]
namespace ParserFunc.FunctionApp
{
    public class Startup : FunctionsStartup
    {
        private IConfiguration configuration;

        public override void Configure(IFunctionsHostBuilder builder)
        {
            configuration = builder.GetContext().Configuration;

            ConfigureServices(builder.Services);
        }   

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IValidator, JsonValidator>();
            services.AddScoped<IParserService, ParserService>();

            services.AddScoped(GetQueueService);
            services.AddScoped(GetServiceBusClient);
            services.AddScoped(GetFileShareService);
            services.AddScoped(GetBlobService);
        }

        private IBlobService GetBlobService(IServiceProvider arg)
        {
            var connectionString = configuration["AzureWebJobsStorage"];
            var containerName = configuration["JsonContainerName"];
            var blobContainerClient = new BlobContainerClient(connectionString, containerName);

            var clientWrapper = new BlobContainerClientWrapper(blobContainerClient);

            return new BlobService(clientWrapper);
        }

        private IFileShareService GetFileShareService(IServiceProvider arg)
        {
            var connectionString = configuration["AzureWebJobsStorage"];
            var fileShareName = configuration["JsonFileShareName"];
            var shareClient = new ShareClient(connectionString, fileShareName);
            var shareClientWrapper = new ShareClientWrapper(shareClient);

            var queueService = arg.GetRequiredService<IQueueService>();

            return new FileShareService(shareClientWrapper, queueService, configuration);
        }

        private IServiceBusClient GetServiceBusClient(IServiceProvider arg)
        {
            var connectionString = configuration["AzureServiceBusSecret"];

            return new ServiceBusClientWrapper(connectionString);
        }

        private IQueueService GetQueueService(IServiceProvider arg)
        {
            var serviceBusClient = arg.GetRequiredService<IServiceBusClient>();

            return new QueueService(configuration, serviceBusClient);
        }
    }
}