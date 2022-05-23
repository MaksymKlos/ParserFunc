using System.IO;

namespace JsonParserService.Tests.Helpers
{
    public static class FileReader
    {
        public static string ReadTextFromFile(string path)
        {
            using var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            using var streamReader = new StreamReader(fileStream);

            return streamReader.ReadToEnd();
        }
    }
}