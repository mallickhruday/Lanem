using System;
using System.Web;
using Lanem.Filters;
using Lanem.IO;
using Lanem.Loggers;
using Lanem.Parsers;
using NSubstitute;
using NUnit.Framework;

namespace Lanem.Tests
{
    [TestFixture]
    public class FileErrorLoggerTests
    {
        private IExceptionFilter _exceptionFilter;
        private IErrorParser _errorParser;
        private IFileNameGenerator _fileNameGenerator;
        private IFileWriter _fileWriter;
        private IErrorLogger _errorLogger;

        [SetUp]
        public void Setup()
        {
            _exceptionFilter = Substitute.For<IExceptionFilter>();
            _errorParser = Substitute.For<IErrorParser>();
            _fileNameGenerator = Substitute.For<IFileNameGenerator>();
            _fileWriter = Substitute.For<IFileWriter>();

            _errorLogger = new FileErrorLogger(
                _exceptionFilter,
                _errorParser,
                _fileNameGenerator,
                _fileWriter);
        }

        [Test]
        public void Constructor_With_Null_ExceptionFilter_Throws_Exception()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new FileErrorLogger(
                    null,
                    _errorParser,
                    _fileNameGenerator,
                    _fileWriter));
        }

        [Test]
        public void Constructor_With_Null_ErrorParser_Throws_Exception()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new FileErrorLogger(
                    _exceptionFilter,
                    null,
                    _fileNameGenerator,
                    _fileWriter));
        }

        [Test]
        public void Constructor_With_Null_FileNameGenerator_Throws_Exception()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new FileErrorLogger(
                    _exceptionFilter,
                    _errorParser,
                    null,
                    _fileWriter));
        }

        [Test]
        public void Constructor_With_Null_FileWriter_Throws_Exception()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new FileErrorLogger(
                    _exceptionFilter,
                    _errorParser,
                    _fileNameGenerator,
                    null));
        }

        [Test]
        public void Log_With_Null_Error_Throws_Exception()
        {
            Assert.Throws<ArgumentNullException>(() => _errorLogger.Log(null));
        }

        [Test]
        public void Log_Checks_If_Error_Should_Get_Skipped()
        {
            var exception = new Exception();
            var httpRequest = Substitute.For<HttpRequestBase>();
            var error = new Error(exception, httpRequest);

            _errorLogger.Log(error);

            _exceptionFilter.ReceivedWithAnyArgs(1).SkipException(null);
            _exceptionFilter.Received().SkipException(exception);
        }

        [Test]
        public void Log_Does_Not_Parse_Error_When_Error_Gets_Filtered()
        {
            var exception = new Exception();
            var httpRequest = Substitute.For<HttpRequestBase>();
            _exceptionFilter.SkipException(null).ReturnsForAnyArgs(true);

            _errorLogger.Log(new Error(exception, httpRequest));

            _errorParser.DidNotReceiveWithAnyArgs().Parse(null);
        }

        [Test]
        public void Log_Does_Not_Generate_FileName_When_Error_Gets_Filtered()
        {
            var exception = new Exception();
            var httpRequest = Substitute.For<HttpRequestBase>();
            _exceptionFilter.SkipException(null).ReturnsForAnyArgs(true);

            _errorLogger.Log(new Error(exception, httpRequest));
            
            _fileNameGenerator.DidNotReceiveWithAnyArgs().GenerateFileName();
        }

        [Test]
        public void Log_Does_Not_Write_To_File_When_Error_Gets_Filtered()
        {
            var exception = new Exception();
            var httpRequest = Substitute.For<HttpRequestBase>();
            _exceptionFilter.SkipException(null).ReturnsForAnyArgs(true);

            _errorLogger.Log(new Error(exception, httpRequest));
            
            _fileWriter.DidNotReceiveWithAnyArgs().Write(null, null);
        }

        [Test]
        public void Log_Parses_Error_When_Error_Did_Not_Get_Filtered()
        {
            var exception = new Exception();
            var httpRequest = Substitute.For<HttpRequestBase>();
            var error = new Error(exception, httpRequest);

            _errorLogger.Log(error);

            _errorParser.ReceivedWithAnyArgs(1).Parse(null);
            _errorParser.Received().Parse(error);
        }

        [Test]
        public void Log_Generates_FileName_When_Error_Did_Not_Get_Filtered()
        {
            var exception = new Exception();
            var httpRequest = Substitute.For<HttpRequestBase>();

            _errorLogger.Log(new Error(exception, httpRequest));

            _fileNameGenerator.ReceivedWithAnyArgs(1).GenerateFileName();
        }

        [Test]
        public void Log_Writes_Error_To_File_When_Error_Did_Not_Get_Filtered()
        {
            var exception = new Exception();
            var httpRequest = Substitute.For<HttpRequestBase>();
            const string parsedError = "randomly parsed error";
            const string fileName = "this is a random file name";

            _errorParser.Parse(null).ReturnsForAnyArgs(parsedError);
            _fileNameGenerator.GenerateFileName().ReturnsForAnyArgs(fileName);

            _errorLogger.Log(new Error(exception, httpRequest));

            _fileWriter.ReceivedWithAnyArgs(1).Write(null, null);
            _fileWriter.Received().Write(fileName, parsedError);
        }
    }
}