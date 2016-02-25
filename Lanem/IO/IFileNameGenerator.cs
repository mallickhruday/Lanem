namespace Lanem.IO
{
    /// <summary>
    /// Defines a single method to generate a new file name.
    /// </summary>
    public interface IFileNameGenerator
    {
        /// <summary>
        /// Generates a new file name which can be used to create a new file on disk.
        /// </summary>
        /// <returns>Full file name of a non existing file.</returns>
        string GenerateFileName();
    }
}