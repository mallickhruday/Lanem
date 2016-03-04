namespace Lanem.Loggers
{
    /// <summary>
    /// Defines a single method to log <see cref="Error"/> information.
    /// </summary>
    public interface IErrorLogger
    {
        /// <summary>
        /// Logs an <see cref="Error"/>.
        /// </summary>
        /// <param name="error">The error to be logged.</param>
        void Log(Error error);
    }
}