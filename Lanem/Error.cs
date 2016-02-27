using System;
using System.Web;
using Guardo;

namespace Lanem
{
    /// <summary>
    /// Encapsulates exception related information raised during an ASP.NET web request.
    /// </summary>
    public sealed class Error
    {
        /// <summary>
        /// Initializes a new error object.
        /// </summary>
        /// <param name="exception">The exception which has been raised during an ASP.NET web request.</param>
        /// <param name="httpRequest">The HTTP request during which an exception has been raised.</param>
        public Error(Exception exception, HttpRequestBase httpRequest)
        {
            Requires.NotNull(exception);
            Requires.NotNull(httpRequest);

            Exception = exception;
            HttpRequest = httpRequest;
        }

        /// <summary>
        /// The exception which has been raised during an ASP.NET web request.
        /// </summary>
        public Exception Exception { get; }

        /// <summary>
        /// The HTTP request during which an exception has been raised.
        /// </summary>
        public HttpRequestBase HttpRequest { get; }
    }
}