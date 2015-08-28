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
            return string.Format("{0}\\{1}_{2}.log", _logDirectoryPath, CreateCurrentDateString(), CreateUniqueString());
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