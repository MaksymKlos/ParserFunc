namespace ParserFunc.FunctionApp.Models
{
    public interface IValidator
    {
        bool ValidateFileName(string fileName, out string error);
    }
}