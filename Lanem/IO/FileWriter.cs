using System.IO;
using Guardo;

namespace Lanem.IO
{
    public sealed class FileWriter : IFileWriter
    {
        public void Write(string fileName, string content)
        {
            Requires.NotNullOrEmpty(fileName);

            File.WriteAllText(fileName, content);
        }
    }
}