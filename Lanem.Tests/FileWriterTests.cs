using System;
using System.IO;
using Lanem.IO;
using NUnit.Framework;

namespace Lanem.Tests
{
    [TestFixture]
    public class FileWriterTests
    {
        private IFileWriter _fileWriter;
        private string _testDir;
        private string _fileName;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _testDir = Path.Combine(
                TestContext.CurrentContext.TestDirectory,
                "Temp_UnitTest_Output");

            if (!Directory.Exists(_testDir))
                Directory.CreateDirectory(_testDir);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Directory.Delete(_testDir, true);
        }

        [SetUp]
        public void Setup()
        {
            _fileName = Path.Combine(_testDir, $"{Guid.NewGuid().ToString("N")}.txt");
            _fileWriter = new FileWriter();
        }

        [Test]
        public void Write_With_Null_FileName_Throws_Exception()
        {
            Assert.Throws<ArgumentNullException>(() => _fileWriter.Write(null, "content"));
        }

        [Test]
        public void Write_With_Empty_FileName_Throws_Exception()
        {
            Assert.Throws<ArgumentException>(() => _fileWriter.Write("", "content"));
        }

        [Test]
        public void Write_With_Null_Content_Creates_Empty_File()
        {
            _fileWriter.Write(_fileName, null);

            var fileInfo = new FileInfo(_fileName);
            Assert.AreEqual(0, fileInfo.Length);
        }
        [Test]
        public void Write_With_Content_Creates_File_With_Content()
        {
            const string content = "hello world.";

            _fileWriter.Write(_fileName, content);
            
            var fileInfo = new FileInfo(_fileName);
            Assert.AreEqual(content.Length, fileInfo.Length);

            var contentFromFile = File.ReadAllText(_fileName);
            Assert.AreEqual(content, contentFromFile);
        }
    }
}