using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using JsonParserService.Tests.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using ParserFunc.JsonParserService.Helpers;
using ParserFunc.JsonParserService.Models.JsonObjects.Dependency;
using ParserFunc.JsonParserService.Parsers;

namespace JsonParserService.Tests.ParsersTests
{
    [TestFixture]
    public class DependencyJsonParserTests
    {
        private const string TestFilePath = "../../../TestJsons/input-dependency.json";
        private JToken jToken;

        private DependencyJsonParser parser;

        [SetUp]
        public void SetUp()
        {
            parser = new DependencyJsonParser();

            using var fs = new FileStream(TestFilePath, FileMode.Open);
            using var reader = new StreamReader(fs);

            jToken = JToken.ReadFrom(new JsonTextReader(reader));
        }

        [Test]
        public void ParseJson_ValidFile_ReturnsStructureObject()
        {
            var result = (DependencyObject)parser.ParseJson(jToken);

            var expectedObject = ComparativeHelper.GetExpectedDependencyObject();

            result.Should().BeEquivalentTo(expectedObject, options
                => options.IncludingNestedObjects());
        }

        [Test]
        [TestCase("description")]
        [TestCase("default-language-code")]
        [TestCase("vocabulary-type")]
        [TestCase("values")]
        public void ParseJson_CannotFindDocumentProperty_ThrowsArgumentNullException(string name)
        {
            var prop = jToken.First(v => v.GetPropertyName() == name);

            prop.Remove();

            Assert.Throws<ArgumentNullException>(() => parser.ParseJson(jToken));
        }


        [Test]
        [TestCase("name")]
        [TestCase("oid")]
        [TestCase("version")]
        public void ParseJson_CannotFindDocumentDescProperty_ThrowsArgumentNullException(string name)
        {
            var prop = jToken.First.Values().First(v => v.GetPropertyName() == name);

            prop.Remove();

            Assert.Throws<ArgumentNullException>(() => parser.ParseJson(jToken));
        }

    }
}