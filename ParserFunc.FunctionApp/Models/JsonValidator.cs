namespace ParserFunc.FunctionApp.Models
{
    public class JsonValidator : IValidator
    {
        public bool ValidateFileName(string fileName, out string error)
        {
            error = string.Empty;

            var isFileValid = !string.IsNullOrEmpty(fileName) && fileName.EndsWith(".json");

            if (!isFileValid)
            {
                error = $"Incorrect json name {fileName}";
            }

            return isFileValid;
        }
    }
}