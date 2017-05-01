using Imagoribeo.DO;

namespace Imagoribeo.Interfaces.Analyze
{
    public interface IFileInfoIO
    {
        FileInfo Load(string path);
        void Save(FileInfo fileInfo, string path);
    }
}
