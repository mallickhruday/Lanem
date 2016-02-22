using System;

namespace Lanem.Filters
{
    public interface IErrorFilter
    {
        bool SkipError(Exception exception);
    }
}