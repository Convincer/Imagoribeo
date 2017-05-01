using ImageMagick;
using Imagoribeo.DO;
using Imagoribeo.Interfaces.Analyze;
using Imagoribeo.Interfaces.Global;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace Imagoribeo.Analyze
{
    public class Analyzer : IAnalyzer
    {
        private readonly ILogger<Analyzer> logger;
        private readonly IFileInfoIO _fileInfoIO;
        private readonly ISystemIO _systemio;

        public Analyzer(IFileInfoIO fileInfoIO, ISystemIO systemio)
        {
            var loggerFactory = new LoggerFactory().AddConsole().AddDebug();
            logger = loggerFactory.CreateLogger<Analyzer>();
            _fileInfoIO = fileInfoIO;
            _systemio = systemio;
        }

        public IList<FileInfo> Directory(string path, bool useCache = true)
        {
            if(!_systemio.DirectoryExists(path))
            {
                logger.LogError(path + " not found");
                throw new ArgumentException(path + " not found");
            }

            var result = new List<FileInfo>();
 
            if(useCache)
            {
                if (!_systemio.DirectoryExists(path + "/.archive"))
                {
                    _systemio.DirectoryCreateDirectory(path + "/.archive");
                }
            }

            var files = _systemio.DirectoryGetFiles(path);
            foreach (var file in files)
            {

                if (useCache)
                {
                    var fileInfo = _systemio.FileInfo(file);
                    if (_systemio.FileExists(path + "/.archive/" + fileInfo.Name))
                    {
                        result.Add(_fileInfoIO.Load(path + "/.archive/" + fileInfo.Name));
                        continue;
                    }
                    else
                    {
                        var infoObject = File(file);
                        result.Add(infoObject);
                        _fileInfoIO.Save(infoObject, path + "/.archive/" + fileInfo.Name);
                    }
                    continue;
                }

                result.Add(File(file));
            }
 
            return result;
        }


        public FileInfo File(string path)
        {
            var result = new FileInfo();

            logger.LogDebug("loading file " + path);


            using (var image = new MagickImage(path))
            {
                result.Filename = path;
                result.Width = image.Width;
                result.Height = image.Height;
                result.Hashsum = XmlConvert.EncodeName(Encoding.ASCII.GetString(MD5.Create().ComputeHash(image.ToByteArray())));
            }

            logger.LogDebug("loading file succeeded");
            return result;
        }

        public void CleanUp(string path)
        {
            if (!_systemio.DirectoryExists(path))
            {
                logger.LogError(path + " not found");
                throw new ArgumentException(path + " not found");
            }

            if (!_systemio.DirectoryExists(path + "/.archive"))
                return;

            var files = _systemio.DirectoryGetFiles(path + "/.archive");
            foreach (var file in files)
            {
                var info = _fileInfoIO.Load(file);
                if (_systemio.FileExists(info.Filename))
                {
                    continue;
                }

                _systemio.FileDelete(file);
            }
        }
    }
}
