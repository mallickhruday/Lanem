using System;
using Lanem.IO;
using NUnit.Framework;

namespace Lanem.Tests
{
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