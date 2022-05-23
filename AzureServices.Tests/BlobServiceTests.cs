using System;
using System.IO;
using Moq;
using NUnit.Framework;
using ParserFunc.AzureServices.Interfaces.Wrappers;
using ParserFunc.AzureServices.Services;

namespace AzureServices.Tests
{
    [TestFixture]
    public class BlobServiceTests
    {
        private Mock<IBlobClient> blobClient;
        private Mock<IBlobContainerClient> containerClient;
        private BlobService blobService;
        

        [SetUp]
        public void SetUp()
        {
            blobClient = new Mock<IBlobClient>();
            containerClient = new Mock<IBlobContainerClient>();
            blobService = new BlobService(containerClient.Object);

            containerClient
                .Setup(client => client.GetBlobClient(It.IsAny<string>()))
                .Returns(blobClient.Object);
        }

        [Test]
        public void GetStreamByNameAsync_BlobDoesNotExist_ThrowsArgumentException()
        {
            blobClient.Setup(client => client.ExistsAsync()).ReturnsAsync(false);

            Assert.Throws<AggregateException>(() => blobService.GetStreamByNameAsync(It.IsAny<string>()).Wait());
        }

        [Test]
        public void GetStreamByNameAsync_BlobExist_ReturnsStreamFromBlob()
        {
           
            blobClient.Setup(client => client.DownloadContentAsync()).ReturnsAsync(new MemoryStream());

            var result = blobService.GetStreamByNameAsync(It.IsAny<string>()).Result;

            Assert.That(result, Is.InstanceOf<Stream>());
            blobClient.Verify(client => client.DownloadContentAsync(), Times.Once);
        }

    }
}
