using User.S3.Lib.Service;
using User.Services.Interfaces;

namespace User.Services.Service
{
    public class ClientImageUploadService : IClientImageUploadService
    {
        private readonly S3Uploader _uploader;

        public ClientImageUploadService(S3Uploader s3Uploader)
        {
            _uploader = s3Uploader;
        }

        public async Task<string> UploadImageAsync(string pathS3, string filePath, string contentType, Stream? imageStream = null)
        {
            try
            {
                // Faça o upload da imagem usando o uploader do S3
                var imageUrl = await _uploader.Upload(pathS3, filePath, contentType, imageStream);

                // Retorne a URL da imagem
                return imageUrl;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro durante o upload da imagem para o Amazon S3: {ex.Message}");
                throw new Exception(ex.Message);
            }
        }
    }
}
