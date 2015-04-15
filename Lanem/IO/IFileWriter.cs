namespace Lanem.IO
{
    public interface IFileWriter
    {
        void Write(string filePath, string content);
    }
}