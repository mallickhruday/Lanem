using System;

namespace Lanem.ErrorFilters
{
    public interface IErrorFilter
    {
        bool SkipError(Exception exception);
    }
}