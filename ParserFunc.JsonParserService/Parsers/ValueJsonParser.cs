using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using ParserFunc.JsonParserService.Helpers;
using ParserFunc.JsonParserService.Models.JsonObjects;
using ParserFunc.JsonParserService.Models.JsonObjects.Value;

namespace ParserFunc.JsonParserService.Parsers
{
    public class ValueJsonParser : ParserJson
    {
        public override JsonObject ParseJson(JToken jToken)
        {
            var valueObject = new ValueObject
            {
                Values = GetValues(jToken.GetChildNodes("values"))
            };

            ParseDocumentProperties(valueObject, jToken);
            
            return valueObject;
        }

        private List<JsonObjectValue> GetValues(JEnumerable<JToken> values)
        {
            return values.Select(value => new JsonObjectValue
            {
                OId = value.GetPropertyName(),
                Descriptions = value.First.Children()
                    .ToDictionary
                    (
                        jToken=> jToken.GetPropertyName(),
                        jToken => jToken.GetDescriptions()
                    )
            }).ToList();
        }
    }
}