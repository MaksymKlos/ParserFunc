using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using ParserFunc.JsonParserService.Helpers;
using ParserFunc.JsonParserService.Models.JsonObjects;
using ParserFunc.JsonParserService.Models.JsonObjects.Dependency;

namespace ParserFunc.JsonParserService.Parsers
{
    public class DependencyJsonParser : ParserJson
    {
        public override JsonObject ParseJson(JToken jToken)
        {
            var dependencyObject = new DependencyObject
            {
                Values = GetValues(jToken.GetChildNodes("values"))
            };

            ParseDocumentProperties(dependencyObject, jToken);

            return dependencyObject;
        }

        private List<DependencyValue> GetValues(JEnumerable<JToken> nodes)
        {
            var appTypes = nodes.Where(node => IsApplicationType(node.First.First));
            var dependedNodes = nodes.Where(node => !IsApplicationType(node.First.First));

            return appTypes
                .Select(node => new DependencyValue
                {
                    OId = node.GetPropertyName(),
                    ApplicationTypes = GetAppTypes(node.Values(), dependedNodes)
                }).ToList();
        }

        private bool IsApplicationType(JToken jToken)
        {
            return jToken.GetPropertyName().Contains("us_application_type");
        }

        private List<ApplicationType> GetAppTypes(IEnumerable<JToken> nodes, IEnumerable<JToken> depends)
        {
            return nodes.Select(node => new ApplicationType
            {
                Name = node.GetPropertyName(),
                Descriptions = node.GetDescriptions(),
                DependsOf = GetDependsOfValue(node, depends)
            }).ToList();
        }

        private JsonObjectValue GetDependsOfValue(JToken jToken, IEnumerable<JToken> dependedNodes)
        {
            var dependsOfNode = jToken.GetChildToken("dependsOf");
            if (!dependsOfNode.HasValues) return null;

            var dependsOfOid = dependsOfNode.First.GetPropertyName();
            var possibleKeys = dependsOfNode.Values().Children()
                .Select(value => value.ToString());
            
            return new JsonObjectValue
            {
                OId = dependsOfOid,
                Descriptions = GetDependedValues(dependedNodes, dependsOfOid, possibleKeys)
            };
        }

        private Dictionary<string, List<Description>> GetDependedValues(IEnumerable<JToken> nodes, string oId, IEnumerable<string> possibleKeys)
        {
            return nodes
                .Where(node => node.GetPropertyName() == oId)
                .SelectMany(node => node.Values())
                .Where(node => possibleKeys.Contains(node.GetPropertyName()))
                .ToDictionary
                (
                    node => node.GetPropertyName(),
                    node => node.GetDescriptions()
                );
        }
    }
}