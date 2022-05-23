using ParserFunc.AzureServices.Models.Wrappers;

namespace ParserFunc.AzureServices.Interfaces.Wrappers
{
    public interface IBlobContainerClient
    {
        IBlobClient GetBlobClient(string blobName);
    }
}