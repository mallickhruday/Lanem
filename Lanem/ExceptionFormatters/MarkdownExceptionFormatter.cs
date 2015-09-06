using System;
using System.Globalization;
using System.Text;

namespace Lanem.ExceptionFormatters
{
    public sealed class MarkdownExceptionFormatter : IExceptionFormatter
    {
        public string Format(Exception exception)
        {
            var builder = new StringBuilder();

            builder.AppendLine(string.Concat("# ", DateTime.UtcNow.ToString("U"), " (UTC)"));

            Format(builder, exception);

            return builder.ToString();
        }

        private static void Format(StringBuilder builder, Exception exception)
        {
            Format(builder, "Type", exception.GetType().ToString());
            Format(builder, "Message", exception.Message);
            Format(builder, "Source", exception.Source);
            Format(builder, "TargetSite", exception.TargetSite.ToString());

            if (exception.TargetSite.DeclaringType != null)
            {
                builder.AppendLine(exception.TargetSite.DeclaringType.AssemblyQualifiedName);
                builder.AppendLine(exception.TargetSite.DeclaringType.Assembly.Location);
            }

            Format(builder, "StackTrace", exception.StackTrace);

            builder.AppendLine("");
            builder.AppendLine("## Data");
            if (exception.Data.Keys.Count > 0)
            {
                foreach (var key in exception.Data.Keys)
                {
                    Format(builder, key.ToString(), exception.Data[key].ToString());
                }
            }
            
            Format(builder, "HelpLink", exception.HelpLink);

            if (exception.InnerException == null)
                return;

            builder.AppendLine("-------------");
            Format(builder, exception.InnerException);
        }

        private static void Format(StringBuilder builder, string title, string value)
        {
            builder.AppendLine("");
            builder.AppendLine($"## {title}");
            builder.AppendLine(value ?? "Null");
        }
    }
}