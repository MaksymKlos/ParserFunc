using System;
using Newtonsoft.Json;

namespace ParserFunc.JsonParserService.Models.JsonObjects
{
    [Serializable]
    public class JsonObject
    {
        [JsonProperty(Order = 1)]
        public string Name { get; set; }


        [JsonProperty(Order = 2)]
        public string OId { get; set; }


        [JsonProperty(Order = 3)]
        public string Version { get; set; }


        [JsonProperty(Order = 4)]
        public string DefaultLanguageCode { get; set; }


        [JsonProperty(Order = 5)]
        public string VocabularyType { get; set; }

    }
}