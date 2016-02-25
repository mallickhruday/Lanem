namespace Lanem.IO
{
    /// <summary>
    /// Defines a single method to write content synchronously into a file.
    /// </summary>
    public interface IFileWriter
    {
        /// <summary>
        /// Writes content synchronously into a file.
        /// </summary>
        /// <param name="filePath">Full file name of the file to be written to.</param>
        /// <param name="content">Content to be written into the file.</param>
        void Write(string filePath, string content);
    }
}