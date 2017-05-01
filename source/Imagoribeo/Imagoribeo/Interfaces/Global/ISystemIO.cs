using System.IO;

namespace Imagoribeo.Interfaces.Global
{
    public interface ISystemIO
    {
        bool DirectoryExists(string path);
        void DirectoryCreateDirectory(string path);
        string[] DirectoryGetFiles(string path);

        bool FileExists(string path);
        void FileDelete(string path);
        FileInfo FileInfo(string path);
    }
}
