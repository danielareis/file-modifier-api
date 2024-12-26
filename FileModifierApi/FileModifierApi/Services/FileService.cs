using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FileModifier.Services
{
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

        private string MutateContent(string content)
        {
            var currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var randomSequence = GetRandomAlphanumericString(10);

            return $"{content}{Environment.NewLine}Date: {currentDate}{Environment.NewLine}Random: {randomSequence}";
        }

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
