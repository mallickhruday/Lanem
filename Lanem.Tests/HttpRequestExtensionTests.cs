using System.Collections.Specialized;
using System.Web;
using Lanem.Extensions;
using NUnit.Framework;

namespace Lanem.Tests
{
    [TestFixture]
    public class HttpRequestExtensionTests
    {
        [Test]
        public void ToRawString_With_Empty_Request_Returns_OneLine()
        {
            var httpRequest = new HttpRequest(
                string.Empty, 
                "http://example.org/", 
                string.Empty);

            const string expectedRawString = "GET http://example.org/ \r\n";

            var rawString = httpRequest.ToRawString();

            Assert.AreEqual(expectedRawString, rawString);
        }

        [Test]
        public void ToRawString_EmptyRequestWithServerProtocol_ReturnsOneLine()
        {
            var httpRequest = new HttpRequest(
                string.Empty,
                "http://example.org/",
                string.Empty);

            httpRequest.ServerVariables.Add("SERVER_PROTOCOL", "HTTP1.1");

            const string expectedRawString = "GET http://example.org/ HTTP/1.1\r\n";

            var rawString = httpRequest.ToRawString();

            Assert.AreEqual(expectedRawString, rawString);
        }

        [Test]
        public void ToRawString_RequestWithHeadersAndBody_ReturnsCompleteString()
        {
            var httpRequest = new HttpRequest(
                string.Empty,
                "http://example.org/",
                string.Empty)
            {
                //Headers =
                //{
                //    { "X-Test-Header", "Lanem" }
                //}
            };

            httpRequest.Headers.Add("", "");
            

            const string expectedRawString = "GET http://example.org/ \r\n";

            var rawString = httpRequest.ToRawString();

            Assert.AreEqual(expectedRawString, rawString);
        }
    }
}
