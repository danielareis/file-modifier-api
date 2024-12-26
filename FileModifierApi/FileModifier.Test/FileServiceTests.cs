using FileModifier.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FileModifier.Tests
{
    [TestFixture]
    public class FileServiceTests
    {
        private FileService _fileService;

        [SetUp]
        public void Setup()
        {
            _fileService = new FileService();
        }

        [Test]
        public async Task MutateFileAsync_ShouldAddDateAndRandomSequence()
        {
            var fileContent = "Test content";
            var fileMock = new Mock<IFormFile>();
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(fileContent));
            fileMock.Setup(f => f.OpenReadStream()).Returns(ms);
            fileMock.Setup(f => f.Length).Returns(ms.Length);

            var result = await _fileService.MutateFileAsync(fileMock.Object);
            var resultContent = Encoding.UTF8.GetString(result);

            Assert.That(resultContent, Does.Contain("Test content"));
            Assert.That(resultContent, Does.Contain("Date:"));
            Assert.That(resultContent, Does.Contain("Random:"));
        }

        [Test]
        public async Task MutateFileAsync_ShouldReturnByteArray()
        {
            var fileContent = "Test content";
            var fileMock = new Mock<IFormFile>();
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(fileContent));
            fileMock.Setup(f => f.OpenReadStream()).Returns(ms);
            fileMock.Setup(f => f.Length).Returns(ms.Length);

            var result = await _fileService.MutateFileAsync(fileMock.Object);

            Assert.IsInstanceOf<byte[]>(result);
        }
    }
}
