using System.IO;
using System.Web;

namespace Lanem.Extensions
{
    public class HttpRequestConverter
    {
        /// <summary>
        /// Converts a <see cref="HttpRequestBase"/> object into its raw string representation as it was received over the HTTP protocol.
        /// </summary>
        /// <param name="httpRequest">The HTTP request to convert into a string.</param>
        /// <returns>Plain text representation of the given HTTP request.</returns>
        public static string ConvertToRawString(HttpRequestBase httpRequest)
        {
            using (var writer = new StringWriter())
            {
                writer.Write(httpRequest.HttpMethod);
                writer.Write(" " + httpRequest.Url);
                writer.WriteLine(" " + httpRequest.ServerVariables["SERVER_PROTOCOL"]);

                foreach (var key in httpRequest.Headers.AllKeys)
                {
                    writer.WriteLine($"{key}: {httpRequest.Headers[key]}");
                }

                string body;
                using (var reader = new StreamReader(httpRequest.InputStream))
                {
                    body = reader.ReadToEnd();
                }

                if (string.IsNullOrEmpty(body))
                    return writer.ToString();

                writer.WriteLine();
                writer.WriteLine(body);
                return writer.ToString();
            }
        }
    }

    public static class HttpRequestExtensions
    {
        /// <summary>
        /// Converts a <see cref="HttpRequestBase"/> into its raw string representation as it was received over the HTTP protocol.
        /// </summary>
        /// <param name="httpRequest">The HTTP request to convert into a string.</param>
        /// <returns>Plain text representation of the given HTTP request.</returns>
        public static string ToRawString(this HttpRequestBase httpRequest)
        {
            using (var writer = new StringWriter())
            {
                writer.Write(httpRequest.HttpMethod);
                writer.Write(" " + httpRequest.Url);
                writer.WriteLine(" " + httpRequest.ServerVariables["SERVER_PROTOCOL"]);

                foreach (var key in httpRequest.Headers.AllKeys)
                {
                    writer.WriteLine($"{key}: {httpRequest.Headers[key]}");
                }

                string body;
                using (var reader = new StreamReader(httpRequest.InputStream))
                {
                    body = reader.ReadToEnd();
                }

                if (string.IsNullOrEmpty(body))
                    return writer.ToString();

                writer.WriteLine();
                writer.WriteLine(body);
                return writer.ToString();
            }
        }
    }
}