using FileModifier.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FileModifier.Controllers
{
    /// <summary>
    /// Controller for handling file upload and mutation operations.
    /// </summary>
    /// <param name="fileService">Service for file manipulation.</param>
    [ApiController]
    [Route("api/[controller]")]
    public class FileController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Uploads a file, mutates its content, and returns the mutated file.
        /// </summary>
        /// <param name="file">The file to be uploaded and mutated.</param>
        /// <returns>A mutated file with the same name prefixed with "mutated_".</returns>
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var command = new MutateFileCommand(file);
            var mutatedFile = await mediator.Send(command);

            return File(mutatedFile, "application/octet-stream", "mutated_" + file.FileName);
        }
    }
}