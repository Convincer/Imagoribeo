using Imagoribeo.DO;
using Imagoribeo.Interfaces.Analyze;
using System.Xml.Serialization;

namespace Imagoribeo.Analyze
{
    public class FileInfoIO : IFileInfoIO
    {
        public FileInfo Load(string path)
        {
            using (var stream = System.IO.File.OpenRead(path))
            {
                var serializer = new XmlSerializer(typeof(FileInfo));
                return serializer.Deserialize(stream) as FileInfo;
            }
        }

        public void Save(FileInfo fileInfo, string path)
        {
            var file = System.IO.File.Create(path);
            using (var writer = new System.IO.StreamWriter(file))
            {
                var serializer = new XmlSerializer(fileInfo.GetType());
                serializer.Serialize(writer, fileInfo);
                writer.Flush();
            }
        }
    }
}
