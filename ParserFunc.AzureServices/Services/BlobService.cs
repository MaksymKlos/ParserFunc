using System;
using System.IO;
using System.Threading.Tasks;
using ParserFunc.AzureServices.Interfaces;
using ParserFunc.AzureServices.Interfaces.Wrappers;

namespace ParserFunc.AzureServices.Services
{
    public class BlobService : IBlobService
    {
        private readonly IBlobContainerClient blobContainerClient;

        public BlobService(IBlobContainerClient blobContainerClient)
        {
            this.blobContainerClient = blobContainerClient;
        }

        public async Task<Stream> GetStreamByNameAsync(string blobName)
        {
            var blob = blobContainerClient.GetBlobClient(blobName);

            if (!await blob.ExistsAsync())
            {
                throw new ArgumentException($"File {blobName} cannot be find", nameof(blobName));
            }

            return await blob.DownloadContentAsync();
        }
    }
}