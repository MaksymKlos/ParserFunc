using System.Threading.Tasks;

namespace ParserFunc.AzureServices.Interfaces
{
    public interface IFileShareService
    {
        Task UploadToFileShareAsync(string json, string fileName);
    }
}