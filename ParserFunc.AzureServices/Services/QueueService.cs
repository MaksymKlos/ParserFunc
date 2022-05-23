using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ParserFunc.AzureServices.Interfaces;
using ParserFunc.AzureServices.Interfaces.Wrappers;

namespace ParserFunc.AzureServices.Services
{
    public class QueueService : IQueueService
    {
        private readonly IConfiguration configuration;
        private readonly IServiceBusClient serviceBusClient;

        public QueueService(IConfiguration configuration, IServiceBusClient serviceBusClient)
        {
            this.configuration = configuration;
            this.serviceBusClient = serviceBusClient;
        }

        public async Task SendMessageAsync<T>(T message)
        {
            var serviceBusMessage = GetServiceBusMessage(message);
            var queueName = configuration["JsonQueueName"];

            await serviceBusClient.SendMessageToQueueAsync(serviceBusMessage, queueName);
        }

        private ServiceBusMessage GetServiceBusMessage<T>(T pathMessage)
        {
            var jsonMessage = JsonConvert.SerializeObject(pathMessage, Formatting.Indented);

            var message = new ServiceBusMessage(jsonMessage);

            return message;
        }
    }
}