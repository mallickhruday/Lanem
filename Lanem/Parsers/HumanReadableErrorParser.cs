using System;
using System.Net.NetworkInformation;
using System.Text;
using Guardo;
using Lanem.Extensions;
using Newtonsoft.Json;

namespace Lanem.Parsers
{
    public sealed class DateTimeProvider
    {
        private static DateTime? _utcNow;

        public static void SetUtcNow(DateTime utcNow)
        {
            _utcNow = utcNow;
        }

        public static DateTime UtcNow => _utcNow ?? DateTime.UtcNow;
    }

    public sealed class MarkDownErrorParser : IErrorParser
    {
        public string Parse(Error error)
        {
            Requires.NotNull(error);

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

    public sealed class HumanReadableErrorParser : IErrorParser
    {
        public string Parse(Error error)
        {
            Requires.NotNull(error);

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