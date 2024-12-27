using MediatR;
using Microsoft.AspNetCore.Http;

namespace FileModifier.Commands
{
    /// <summary>
    /// Command for mutating a file.
    /// </summary>
    /// <param name="file">The file to be mutated.</param>
    public class MutateFileCommand(IFormFile file) : IRequest<byte[]>
    {
        public IFormFile File { get; } = file;
    }
}
