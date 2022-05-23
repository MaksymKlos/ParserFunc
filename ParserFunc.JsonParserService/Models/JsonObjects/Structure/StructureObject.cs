using System.Collections.Generic;
using Newtonsoft.Json;

namespace ParserFunc.JsonParserService.Models.JsonObjects.Structure
{
    public class StructureObject : JsonObject
    {
        [JsonProperty(Order = 6)]
        public List<StructureNode> Nodes { get; set; }
    }
}