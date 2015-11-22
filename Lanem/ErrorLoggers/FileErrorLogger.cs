using System;
using System.Text;
using Lanem.ErrorFilters;
using Lanem.ExceptionFormatters;
using Lanem.Extensions;
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

        public void Log(ErrorDetails errorDetails)
        {
            if (_errorFilter.SkipError(errorDetails.Exception))
                return;
            
            var formattedException = _formatter.Format(errorDetails.Exception);
            var rawRequest = errorDetails.HttpRequest.AsRawString();

            var content = new StringBuilder();
            content.AppendLine(DateTime.UtcNow.ToString("dddd, dd MMMM yyyy HH:mm:ss"));
            content.AppendLine("");
            content.AppendLine("Exception:");
            content.AppendLine("--------------");
            content.AppendLine(formattedException);
            content.AppendLine("");
            content.AppendLine("HTTP Request:");
            content.AppendLine("--------------");
            content.AppendLine(rawRequest);

            var logFilePath = _logFilePathGenerator.CreateNewLogFilePath();

            _fileWriter.Write(logFilePath, content.ToString());
        }
    }
}