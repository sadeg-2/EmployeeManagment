using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Server.Library.Repositories.Contracts
{
    public interface IImageService
    {
        Task<string> SaveImageAsync(IFormFile file);
        Task<string> SaveImageFromBase64Async(string base64Image);
        Task<string> GetImageAsBase64(string relativeUrl);

    }

}
