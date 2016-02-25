using System;
using Lanem.Filters;
using NUnit.Framework;

namespace Lanem.Tests
{
    [TestFixture]
    public class NoExceptionFilterTests
    {
        [Test]
        public void Exceptions_Dont_Get_Filtered()
        {
            var filter = new NoExceptionFilter();

            // Testing for some common exceptions...
            Assert.IsFalse(filter.SkipException(new NullReferenceException()));
            Assert.IsFalse(filter.SkipException(new ArgumentNullException()));
            Assert.IsFalse(filter.SkipException(new ArgumentException()));
            Assert.IsFalse(filter.SkipException(new DivideByZeroException()));
            Assert.IsFalse(filter.SkipException(new FormatException()));
        }
    }
}