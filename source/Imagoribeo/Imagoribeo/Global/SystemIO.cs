using Imagoribeo.Interfaces.Global;
using System.IO;
using System;

namespace Imagoribeo.Global
{
    public class SystemIO : ISystemIO
    {
        public void DirectoryCreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public string[] DirectoryGetFiles(string path)
        {
            return Directory.GetFiles(path);
        }

        public void FileDelete(string path)
        {
            File.Delete(path);
        }

        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public FileInfo FileInfo(string path)
        {
            return new FileInfo(path);
        }
    }
}
