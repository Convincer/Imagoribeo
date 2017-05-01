using Imagoribeo.DO;
using Xunit;

namespace ImagoribeoTests.DO
{
    public class FileInfoTests
    {
        [Fact]
        public void should_return_useful_string()
        {
            var infoObject = new FileInfo()
            {
                Filename = "file",
                Hashsum = "0815",
                Width = 1,
                Height = 2
            };
            var result = infoObject.ToString();

            Assert.Equal(result, "Filename = file\r\nHashsum = 0815\r\nSize = 1x2\r\n");
        }

        [Fact]
        public void should__not_crash_if_empty()
        {
            var infoObject = new FileInfo();

            var result = infoObject.ToString();

            Assert.Equal(result, "Filename = \r\nHashsum = \r\nSize = x\r\n");
        }


    }
}
