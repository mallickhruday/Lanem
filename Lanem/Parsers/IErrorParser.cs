namespace Lanem.Parsers
{
    /// <summary>
    /// Defines a single method to parse an <see cref="Error"/> into a <see cref="string"/>.
    /// </summary>
    public interface IErrorParser
    {
        /// <summary>
        /// Parses an error into a string.
        /// </summary>
        /// <param name="error">The error to be parsed.</param>
        /// <returns>String representation of an error.</returns>
        string Parse(Error error);
    }
}