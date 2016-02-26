using System;
using Guardo;

namespace Lanem.IO
{
    public class LogFileNameGenerator : IFileNameGenerator
    {
        private readonly string _logDirectoryPath;

        public LogFileNameGenerator(string logDirectoryPath)
        {
            Requires.NotNullOrEmpty(logDirectoryPath, nameof(logDirectoryPath));

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