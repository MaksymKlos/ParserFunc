using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using ParserFunc.JsonParserService.Models.JsonObjects;

namespace ParserFunc.JsonParserService.Helpers
{
    public static class JTokenExtension
    {
        public static string GetRequiredParameter(this JToken jToken, string parameterKey)
        {
            var param = jToken.SelectToken(parameterKey);

            if (param == null) throw new ArgumentNullException($"Can't find {parameterKey}.");

            return param.ToString();
        }

        public static JToken GetChildToken(this JToken jToken, string selectionToken)
        {
            var result = jToken?.First?.SelectToken(selectionToken);

            if (result == null) throw new ArgumentNullException($"Can't find {selectionToken}.");

            return result;
        }

        public static string GetNullableParameter(this JToken jToken, string parameterKey)
        {
            return jToken?.SelectToken(parameterKey)?.ToString();
        }

        public static JEnumerable<JToken> GetChildNodes(this JToken jToken, string selectToken)
        {
            var jsonNodes = jToken?.SelectToken(selectToken);

            if (jsonNodes != null) return jsonNodes.Children();

            throw new ArgumentNullException($"Can't find {selectToken}.");
        }

        public static string GetPropertyName(this JToken jToken)
        {
            return ((JProperty)jToken).Name;
        }

        public static List<Description> GetDescriptions(this JToken jToken)
        {
            var descriptions = jToken.GetChildToken("description").Children();

            return descriptions.Select(child => new Description
            {
                LanguageCode = child.GetPropertyName(),
                Value = child.First.GetRequiredParameter("value")
            }).ToList();
        }
    }
}