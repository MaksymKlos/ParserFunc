using Newtonsoft.Json.Linq;
using ParserFunc.JsonParserService.Helpers;
using ParserFunc.JsonParserService.Interfaces;
using ParserFunc.JsonParserService.Models.JsonObjects;

namespace ParserFunc.JsonParserService.Parsers
{
    public abstract class ParserJson : IParser
    {
        public abstract JsonObject ParseJson(JToken jToken);

        private protected void ParseDocumentProperties(JsonObject parsedObject, JToken jToken)
        {
            parsedObject.Name = jToken.GetRequiredParameter("description.name");
            parsedObject.OId = jToken.GetRequiredParameter("description.oid");
            parsedObject.Version = jToken.GetRequiredParameter("description.version");
            parsedObject.DefaultLanguageCode = jToken.GetRequiredParameter("default-language-code");
            parsedObject.VocabularyType = jToken.GetRequiredParameter("vocabulary-type");
        }

    }
}
