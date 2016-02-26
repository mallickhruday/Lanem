using Guardo;
using Lanem.Filters;
using Lanem.IO;
using Lanem.Parsers;

namespace Lanem.Loggers
{
    public sealed class FileErrorLogger : IErrorLogger
    {
        private readonly IExceptionFilter _exceptionFilter;
        private readonly IErrorParser _errorParser;
        private readonly IFileNameGenerator _fileNameGenerator;
        private readonly IFileWriter _fileWriter;

        public FileErrorLogger(
            IExceptionFilter exceptionFilter,
            IErrorParser errorParser,
            IFileNameGenerator fileNameGenerator,
            IFileWriter fileWriter)
        {
            Requires.NotNull(exceptionFilter, nameof(exceptionFilter));
            Requires.NotNull(errorParser, nameof(errorParser));
            Requires.NotNull(fileNameGenerator, nameof(fileNameGenerator));
            Requires.NotNull(fileWriter, nameof(fileWriter));

            _exceptionFilter = exceptionFilter;
            _errorParser = errorParser;
            _fileNameGenerator = fileNameGenerator;
            _fileWriter = fileWriter;
        }

        public void Log(Error error)
        {
            Requires.NotNull(error, nameof(error));

            if (_exceptionFilter.SkipException(error.Exception))
                return;

            var parsedError = _errorParser.Parse(error);
            var fileName = _fileNameGenerator.GenerateFileName();

            _fileWriter.Write(fileName, parsedError);
        }
    }
}