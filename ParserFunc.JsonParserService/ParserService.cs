using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ParserFunc.JsonParserService.Helpers;
using ParserFunc.JsonParserService.Interfaces;
using ParserFunc.JsonParserService.Models.JsonObjects;
using ParserFunc.JsonParserService.Parsers;

namespace ParserFunc.JsonParserService
{
    public class ParserService : IParserService
    {
        private readonly Dictionary<string, IParser> parsersDictionary
            = new Dictionary<string, IParser>
            {
                {"structure", new StructureJsonParser()},
                {"value", new ValueJsonParser()},
                {"dependency", new DependencyJsonParser()}
            };


        public async Task<string> ReparseJson(Stream stream)
        {

            var parsedObject = await ParseJsonFile(stream);

            var serializedObject = await SerializeParsedObject(parsedObject);

            return serializedObject;
        }


        private async Task<JsonObject> ParseJsonFile(Stream stream)
        {
            using var streamReader = new StreamReader(stream);
            var jToken = await JToken.ReadFromAsync(new JsonTextReader(streamReader));

            var vocabularyType = jToken.GetRequiredParameter("vocabulary-type");

            if (!parsersDictionary.TryGetValue(vocabularyType, out var parser))
            {
                throw new ArgumentException($"{vocabularyType} is unknown vocabulary type");
            }

            return await Task.Run(() => parser.ParseJson(jToken));
        }

        private async Task<string> SerializeParsedObject(JsonObject parsedObject)
        {
            var serializingSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            };

            var serializationTask = Task.Run(() =>
                JsonConvert.SerializeObject(parsedObject, serializingSettings));

            return await serializationTask;
        }
    }
}