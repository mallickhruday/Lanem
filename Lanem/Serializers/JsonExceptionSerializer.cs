using System;
using Newtonsoft.Json;

namespace Lanem.Serializers
{
    public sealed class JsonExceptionSerializer : IExceptionSerializer
    {
        public string Serialize(Exception exception)
        {
            return JsonConvert.SerializeObject(exception, Formatting.Indented);
        }
    }
}