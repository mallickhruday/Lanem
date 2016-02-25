using System.IO;

namespace Lanem.IO
{
    public sealed class FileWriter : IFileWriter
    {
        public void Write(string filePath, string content)
        {
            File.WriteAllText(filePath, content);
        }
    }
}