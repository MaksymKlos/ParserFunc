using System.Collections.Generic;

namespace ParserFunc.JsonParserService.Models.JsonObjects.Dependency
{
    public class ApplicationType
    {
        public string Name { get; set; }

        public List<Description> Descriptions { get; set; }

        public JsonObjectValue DependsOf { get; set; }
    }
}