using System;
using Newtonsoft.Json;

namespace Lanem.ExceptionFormatters
{
    public sealed class JsonExceptionFormatter : IExceptionFormatter
    {
        public string Format(Exception exception)
        {
            return JsonConvert.SerializeObject(exception, Formatting.Indented);
        }
    }
}