using System.IO;
using System.Web;

namespace Lanem.Extensions
{
    public static class HttpRequestExtensions
    {
        public static string ToRawString(this HttpRequest request)
        {
            using (var writer = new StringWriter())
            {
                writer.Write(request.HttpMethod);
                writer.Write(" " + request.Url);
                writer.WriteLine(" " + request.ServerVariables["SERVER_PROTOCOL"]);

                foreach (var key in request.Headers.AllKeys)
                {
                    writer.WriteLine("{0}: {1}", key, request.Headers[key]);
                }

                writer.WriteLine();

                using (var reader = new StreamReader(request.InputStream))
                {
                    var body = reader.ReadToEnd();
                    writer.WriteLine(body);
                }

                return writer.ToString();
            }
        }
    }
}