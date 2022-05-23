using System.Collections.Generic;
using ParserFunc.JsonParserService.Models.JsonObjects;
using ParserFunc.JsonParserService.Models.JsonObjects.Dependency;
using ParserFunc.JsonParserService.Models.JsonObjects.Structure;
using ParserFunc.JsonParserService.Models.JsonObjects.Value;

namespace JsonParserService.Tests.Helpers
{
    public static class ComparativeHelper
    {
        #region StructureObject

        public static StructureObject GetExpectedStructureObject()
        {
            return new StructureObject
            {
                DefaultLanguageCode = "eng",
                Name = "us-context-of-use",
                OId = "2.16.840.1.113883.3.989.5.1.2.2.1.2.3",
                Version = "3",
                VocabularyType = "structure",
                Nodes = GetStructureNodes()
            };
        }

        private static List<StructureNode> GetStructureNodes()
        {
            return new List<StructureNode>
            {
                new StructureNode
                {
                    NodeTitle = "us_1.1",
                    LinkOId = "2.16.840.1.113883.3.989.5.1.2.2.1.5.3",
                    Descriptions = new List<Description>
                    {
                        new Description { LanguageCode = "eng", Value = "m1.1 forms" },
                        new Description { LanguageCode = "ua", Value = "m1.2 forms" }
                    }
                },
                new StructureNode
                {
                    NodeTitle = "us_1.2",
                    LinkOId = null,
                    Descriptions = new List<Description>
                    {
                        new Description { LanguageCode = "eng", Value = "m1.2 cover letters" }
                    }
                }
            };
        }


        #endregion

        #region ValueObject
        public static ValueObject GetExpectedValueObject()
        {
            return new ValueObject
            {
                DefaultLanguageCode = "eng",
                Name = "us-promotional-document-type",
                OId = "2.16.840.1.113883.3.989.5.1.2.2.1.6.1",
                Version = "1",
                VocabularyType = "value",
                Values = GetExpectedValues()
            };
        }

        private static List<JsonObjectValue> GetExpectedValues()
        {
            return new List<JsonObjectValue>
            {
                new JsonObjectValue
                {
                    OId = "2.16.840.1.113883.3.989.5.1.2.2.1.6.1",
                    Descriptions = GetFirstDescriptions()
                },
                new JsonObjectValue
                {
                    OId = "2.16.840.1.113883.3.989.5.1.2.2.1.6.2",
                    Descriptions = GetSecondDescriptions()
                }
            };
        }

        private static Dictionary<string, List<Description>> GetFirstDescriptions()
        {
            return new Dictionary<string, List<Description>>
            {
                {
                    "us_promo_document_type_1", new List<Description> {
                    new Description{LanguageCode = "eng", Value = "Promotional 2253"} }
                },
                {
                    "us_promo_document_type_2", new List<Description> {
                    new Description{LanguageCode = "eng", Value = "Request For Advisory Launch"} }
                }
            };
        }

        private static Dictionary<string, List<Description>> GetSecondDescriptions()
        {
            return new Dictionary<string, List<Description>>
            {
                {"us_promo_document_type_1", new List<Description> {
                    new Description{LanguageCode = "eng", Value = "Promotional 2253"} }
                }
            };
        }

        #endregion

        #region DependencyObject

        public static DependencyObject GetExpectedDependencyObject()
        {
            return new DependencyObject
            {
                DefaultLanguageCode = "eng",
                Name = "usfda-application-type",
                OId = "2.16.840.1.113883.3.989.5.1.2.2.1.1.2",
                Version = "2",
                VocabularyType = "dependency",
                Values = GetDependencyValues()
            };
        }

        private static List<DependencyValue> GetDependencyValues()
        {
            return new List<DependencyValue>
            {
                new DependencyValue
                {
                    OId = "2.16.840.1.113883.3.989.5.1.2.2.1.1.2",
                    ApplicationTypes = GetFirstAppTypes()
                },
                new DependencyValue
                {
                    OId = "2.16.840.1.113883.3.989.5.1.2.2.1.1.3",
                    ApplicationTypes = GetSecondAppTypes()
                }
            };
        }

        private static List<ApplicationType> GetFirstAppTypes()
        {
            return new List<ApplicationType>
            {
                new ApplicationType
                {
                    Name = "us_application_type_1",
                    Descriptions = new List<Description>
                    {
                        new Description{LanguageCode = "eng", Value = "New Drug Application (NDA)"}
                    },
                    DependsOf = GetFirstDependsOfValue()
                },
                new ApplicationType
                {
                    Name = "us_application_type_2",
                    Descriptions = new List<Description>
                    {
                        new Description{LanguageCode = "eng", Value = "Abbreviated New Drug Application (ANDA)"}
                    },
                    DependsOf = GetSecondDependsOfValue()
                }
            };
        }

        private static JsonObjectValue GetFirstDependsOfValue()
        {
            return new JsonObjectValue
            {
                OId = "2.16.840.1.113883.3.989.5.1.2.2.1.13.1",
                Descriptions = new Dictionary<string, List<Description>>
                {
                    {
                        "original", new List<Description> {
                            new Description{LanguageCode = "eng", Value = "Original Application"} }
                    },
                    {
                        "efficacySupplement", new List<Description> {
                            new Description{LanguageCode = "eng", Value = "Efficacy Supplement"} }
                    }
                }
            };
        }

        private static JsonObjectValue GetSecondDependsOfValue()
        {
            return new JsonObjectValue
            {
                OId = "2.16.840.1.113883.3.989.5.1.2.2.1.13.2",
                Descriptions = new Dictionary<string, List<Description>>
                {
                    {
                        "original", new List<Description> {
                            new Description{LanguageCode = "eng", Value = "Original Application"} }
                    }
                }
            };
        }

        private static List<ApplicationType> GetSecondAppTypes()
        {
            return new List<ApplicationType>
            {
                new ApplicationType
                {
                    Name = "us_application_type_1",
                    Descriptions = new List<Description>
                    {
                        new Description{LanguageCode = "eng", Value = "New Drug Application (NDA)"}
                    },
                    DependsOf = null
                }
            };
        }

        #endregion
    }
}