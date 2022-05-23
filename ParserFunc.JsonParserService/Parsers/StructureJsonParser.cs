using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using ParserFunc.JsonParserService.Helpers;
using ParserFunc.JsonParserService.Models.JsonObjects;
using ParserFunc.JsonParserService.Models.JsonObjects.Structure;

namespace ParserFunc.JsonParserService.Parsers
{
    public class StructureJsonParser : ParserJson
    {
        public override JsonObject ParseJson(JToken jToken)
        {
            var structure = new StructureObject
            {
                Nodes = GetChildNodes(jToken.GetChildNodes("nodes"))
            };

            ParseDocumentProperties(structure, jToken);
            
            return structure;
        }

        private List<StructureNode> GetChildNodes(JEnumerable<JToken> nodes)
        {
            return nodes.Select(node => new StructureNode
            {
                NodeTitle = node.GetPropertyName(),
                LinkOId = node.First.GetNullableParameter("fields.formType.link"),
                Descriptions = node.GetDescriptions()
            }).ToList();
        }
    }
}