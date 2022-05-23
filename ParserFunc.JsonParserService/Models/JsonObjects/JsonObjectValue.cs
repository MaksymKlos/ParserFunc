using System.Collections.Generic;

namespace ParserFunc.JsonParserService.Models.JsonObjects
{
    public class JsonObjectValue
    {
        public string OId { get; set; }

        public Dictionary<string, List<Description>> Descriptions { get; set; }
    }
}