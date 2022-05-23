using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Internal;
using Moq;
using NUnit.Framework;
using ParserFunc.AzureServices.Interfaces;
using ParserFunc.FunctionApp;
using ParserFunc.FunctionApp.Models;
using ParserFunc.JsonParserService.Interfaces;

namespace FunctionApp.Tests
{
    [TestFixture]
    public class ParserFunctionTests
    {
        private Mock<IParserService> parseServiceMock;
        private Mock<IBlobService> blobServiceMock;
        private Mock<IFileShareService> fileShareServiceMock;
        private Mock<IValidator> jsonValidatorMock;

        private Mock<ILogger> loggerMock;
        private string validationError = "test error";

        private ParseFunction parseFunction;

        [SetUp]
        public void SetUp()
        {
            parseServiceMock = new Mock<IParserService>();
            blobServiceMock = new Mock<IBlobService>();
            fileShareServiceMock = new Mock<IFileShareService>();
            jsonValidatorMock = new Mock<IValidator>();

            parseFunction = new ParseFunction(
                jsonValidatorMock.Object,
                fileShareServiceMock.Object,
                blobServiceMock.Object,
                parseServiceMock.Object);

            loggerMock = new Mock<ILogger>();
        }



        [Test]
        public void ParseJson_ValidationFailed_LogErrorAndReturnsBadRequest()
        {
            SetUpValidateFileName(false);

            var resultTask = parseFunction.ParseJson(null, It.IsAny<string>(), loggerMock.Object);

            Assert.That(resultTask.Result, Is.TypeOf<BadRequestObjectResult>());
            VerifyLogError(validationError);
        }
        

        [Test]
        public void ParseJson_ErrorOccurredWhileGettingBlob_LogErrorAndReturnsBadRequest()
        {
            SetUpValidateFileName(true);
            SetUpGetStreamByNameAsyncFailed();

            var resultTask = parseFunction.ParseJson(null, It.IsAny<string>(), loggerMock.Object);

            Assert.That(resultTask.Result, Is.TypeOf<BadRequestObjectResult>());
            VerifyLogError();
        }

        private void SetUpGetStreamByNameAsyncFailed()
        {
            blobServiceMock
                .Setup(service => service.GetStreamByNameAsync(It.IsAny<string>()))
                .Throws<Exception>();
        }


        [Test]
        public void ParseJson_ErrorOccurredWhileParsingJson_LogErrorAndReturnsBadRequest()
        {
            SetUpValidateFileName(true);
            SetUpReparseJsonFailed();

            var resultTask = parseFunction.ParseJson(null, It.IsAny<string>(), loggerMock.Object);

            Assert.That(resultTask.Result, Is.TypeOf<BadRequestObjectResult>());
            VerifyLogError();
        }


        private void SetUpReparseJsonFailed()
        {
            parseServiceMock
                .Setup(service => service.ReparseJson(It.IsAny<Stream>()))
                .Throws<Exception>();
        }


        [Test]
        public void ParseJson_ErrorOccurredWhileUploadingFileOnFileShare_LogErrorAndReturnsBadRequest()
        {
            SetUpValidateFileName(true);
            SetUpUploadToFileShareFailed();

            var resultTask = parseFunction.ParseJson(null, It.IsAny<string>(), loggerMock.Object);

            Assert.That(resultTask.Result, Is.TypeOf<BadRequestObjectResult>());
            VerifyLogError();
        }

        private void SetUpUploadToFileShareFailed()
        {
            fileShareServiceMock
                .Setup(service => service.UploadToFileShareAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<Exception>();
        }


        private void VerifyLogError(string error = "")
        {
            loggerMock.Verify(
                logger => logger.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<FormattedLogValues>(values => values.ToString().Contains(error)),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<object, Exception, string>>()
                )
            );
        }


        [Test]
        public void ParseJson_SuccessfulParsing_UploadsToFileShareAndReturnsOkResult()
        {
            SetUpValidateFileName(true);

            var resultTask = parseFunction.ParseJson(null, It.IsAny<string>(), loggerMock.Object);

            Assert.That(resultTask.Result, Is.TypeOf<OkObjectResult>());
            VerifyLogInformation();
            VerifyServices();
        }

        private void VerifyLogInformation()
        {
            loggerMock.Verify(
                m => m.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<FormattedLogValues>(v => v.ToString().Contains("")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<object, Exception, string>>()
                )
            );
        }

        private void VerifyServices()
        {
            jsonValidatorMock.Verify(validator=>
                validator.ValidateFileName(It.IsAny<string>(), out validationError), Times.Once);

            blobServiceMock.Verify(service=>
                service.GetStreamByNameAsync(It.IsAny<string>()), Times.Once);

            parseServiceMock.Verify(service=>
                service.ReparseJson(It.IsAny<Stream>()), Times.Once);

            fileShareServiceMock.Verify(service=>
                service.UploadToFileShareAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        private void SetUpValidateFileName(bool isValid)
        {
            jsonValidatorMock
                .Setup(validator => validator.ValidateFileName(It.IsAny<string>(), out validationError))
                .Returns(() => isValid);
        }
    }
}