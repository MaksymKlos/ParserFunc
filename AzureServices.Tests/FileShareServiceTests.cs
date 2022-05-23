using System.IO;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using ParserFunc.AzureServices.Interfaces;
using ParserFunc.AzureServices.Interfaces.Wrappers;
using ParserFunc.AzureServices.Models;
using ParserFunc.AzureServices.Services;

namespace AzureServices.Tests
{
    [TestFixture]
    public class FileShareServiceTests
    {
        private Mock<IShareClient> shareClient;
        private Mock<IQueueService> queueService;
        private Mock<IConfiguration> configuration;

        private FileShareService fileShareService;

        [SetUp]
        public void SetUp()
        {
            shareClient = new Mock<IShareClient>();
            queueService = new Mock<IQueueService>();
            configuration = new Mock<IConfiguration>();

            fileShareService = new FileShareService(shareClient.Object, queueService.Object, configuration.Object);
        }

        [Test]
        public void UploadToFileShareAsync_SuccessfulUploading_UploadFileAndSendMessageToQueue()
        {
            const string directoryName = "directoryName";
            const string fileName = "fileName";
            const string json = "json";

            configuration.Setup(conf => conf["JsonFileShareDirectoryName"]).Returns(directoryName);


            fileShareService.UploadToFileShareAsync(json, fileName).Wait();
            
            shareClient.Verify(client => 
                client.UploadRangeAsync(directoryName, fileName, It.IsAny<Stream>()), Times.Once);

            queueService.Verify(service => 
                service.SendMessageAsync(It.IsAny<FileSharePathMessage>()), Times.Once);
        }
    }
}