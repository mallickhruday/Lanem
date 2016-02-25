using System;
using Lanem.IO;
using NUnit.Framework;

namespace Lanem.Tests
{
    public class FileNameGeneratorTests
    {
        [TestFixture]
        public class ConstructorTests
        {
            [Test]
            public void Null_DirectoryPath_Throws_Exception()
            {
                Assert.Throws<ArgumentNullException>(() => new LogFileNameGenerator(null));
            }
            
            [Test]
            public void Empty_DirectoryPath_Throws_Exception()
            {
                Assert.Throws<ArgumentException>(() => new LogFileNameGenerator(""));
            }
        }

        [TestFixture]
        public class GenerateFileNameTests
        {
            private IFileNameGenerator _logFileNameGenerator;

            [SetUp]
            public void Setup()
            {
                var workDirectory = TestContext.CurrentContext.WorkDirectory;
                _logFileNameGenerator = new LogFileNameGenerator(workDirectory);
            }

            [Test]
            public void FileName_Ends_With_Log()
            {
                var fileName = _logFileNameGenerator.GenerateFileName();

                Assert.IsTrue(fileName.EndsWith(".log"));
            }

            [Test]
            public void FileNames_Are_Pseudo_Randomly_Generated()
            {
                var fileName1 = _logFileNameGenerator.GenerateFileName();
                var fileName2 = _logFileNameGenerator.GenerateFileName();

                Assert.AreNotEqual(fileName1, fileName2);
            }
        }
    }
}