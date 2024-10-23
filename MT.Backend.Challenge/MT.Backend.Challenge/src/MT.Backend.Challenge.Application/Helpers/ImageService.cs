using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using MT.Backend.Challenge.Domain.Entities.configs;
using MT.Backend.Challenge.Domain.Exceptions;
using System;
using System.IO;

public class ImageService
{
    private readonly Cloudinary Cloudinary;

    public ImageService(ImageServiceConfig config)
    {
        var conf = config;
        var account = new Account(
            conf.CloudName,
            conf.ApiKey,
            conf.ApiSecret);

        Cloudinary = new Cloudinary(account);
    }

    public string UploadImage(string base64)
    {
        try
        {
            byte[] imageBytes = Convert.FromBase64String(base64);
            using var ms = new MemoryStream(imageBytes);

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription("image", ms),
                Folder = "mt-cnh",
                Overwrite = true
            };

            var uploadResult = Cloudinary.Upload(uploadParams);

            if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUrl.ToString();
            }
            return string.Empty;
        }
        catch (Exception ex)
        {
            throw new SendImageException(ex.Message);
        }
    }
}
