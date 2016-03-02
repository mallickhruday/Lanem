using System;
using System.Text;
using Guardo;
using Lanem.Common;

namespace Lanem.Parsers
{
    public sealed class TextErrorParser : IErrorParser
    {
        private readonly IHttpRequestConverter _httpRequestConverter;
        private const string DefaultIndent = "  ";

        public TextErrorParser(IHttpRequestConverter httpRequestConverter)
        {
            _httpRequestConverter = httpRequestConverter;
        }

        public string Parse(Error error)
        {
            Requires.NotNull(error);
            
            var rawRequest = _httpRequestConverter.ToRawString(error.HttpRequest);
            var ex = error.Exception;
            
            var sb = new StringBuilder();

            sb.AppendLine(DateTimeProvider.UtcNow.ToString("dddd, dd MMMM yyyy HH:mm:ss"));
            sb.AppendLine("");
            sb.AppendLine("Exception:");
            sb.AppendLine("--------------");
            sb.AppendLine($"Message: {ex.Message}");
            AppendInnerException(sb, ex.InnerException);
            sb.AppendLine("Stack Trace:");
            sb.AppendLine(ex.StackTrace);
            sb.AppendLine($"Source: {ex.Source}");
            sb.AppendLine($"Target Site:");
            sb.AppendLine($"{DefaultIndent}Method: {ex.TargetSite}");
            sb.AppendLine($"{DefaultIndent}Assembly: {ex.TargetSite.DeclaringType?.AssemblyQualifiedName}");
            sb.AppendLine("");
            sb.AppendLine("HTTP Request:");
            sb.AppendLine("--------------");
            sb.AppendLine(rawRequest);

            return sb.ToString();
        }

        private static void AppendInnerException(StringBuilder sb, Exception ex, string indent = DefaultIndent)
        {
            while (true)
            {
                if (ex == null)
                {
                    sb.AppendLine("Inner Exception: null");
                    return;
                }

                sb.AppendLine("Inner Exception:");
                sb.AppendLine($"{indent}Message: {ex.Message}");
                sb.AppendLine($"{indent}Class Name: {ex.Source}");
                ex = ex.InnerException;
                indent += DefaultIndent;
            }
        }
    }
}