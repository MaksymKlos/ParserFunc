using Newtonsoft.Json.Linq;
using ParserFunc.JsonParserService.Models.JsonObjects;

namespace ParserFunc.JsonParserService.Interfaces
{
    public interface IParser
    {
        JsonObject ParseJson(JToken jToken);
    }
}