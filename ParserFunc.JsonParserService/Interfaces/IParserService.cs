using System.IO;
using System.Threading.Tasks;

namespace ParserFunc.JsonParserService.Interfaces
{
    public interface IParserService
    {
        Task<string> ReparseJson(Stream stream);
    }
}