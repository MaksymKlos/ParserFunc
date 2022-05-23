using System.IO;
using JsonParserService.Tests.Helpers;
using NUnit.Framework;
using ParserFunc.JsonParserService;

namespace JsonParserService.Tests
{
    [TestFixture]
    public class ParserServiceTests
    {
        private const string InputFilePath = "../../../TestJsons/input-{0}.json";
        private const string OutputFilePath = "../../../TestJsons/output-{0}.json";

        private FileStream stream;
        private ParserService parserService;

        [SetUp]
        public void SetUp()
        {
            parserService = new ParserService();
        }

        [TearDown]
        public void TearDown()
        {
            stream?.Dispose();
        }

        [Test]
        [TestCase("structure")]
        [TestCase("value")]
        [TestCase("dependency")]
        public void ReparseJson_ValidStream_ReturnsSerializedJsonObject(string name)
        {
            var inputPath = string.Format(InputFilePath, name);
            var outputPath = string.Format(OutputFilePath, name);
            var expectedResult = FileReader.ReadTextFromFile(outputPath);


            stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read);
            var result = parserService.ReparseJson(stream).Result;


            Assert.AreEqual(expectedResult, result);
        }
    }
}