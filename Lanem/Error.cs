using System;
using System.Web;

namespace Lanem
{
    public sealed class Error
    {
        public Exception Exception { get; set; }
        public HttpRequest HttpRequest { get; set; }
    }
}