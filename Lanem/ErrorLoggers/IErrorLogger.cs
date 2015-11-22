namespace Lanem.ErrorLoggers
{
    public interface IErrorLogger
    {
        void Log(ErrorDetails errorDetails);
    }
}