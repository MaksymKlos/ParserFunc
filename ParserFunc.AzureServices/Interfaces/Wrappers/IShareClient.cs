using System.IO;
using System.Threading.Tasks;

namespace ParserFunc.AzureServices.Interfaces.Wrappers
{
    public interface IShareClient
    {
        Task UploadRangeAsync(string directoryName, string fileName, Stream content);
    }
}