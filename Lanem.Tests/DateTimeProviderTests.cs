using System;
using System.Diagnostics;
using System.Threading;
using Lanem.Common;
using NUnit.Framework;

namespace Lanem.Tests
{
    [TestFixture]
    public class FileWriterTests
    {
        private string _directoryPath;

        [SetUp]
        public void Setup()
        {
            _directoryPath = TestContext.CurrentContext.WorkDirectory;
        }

        [Test]
        public void Test()
        {
            //System.Diagnostics.
            Debug.WriteLine(_directoryPath);
            Console.WriteLine(_directoryPath);
        }
    }

    [TestFixture]
    public class DateTimeProviderTests
    {
        [SetUp]
        public void Setup()
        {
            DateTimeProvider.Reset();
        }

        [TearDown]
        public void TearDown()
        {
            DateTimeProvider.Reset();
        }

        [Test]
        public void UtcNow_Returns_Current_UTC_Time_By_Default()
        {
            DateTimeProvider.Reset();

            var diff = DateTimeProvider.UtcNow - DateTime.UtcNow;
            
            Assert.IsTrue(diff <= TimeSpan.FromSeconds(1));
        }

        [Test]
        public void UtcNow_When_Overwritten_Returns_Static_Time()
        {
            var staticDate = DateTime.UtcNow;
            DateTimeProvider.SetUtcNow(staticDate);

            Assert.AreEqual(staticDate, DateTimeProvider.UtcNow);
            Thread.Sleep(TimeSpan.FromMilliseconds(10));
            Assert.AreEqual(staticDate, DateTimeProvider.UtcNow);
        }
    }
}