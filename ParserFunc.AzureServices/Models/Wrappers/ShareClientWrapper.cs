using System.IO;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Files.Shares;
using ParserFunc.AzureServices.Interfaces.Wrappers;

namespace ParserFunc.AzureServices.Models.Wrappers
{
    public class ShareClientWrapper : IShareClient
    {
        private readonly ShareClient shareClient;

        public ShareClientWrapper(ShareClient shareClient)
        {
            this.shareClient = shareClient;
        }


        public async Task UploadRangeAsync(string directoryName, string fileName, Stream content)
        {
            var directoryClient = shareClient.GetDirectoryClient(directoryName);
            var fileClient = directoryClient.GetFileClient(fileName);

            await fileClient.CreateAsync(content.Length);
            await fileClient.UploadRangeAsync(new HttpRange(0, content.Length), content);
        }
    }
}