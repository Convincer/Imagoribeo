using Imagoribeo.DO;
using System.Collections.Generic;

namespace Imagoribeo.Interfaces.Analyze
{
    public interface IAnalyzer
    {
        IList<FileInfo> Directory(string path, bool useCache = true);
        FileInfo File(string path);
        void CleanUp(string path);
    }
}
