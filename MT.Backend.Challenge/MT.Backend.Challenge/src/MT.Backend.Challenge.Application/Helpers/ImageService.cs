using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.IO;
using System.Security.Principal;
using System;
using MT.Backend.Challenge.Domain.Constants;
using MT.Backend.Challenge.Domain.Exceptions;

public class ImageService
{
    private readonly Cloudinary _cloudinary;

    public ImageService()
    {
        var account = new Account(
            "dqtwiabf9",
            "337557872979834",
            "CgIK6PnLjucH4RvrsJquMysG2oQ");

        _cloudinary = new Cloudinary(account);
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

            var uploadResult = _cloudinary.Upload(uploadParams);

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
