using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Web;
using Lanem.Common;
using NSubstitute;
using NUnit.Framework;

namespace Lanem.Tests
{
    [TestFixture]
    public class HttpRequestConverterTests
    {
        private IHttpRequestConverter _httpRequestConverter;

        [SetUp]
        public void Setup()
        {
            _httpRequestConverter = new HttpRequestConverter();
        }

        [Test]
        public void ToRawString_With_Null_HttpRequest_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => _httpRequestConverter.ToRawString(null));
        }

        [Test]
        public void ToRawString_Request_Without_Body_Or_Header_Returns_Only_Request_Path()
        {
            var request = Substitute.For<HttpRequestBase>();
            request.HttpMethod.ReturnsForAnyArgs("method-1234");
            request.Url.ReturnsForAnyArgs(new Uri("http://www.example.org/test/a/b"));
            request.ServerVariables.ReturnsForAnyArgs(
                new NameValueCollection
                {
                    {"SERVER_PROTOCOL", "ABCDEFG"}
                });
            request.Headers.ReturnsForAnyArgs(new NameValueCollection());
            request.InputStream.ReturnsForAnyArgs(new MemoryStream());
            var expected = $"method-1234 http://www.example.org/test/a/b ABCDEFG{Environment.NewLine}";

            var actual = _httpRequestConverter.ToRawString(request);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ToRawString_Request_Without_Body_But_With_Header_Returns_Header()
        {
            var request = Substitute.For<HttpRequestBase>();
            request.HttpMethod.ReturnsForAnyArgs("method-1234");
            request.Url.ReturnsForAnyArgs(new Uri("http://www.example.org/test/a/b"));
            request.ServerVariables.ReturnsForAnyArgs(
                new NameValueCollection
                {
                    {"SERVER_PROTOCOL", "ABCDEFG"}
                });
            request.Headers.ReturnsForAnyArgs(
                new NameValueCollection
                {
                    { "A", "aa" },
                    { "BB", "bb" },
                    { "CCC", "c" }
                });
            request.InputStream.ReturnsForAnyArgs(new MemoryStream());
            var expected = $"method-1234 http://www.example.org/test/a/b ABCDEFG{Environment.NewLine}A: aa{Environment.NewLine}BB: bb{Environment.NewLine}CCC: c{Environment.NewLine}";

            var actual = _httpRequestConverter.ToRawString(request);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ToRawString_Request_With_Body_And_Header_Returns_Header_And_Body()
        {
            var request = Substitute.For<HttpRequestBase>();
            request.HttpMethod.ReturnsForAnyArgs("method-1234");
            request.Url.ReturnsForAnyArgs(new Uri("http://www.example.org/test/a/b"));
            request.ServerVariables.ReturnsForAnyArgs(
                new NameValueCollection
                {
                    {"SERVER_PROTOCOL", "ABCDEFG"}
                });
            request.Headers.ReturnsForAnyArgs(
                new NameValueCollection
                {
                    { "A", "aa" },
                    { "BB", "bb" },
                    { "CCC", "c" }
                });
            request.InputStream.ReturnsForAnyArgs(
                new MemoryStream(
                    Encoding.UTF8.GetBytes("Hello World!")));
            var expected = $"method-1234 http://www.example.org/test/a/b ABCDEFG{Environment.NewLine}A: aa{Environment.NewLine}BB: bb{Environment.NewLine}CCC: c{Environment.NewLine}{Environment.NewLine}Hello World!{Environment.NewLine}";

            var actual = _httpRequestConverter.ToRawString(request);

            Assert.AreEqual(expected, actual);
        }
    }
}