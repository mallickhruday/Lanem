using System;

namespace Lanem.Filters
{
    /// <summary>
    /// Supports filtering of exceptions.
    /// </summary>
    public interface IExceptionFilter
    {
        /// <summary>
        /// Determines if an <param name="exception"/> should be skipped over further processing or not.
        /// </summary>
        /// <param name="exception">The exception to be checked.</param>
        /// <returns>True if the given exception should be skipped, otherwise false.</returns>
        bool SkipException(Exception exception);
    }
}