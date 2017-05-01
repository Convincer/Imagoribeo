using Imagoribeo.Analyze;
using Imagoribeo.DO;
using Imagoribeo.Interfaces.Analyze;
using Imagoribeo.Interfaces.Global;
using Moq;
using Xunit;

namespace ImagoribeoTests.Analyze
{
    public class AnalyzerTests
    {
        [Fact]
        public void should_return_filled_object()
        {
            var result = new Analyzer(null, null).File("test.jpg");

            Assert.Equal(result.Width, 550);
            Assert.Equal(result.Height, 866);
        }

        [Fact]
        public void should_cleanup_old_files()
        {
            var fileInfoIO = new Mock<IFileInfoIO>();
            var systemIO = new Mock<ISystemIO>();

            systemIO.Setup(x => x.DirectoryExists("test")).Returns(true);
            systemIO.Setup(x => x.DirectoryExists("test/.archive")).Returns(true);
            systemIO.Setup(x => x.DirectoryGetFiles("test/.archive")).Returns(new[] { "path/testfile1", "path/testfile2" });
            fileInfoIO.Setup(x => x.Load("path/testfile1")).Returns(new FileInfo { Filename = "C:/path/testfile1" } );
            fileInfoIO.Setup(x => x.Load("path/testfile2")).Returns(new FileInfo { Filename = "C:/path/testfile2" });

            systemIO.Setup(x => x.FileExists("C:/path/testfile1")).Returns(true);
            systemIO.Setup(x => x.FileExists("C:/path/testfile2")).Returns(false);

            new Analyzer(fileInfoIO.Object, systemIO.Object).CleanUp("test");

            systemIO.Verify(x => x.FileDelete("path/testfile1"), Times.Never);
            systemIO.Verify(x => x.FileDelete("path/testfile2"), Times.Once);
        }
    }
}
