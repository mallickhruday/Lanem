using System;

namespace Lanem.ErrorLoggers
{
    public interface IErrorLogger
    {
        void Log(Exception exception);
    }
}