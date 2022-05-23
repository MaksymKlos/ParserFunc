using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ParserFunc.AzureServices.Interfaces;
using ParserFunc.AzureServices.Interfaces.Wrappers;
using ParserFunc.AzureServices.Models;

namespace ParserFunc.AzureServices.Services
{
    public class FileShareService : IFileShareService
    {
        private readonly IShareClient shareClient;
        private readonly IQueueService queueService;
        private readonly IConfiguration configuration;

        public FileShareService(IShareClient client, IQueueService queue, IConfiguration cfg)
        {
            shareClient = client;
            queueService = queue;
            configuration = cfg;
        }

        public async Task UploadToFileShareAsync(string json, string fileName)
        {
            var directoryName = configuration["JsonFileShareDirectoryName"];
            var streamContent = StringToStreamConverter.Convert(json);

            await shareClient.UploadRangeAsync(directoryName, fileName, streamContent);

            await SendPathToQueueAsync(directoryName, fileName);
        }

        private async Task SendPathToQueueAsync(string fileName, string directoryName)
        {
            var message = new FileSharePathMessage
            {
                DirectoryName = directoryName,
                FileName = fileName
            };

            await queueService.SendMessageAsync(message);
        }
    }
}