using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FileModifier.Services
{

    /// <summary>
    /// Provides services for file manipulation.
    /// </summary>
    public class FileService
    {
        /// <summary>
        /// Mutates the content of the uploaded file.
        /// </summary>
        /// <param name="file">The file to mutate.</param>
        /// <returns>The mutated file content as a byte array.</returns>
        public async Task<byte[]> MutateFileAsync(IFormFile file)
        {
            using var reader = new StreamReader(file.OpenReadStream());
            var content = await reader.ReadToEndAsync();

            var mutatedContent = MutateContent(content);

            return Encoding.UTF8.GetBytes(mutatedContent);
        }

        /// <summary>
        /// Mutates the content by appending the current date and a random alphanumeric string.
        /// </summary>
        /// <param name="content">The original content.</param>
        /// <returns>The mutated content.</returns>
        private string MutateContent(string content)
        {
            var currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var randomSequence = GetRandomAlphanumericString(10);

            return $"{content}{Environment.NewLine}Date: {currentDate}{Environment.NewLine}Random: {randomSequence}";
        }

        /// <summary>
        /// Generates a random alphanumeric string of the specified length.
        /// </summary>
        /// <param name="length">The length of the string to generate.</param>
        /// <returns>A random alphanumeric string.</returns>
        private string GetRandomAlphanumericString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();

            return new string(Enumerable.Range(0, length)
                .Select(_ => chars[random.Next(chars.Length)])
                .ToArray()
            );
        }
    }
}
