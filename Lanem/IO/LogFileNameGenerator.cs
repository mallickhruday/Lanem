using System;

namespace Lanem.IO
{
    public class LogFileNameGenerator : IFileNameGenerator
    {
        private readonly string _logDirectoryPath;

        public LogFileNameGenerator(string logDirectoryPath)
        {
            if (logDirectoryPath == null)
                throw new ArgumentNullException(nameof(logDirectoryPath));

            if (logDirectoryPath.Length == 0)
                throw new ArgumentException("The log directory path cannot be empty.", nameof(logDirectoryPath));

            _logDirectoryPath = logDirectoryPath;
        }

        public string GenerateFileName()
        {
            return $"{_logDirectoryPath}\\{CreateCurrentDateString()}.{CreateUniqueString()}.log";
        }

        protected virtual string CreateCurrentDateString()
        {
            return DateTime.UtcNow.Ticks.ToString();
        }

        protected virtual string CreateUniqueString()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}