using System;
using Lanem.ErrorFilters;
using Lanem.ExceptionFormatters;
using Lanem.IO;

namespace Lanem.ErrorLoggers
{
    public sealed class FileErrorLogger : IErrorLogger
    {
        private readonly IErrorFilter _errorFilter;
        private readonly IExceptionFormatter _formatter;
        private readonly ILogFilePathGenerator _logFilePathGenerator;
        private readonly IFileWriter _fileWriter;

        public FileErrorLogger(
            IErrorFilter errorFilter,
            IExceptionFormatter formatter,
            ILogFilePathGenerator logFilePathGenerator,
            IFileWriter fileWriter)
        {
            _errorFilter = errorFilter;
            _formatter = formatter;
            _logFilePathGenerator = logFilePathGenerator;
            _fileWriter = fileWriter;
        }

        public void Log(Exception exception)
        {
            if (_errorFilter.SkipError(exception))
                return;

            var content = _formatter.Format(exception);

            var logFilePath = _logFilePathGenerator.CreateNewLogFilePath();

            _fileWriter.Write(logFilePath, content);
        }
    }
}