using System;
using Azure.Storage.Blobs;
using ParserFunc.AzureServices.Interfaces.Wrappers;

namespace ParserFunc.AzureServices.Models.Wrappers
{
    public class BlobContainerClientWrapper : IBlobContainerClient
    {
        private readonly BlobContainerClient blobContainerClient;

        public BlobContainerClientWrapper(BlobContainerClient blobContainerClient)
        {
            this.blobContainerClient = blobContainerClient;
        }


        public IBlobClient GetBlobClient(string blobName)
        {
            if (blobContainerClient == null)
            {
                throw new ArgumentException($"{blobContainerClient} is null");
            }

            var blobClient = blobContainerClient.GetBlobClient(blobName);

            return new BlobClientWrapper(blobClient);
        }
    }
}