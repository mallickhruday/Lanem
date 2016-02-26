using System;
using Lanem.Filters;
using Lanem.IO;
using Lanem.Loggers;
using Lanem.Parsers;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;

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
            var error = new Error { Exception = exception };

            _errorLogger.Log(error);

            _exceptionFilter.ReceivedWithAnyArgs(1).SkipException(null);
            _exceptionFilter.Received().SkipException(exception);
        }

        [Test]
        public void Log_Does_Not_Parse_Error_When_Error_Gets_Filtered()
        {
            _exceptionFilter.SkipException(null).ReturnsForAnyArgs(true);

            _errorLogger.Log(new Error());

            _errorParser.DidNotReceiveWithAnyArgs().Parse(null);
        }

        [Test]
        public void Log_Does_Not_Generate_FileName_When_Error_Gets_Filtered()
        {
            _exceptionFilter.SkipException(null).ReturnsForAnyArgs(true);

            _errorLogger.Log(new Error());
            
            _fileNameGenerator.DidNotReceiveWithAnyArgs().GenerateFileName();
        }

        [Test]
        public void Log_Does_Not_Write_To_File_When_Error_Gets_Filtered()
        {
            _exceptionFilter.SkipException(null).ReturnsForAnyArgs(true);

            _errorLogger.Log(new Error());
            
            _fileWriter.DidNotReceiveWithAnyArgs().Write(null, null);
        }

        [Test]
        public void Log_Parses_Error_When_Error_Did_Not_Get_Filtered()
        {
            var error = new Error();

            _errorLogger.Log(error);

            _errorParser.ReceivedWithAnyArgs(1).Parse(null);
            _errorParser.Received().Parse(error);
        }

        [Test]
        public void Log_Generates_FileName_When_Error_Did_Not_Get_Filtered()
        {
            _errorLogger.Log(new Error());

            _fileNameGenerator.ReceivedWithAnyArgs(1).GenerateFileName();
        }

        [Test]
        public void Log_Writes_Error_To_File_When_Error_Did_Not_Get_Filtered()
        {
            const string parsedError = "randomly parsed error";
            const string fileName = "this is a random file name";

            _errorParser.Parse(null).ReturnsForAnyArgs(parsedError);
            _fileNameGenerator.GenerateFileName().ReturnsForAnyArgs(fileName);

            _errorLogger.Log(new Error());

            _fileWriter.ReceivedWithAnyArgs(1).Write(null, null);
            _fileWriter.Received().Write(fileName, parsedError);
        }
    }

    [TestFixture]
    public class FileNameGeneratorTests
    {
        private readonly string _logDirectoryPath = TestContext.CurrentContext.WorkDirectory;

        [Test]
        public void Constructor_With_Null_DirectoryPath_Throws_Exception()
        {
            Assert.Throws<ArgumentNullException>(() => new LogFileNameGenerator(null));
        }

        [Test]
        public void Constructor_With_Empty_DirectoryPath_Throws_Exception()
        {
            Assert.Throws<ArgumentException>(() => new LogFileNameGenerator(""));
        }

        [Test]
        public void Constructor_With_NonEmpty_DirectoryPath_InitializesObject()
        {
            var logFileNameGenerator = new LogFileNameGenerator("not empty");

            Assert.IsNotNull(logFileNameGenerator);
        }

        [Test]
        public void FileName_Ends_With_Log()
        {
            var logFileNameGenerator = new LogFileNameGenerator(_logDirectoryPath);

            var fileName = logFileNameGenerator.GenerateFileName();

            Assert.IsTrue(fileName.EndsWith(".log"));
        }

        [Test]
        public void FileNames_Are_Pseudo_Randomly_Generated()
        {
            var logFileNameGenerator = new LogFileNameGenerator(_logDirectoryPath);
            
            var fileName1 = logFileNameGenerator.GenerateFileName();
            var fileName2 = logFileNameGenerator.GenerateFileName();

            Assert.AreNotEqual(fileName1, fileName2);
        }

        [Test]
        public void FileName_Starts_With_LogDirectoryPath()
        {
            const string logDirectoryPath = "C:\\some\\path";
            var fileNameGenerator = new LogFileNameGenerator(logDirectoryPath);

            var fileName = fileNameGenerator.GenerateFileName();

            Assert.IsTrue(fileName.StartsWith(logDirectoryPath));
        }
    }
}