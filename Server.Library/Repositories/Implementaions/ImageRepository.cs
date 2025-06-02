using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Server.Library.Repositories.Contracts;

namespace Server.Library.Repositories.Implementaions
{
    public class ImageRepository(IWebHostEnvironment _env) : IImageService
    {
        public async Task<string> SaveImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Invalid image file.");

            // Ensure the folder exists
            var imagesPath = Path.Combine(_env.WebRootPath, "images");
            if (!Directory.Exists(imagesPath))
                Directory.CreateDirectory(imagesPath);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var fullPath = Path.Combine(imagesPath, fileName);
           
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return relative URL
            return $"/images/{fileName}";
        }
        public async Task<string> SaveImageFromBase64Async(string base64Image)
        {
            if (string.IsNullOrWhiteSpace(base64Image))
                throw new ArgumentException("Invalid base64 image string.");

            // Parse base64 string, expected format: "data:image/png;base64,AAA..."
            var commaIndex = base64Image.IndexOf(',');
            if (commaIndex < 0)
                throw new ArgumentException("Invalid base64 image format.");

            var metadata = base64Image.Substring(0, commaIndex);
            var base64Data = base64Image.Substring(commaIndex + 1);

            // Extract file extension (default to png)
            var ext = "png";
            var match = System.Text.RegularExpressions.Regex.Match(metadata, @"data:image/(?<type>.+?);base64");
            if (match.Success)
                ext = match.Groups["type"].Value;

            // Convert to bytes
            byte[] imageBytes = Convert.FromBase64String(base64Data);

            // Ensure folder exists
            var imagesPath = Path.Combine(_env.WebRootPath, "images");
            if (!Directory.Exists(imagesPath))
                Directory.CreateDirectory(imagesPath);

            var fileName = $"{Guid.NewGuid()}.{ext}";
            var fullPath = Path.Combine(imagesPath, fileName);

            // Save to file
            await File.WriteAllBytesAsync(fullPath, imageBytes);

            // Return relative URL
            return $"/images/{fileName}";
        }
        public async Task<string> GetImageAsBase64(string relativeUrl)
        {
            // Example input: "/images/abc.png"
            if (string.IsNullOrWhiteSpace(relativeUrl))
                throw new ArgumentException("Invalid image URL.");

            // Remove starting slash if present
            if (relativeUrl.StartsWith("/"))
                relativeUrl = relativeUrl[1..];

            var fullPath = Path.Combine(_env.WebRootPath, relativeUrl);

            if (!File.Exists(fullPath))
                throw new FileNotFoundException("Image not found.", fullPath);

            var bytes = File.ReadAllBytes(fullPath);

            // Detect mime type by extension (optional, default to png)
            var ext = Path.GetExtension(fullPath).ToLowerInvariant();
            var mimeType = ext switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".gif" => "image/gif",
                ".bmp" => "image/bmp",
                ".png" => "image/png",
                _ => "application/octet-stream"
            };

            var base64 = Convert.ToBase64String(bytes);

            return $"data:{mimeType};base64,{base64}";
        }


    }
}
