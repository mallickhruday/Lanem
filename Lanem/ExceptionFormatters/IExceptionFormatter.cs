using System;

namespace Lanem.ExceptionFormatters
{
    public interface IExceptionFormatter
    {
        string Format(Exception exception);
    }
}