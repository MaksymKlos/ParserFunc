using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using ParserFunc.AzureServices.Interfaces.Wrappers;

namespace ParserFunc.AzureServices.Models.Wrappers
{
    public class BlobClientWrapper : IBlobClient
    {
        private readonly BlobClient blobClient;

        public BlobClientWrapper(BlobClient blobClient)
        {
            this.blobClient = blobClient;
        }


        public async Task<bool> ExistsAsync()
        {
            var exists = await blobClient.ExistsAsync();

            return exists.Value;
        }

        public async Task<Stream> DownloadContentAsync()
        {
            var response = await blobClient.DownloadAsync();
            var blobDownloadInfo = response.Value;

            return blobDownloadInfo.Content;
        }
    }
}