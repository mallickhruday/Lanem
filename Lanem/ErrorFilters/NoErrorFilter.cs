using System;

namespace Lanem.ErrorFilters
{
    public class NoErrorFilter : IErrorFilter
    {
        public bool SkipError(Exception exception)
        {
            return false;
        }
    }
}