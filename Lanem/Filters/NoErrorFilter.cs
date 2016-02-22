using System;

namespace Lanem.Filters
{
    public class NoErrorFilter : IErrorFilter
    {
        public bool SkipError(Exception exception)
        {
            return false;
        }
    }
}