using System;
using System.Text;
using Lanem.Extensions;
using Lanem.Filters;
using Lanem.IO;
using Lanem.Parsers;

namespace Lanem.Loggers
{
    public sealed class FileErrorLogger : IErrorLogger
    {
        private readonly IErrorFilter _errorFilter;
        private readonly IErrorParser _errorParser;
        private readonly IFileNameGenerator _fileNameGenerator;
        private readonly IFileWriter _fileWriter;

        public FileErrorLogger(
            IErrorFilter errorFilter,
            IErrorParser errorParser,
            IFileNameGenerator fileNameGenerator,
            IFileWriter fileWriter)
        {
            _errorFilter = errorFilter;
            _errorParser = errorParser;
            _fileNameGenerator = fileNameGenerator;
            _fileWriter = fileWriter;
        }

        public void Log(Error error)
        {
            if (_errorFilter.SkipError(error.Exception))
                return;

            var parsedError = _errorParser.Parse(error);
            var fileName = _fileNameGenerator.CreateFileName();

            _fileWriter.Write(fileName, parsedError);
        }
    }
}