using FileModifier.Services;
using Microsoft.AspNetCore.Mvc;

namespace FileModifier.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController(FileService fileService) : ControllerBase
    {
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var mutatedFile = await fileService.MutateFileAsync(file);

            return File(mutatedFile, "application/octet-stream", "mutated_" + file.FileName);
        }
    }
}