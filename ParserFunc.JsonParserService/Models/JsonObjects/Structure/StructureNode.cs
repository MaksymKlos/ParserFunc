using System.Collections.Generic;

namespace ParserFunc.JsonParserService.Models.JsonObjects.Structure
{
    public class StructureNode
    {
        public string NodeTitle { get; set; }

        public string LinkOId { get; set; }

        public List<Description> Descriptions { get; set; }
    }
}