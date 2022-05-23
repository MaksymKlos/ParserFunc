using System.Collections.Generic;
using Newtonsoft.Json;

namespace ParserFunc.JsonParserService.Models.JsonObjects.Value
{
    public class ValueObject : JsonObject
    {
        [JsonProperty(Order = 6)]
        public List<JsonObjectValue> Values { get; set; }
    }
}