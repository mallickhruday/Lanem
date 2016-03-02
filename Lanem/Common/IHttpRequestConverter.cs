using System.Web;

namespace Lanem.Common
{
    /// <summary>
    /// Defines a single method to convert a <see cref="HttpRequestBase"/> object into a <see cref="string"/>.
    /// </summary>
    public interface IHttpRequestConverter
    {
        /// <summary>
        /// Converts a HTTP request object into its raw string representation.
        /// </summary>
        /// <param name="httpRequest">The HTTP request to be converted into a string.</param>
        /// <returns>Text representation of the given HTTP request object.</returns>
        string ToRawString(HttpRequestBase httpRequest);
    }
}