using System.Threading.Tasks;

namespace ParserFunc.AzureServices.Interfaces
{
    public interface IQueueService
    {
        Task SendMessageAsync<T>(T message);
    }
}