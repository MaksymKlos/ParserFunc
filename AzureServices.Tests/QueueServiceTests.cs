using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using ParserFunc.AzureServices.Interfaces.Wrappers;
using ParserFunc.AzureServices.Services;

namespace AzureServices.Tests
{
    [TestFixture]
    public class QueueServiceTests
    {
        private Mock<IConfiguration> configuration;
        private Mock<IServiceBusClient> serviceBusClient;

        private QueueService queueService;


        [SetUp]
        public void SetUp()
        {
            configuration = new Mock<IConfiguration>();
            serviceBusClient = new Mock<IServiceBusClient>();

            queueService = new QueueService(configuration.Object, serviceBusClient.Object);
        }

        [Test]
        public void SendMessageAsync_SuccessfulSendingQueue_SendsMessageToQueue()
        {
            const string queueName = "queueName";

            configuration.Setup(conf => conf["JsonQueueName"]).Returns(queueName);

            queueService.SendMessageAsync(It.IsAny<ServiceBusMessage>()).Wait();

            serviceBusClient.Verify(client => 
                client.SendMessageToQueueAsync(It.IsAny<ServiceBusMessage>(), queueName), Times.Once);
        }
    }
}