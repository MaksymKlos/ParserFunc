using System.IO;
using System.Threading.Tasks;

namespace ParserFunc.AzureServices.Interfaces
{
    public interface IBlobService
    {
        Task<Stream> GetStreamByNameAsync(string blobName); 
    }
}