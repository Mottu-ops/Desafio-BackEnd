using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Services.Interfaces
{
    public interface IClientImageUploadService
    {
        public Task<string> UploadImageAsync(string pathS3, string filePath, string contentType, Stream? imageStream = null);
    }
}
