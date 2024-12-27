using FileModifier.Commands;
using FileModifier.Handlers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileModifier.Tests
{
    [TestFixture]
    public class MutateFileCommandHandlerTests
    {
        private MutateFileCommandHandler _handler = null!;

        [SetUp]
        public void Setup()
        {
            _handler = new MutateFileCommandHandler();
        }

        [Test]
        public async Task Handle_ShouldMutateFileContent()
        {
            var fileContent = "Test content";
            var fileMock = new Mock<IFormFile>();
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(fileContent));
            fileMock.Setup(f => f.OpenReadStream()).Returns(ms);
            fileMock.Setup(f => f.Length).Returns(ms.Length);

            var command = new MutateFileCommand(fileMock.Object);
            var cancellationToken = new CancellationToken();

            var result = await _handler.Handle(command, cancellationToken);
            var resultContent = Encoding.UTF8.GetString(result);

            Assert.IsInstanceOf<byte[]>(result);
            Assert.That(resultContent, Does.Contain("Test content"));
            Assert.That(resultContent, Does.Contain("Date:"));
            Assert.That(resultContent, Does.Contain("Random:"));
        }

        [Test]
        public async Task Handle_ShouldThrowException_WhenFileIsEmpty()
        {
            var fileMock = new Mock<IFormFile>();
            var ms = new MemoryStream();
            fileMock.Setup(f => f.OpenReadStream()).Returns(ms);
            fileMock.Setup(f => f.Length).Returns(ms.Length);

            var command = new MutateFileCommand(fileMock.Object);
            var cancellationToken = new CancellationToken();

            Assert.ThrowsAsync<InvalidDataException>(async () => await _handler.Handle(command, cancellationToken));
        }

        [Test]
        public async Task Handle_ShouldThrowException_WhenFileIsNull()
        {
            IFormFile? file = null;
            var command = new MutateFileCommand(file!);
            var cancellationToken = new CancellationToken();

            Assert.ThrowsAsync<ArgumentNullException>(async () => await _handler.Handle(command, cancellationToken));
        }
    }
}
