using System;
using System.Text;
using Lanem.Extensions;
using Newtonsoft.Json;

namespace Lanem.Parsers
{
    public sealed class HumanReadableErrorParser : IErrorParser
    {
        public string Parse(Error error)
        {
            var parsedException = JsonConvert.SerializeObject(error.Exception, Formatting.Indented);
            var rawRequest = error.HttpRequest.ToRawString();

            var content = new StringBuilder();
            content.AppendLine(DateTime.UtcNow.ToString("dddd, dd MMMM yyyy HH:mm:ss"));
            content.AppendLine("");
            content.AppendLine("Exception:");
            content.AppendLine("--------------");
            content.AppendLine(parsedException);
            content.AppendLine("");
            content.AppendLine("HTTP Request:");
            content.AppendLine("--------------");
            content.AppendLine(rawRequest);

            return content.ToString();
        }
    }
}