using System;

namespace Lanem.ErrorLoggers
{
    public class LogFilePathGenerator : ILogFilePathGenerator
    {
        private readonly string _logDirectoryPath;

        public LogFilePathGenerator(string logDirectoryPath)
        {
            _logDirectoryPath = logDirectoryPath;
        }

        public string CreateNewLogFilePath()
        {
            return $"{_logDirectoryPath}\\{CreateCurrentDateString()}_{CreateUniqueString()}.log";
        }

        protected virtual string CreateCurrentDateString()
        {
            return DateTime.UtcNow.ToString("O").Replace(":", ".");
        }

        protected virtual string CreateUniqueString()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}