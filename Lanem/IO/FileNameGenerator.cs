using System;

namespace Lanem.IO
{
    public class FileNameGenerator : IFileNameGenerator
    {
        private readonly string _logDirectoryPath;

        public FileNameGenerator(string logDirectoryPath)
        {
            _logDirectoryPath = logDirectoryPath;
        }

        public string CreateFileName()
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