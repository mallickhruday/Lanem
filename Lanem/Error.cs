using System;
using System.Web;

namespace Lanem
{
    /// <summary>
    /// Encapsulates exception related information raised during an ASP.NET web request.
    /// </summary>
    public sealed class Error
    {
        /// <summary>
        /// The exception which has been raised during an ASP.NET web request.
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// The http request during which an exception has been raised.
        /// </summary>
        public HttpRequest HttpRequest { get; set; }
    }
}