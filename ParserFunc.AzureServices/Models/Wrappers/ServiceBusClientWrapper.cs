using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using ParserFunc.AzureServices.Interfaces.Wrappers;

namespace ParserFunc.AzureServices.Models.Wrappers
{
    public class ServiceBusClientWrapper : IServiceBusClient
    {
        private readonly string connectionString;

        public ServiceBusClientWrapper(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task SendMessageToQueueAsync(ServiceBusMessage message, string queueName)
        {
            await using var client = new ServiceBusClient(connectionString);

            var sender = client.CreateSender(queueName);

            await sender.SendMessageAsync(message);
        }
    }
}