using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace ParserFunc.AzureServices.Interfaces.Wrappers
{
    public interface IServiceBusClient
    {
        Task SendMessageToQueueAsync(ServiceBusMessage message, string queueName);
    }
}