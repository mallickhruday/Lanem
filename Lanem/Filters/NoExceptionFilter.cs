using System;

namespace Lanem.Filters
{
    /// <summary>
    /// Doesn't filter any exceptions.
    /// </summary>
    public class NoExceptionFilter : IExceptionFilter
    {
        public bool SkipException(Exception exception)
        {
            return false;
        }
    }
}