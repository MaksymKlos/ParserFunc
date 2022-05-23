using System.Collections.Generic;
using Newtonsoft.Json;

namespace ParserFunc.JsonParserService.Models.JsonObjects.Dependency
{
    public class DependencyObject : JsonObject
    {
        [JsonProperty(Order = 6)]
        public List<DependencyValue> Values { get; set; }
    }
}