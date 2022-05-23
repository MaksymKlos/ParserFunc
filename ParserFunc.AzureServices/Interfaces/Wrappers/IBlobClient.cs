using System.IO;
using System.Threading.Tasks;

namespace ParserFunc.AzureServices.Interfaces.Wrappers
{
    public interface IBlobClient
    {
        Task<bool> ExistsAsync();

        Task<Stream> DownloadContentAsync();
    }
}