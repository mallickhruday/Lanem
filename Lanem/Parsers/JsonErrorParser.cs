using Newtonsoft.Json;

namespace Lanem.Parsers
{
    public sealed class JsonErrorParser : IErrorParser
    {
        public string Parse(Error error)
        {
            return JsonConvert.SerializeObject(error, Formatting.None);
        }
    }
}