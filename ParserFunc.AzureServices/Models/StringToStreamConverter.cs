using System.IO;

namespace ParserFunc.AzureServices.Models
{
    public static class StringToStreamConverter
    {
        public static Stream Convert(string str)
        {
            var memStream = new MemoryStream();
            var writer = new StreamWriter(memStream);

            writer.Write(str);
            writer.Flush();

            memStream.Position = 0;

            return memStream;
        }
    }
}