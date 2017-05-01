using System.Text;

namespace Imagoribeo.DO
{
    public class FileInfo
    {
        public string Hashsum { get; set; }
        public string Filename { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }


        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append("Filename = ");
            result.Append(Filename);
            result.AppendLine();
            result.Append("Hashsum = ");
            result.Append(Hashsum);
            result.AppendLine();
            result.Append("Size = ");
            result.Append(Width);
            result.Append('x');
            result.Append(Height);
            result.AppendLine();

            return result.ToString();
        }
    }
}
