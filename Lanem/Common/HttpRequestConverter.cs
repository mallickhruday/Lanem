using System.IO;
using System.Text;
using System.Web;
using Guardo;

namespace Lanem.Common
{
    public class HttpRequestConverter : IHttpRequestConverter
    {
        public string ToRawString(HttpRequestBase httpRequest)
        {
            Requires.NotNull(httpRequest);

            var sb = new StringBuilder();

            AppendHeader(sb, httpRequest);
            AppendBody(sb, httpRequest);

            return sb.ToString();
        }

        private static void AppendHeader(StringBuilder sb, HttpRequestBase req)
        {
            sb.AppendLine($"{req.HttpMethod} {req.Url} {req.ServerVariables["SERVER_PROTOCOL"]}");

            foreach (var key in req.Headers.AllKeys)
            {
                sb.AppendLine($"{key}: {req.Headers[key]}");
            }
        }

        private static void AppendBody(StringBuilder sb, HttpRequestBase req)
        {
            string body;
            using (var reader = new StreamReader(req.InputStream))
            {
                body = reader.ReadToEnd();
            }

            if (string.IsNullOrEmpty(body))
                return;

            sb.AppendLine();
            sb.AppendLine(body);
        }
    }
}