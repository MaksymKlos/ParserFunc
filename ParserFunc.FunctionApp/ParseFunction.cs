using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ParserFunc.AzureServices.Interfaces;
using ParserFunc.FunctionApp.Models;
using ParserFunc.JsonParserService.Interfaces;

namespace ParserFunc.FunctionApp
{
    public class ParseFunction
    {
        private readonly IParserService parserService;
        private readonly IBlobService blobService;
        private readonly IFileShareService fileShareService;
        private readonly IValidator jsonValidator;


        public ParseFunction(
            IValidator validator, 
            IFileShareService fileShare,
            IBlobService blob,
            IParserService parser)
        {
            parserService = parser;
            blobService = blob;
            fileShareService = fileShare;
            jsonValidator = validator;
        }



        [FunctionName("ParseJson")]
        public async Task<IActionResult> ParseJson(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "parseJson/{name}")] HttpRequest req,
            string name,
            ILogger log)
        {
            try
            {
                if (!jsonValidator.ValidateFileName(name, out var error))
                {
                    throw new ArgumentException(error, nameof(name));
                }

                var blob = await blobService.GetStreamByNameAsync(name);
                var resultJson = await parserService.ReparseJson(blob);
                
                await fileShareService.UploadToFileShareAsync(resultJson, name);

                return new OkObjectResult(LogSuccess(log, name));
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(LogError(log, e.Message, name));
            }
        }


        private static string LogSuccess(ILogger logger, string fileName)
        {
            var successMessage = $"File {fileName} was successfully parsed.";

            logger.LogInformation($"{successMessage}: {DateTime.Now}");

            return successMessage;
        }

        private static string LogError(ILogger logger, string error, string fileName)
        {
            var errorMessage = $"Error occurred while parsing {fileName}.";

            logger.LogError($"{error}: {DateTime.Now}");

            return errorMessage;
        }
    }
}
