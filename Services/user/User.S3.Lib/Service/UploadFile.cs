using Amazon.S3;
using Amazon.S3.Model;
using Amazon;
using System.Net;

namespace User.S3.Lib.Service
{
    public class S3Uploader
    {
        private readonly string _bucket = "";
        private readonly string _key = "";
        private readonly string _secret = "";


        private readonly RegionEndpoint _region = RegionEndpoint.SAEast1;

        public S3Uploader(string bucket, string key, string secret)
        {
            _bucket = bucket;
            _key = key;
            _secret = secret;
        }

        public async Task<string> Upload(string _pathS3, string _filePath, string _contentType, Stream? _inputStream)
        {
            try
            {
                var config = new AmazonS3Config
                {
                    SignatureVersion = "4",
                    RegionEndpoint = _region,
                    SignatureMethod = Amazon.Runtime.SigningAlgorithm.HmacSHA256
                };

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                using var client = new AmazonS3Client(new Amazon.Runtime.BasicAWSCredentials(_key, _secret), config);
                var putRequest = new PutObjectRequest
                {
                    BucketName = _bucket,
                    Key = _pathS3,
                    CannedACL = S3CannedACL.PublicRead
                };

                if (_inputStream != null && _inputStream.Length > 0)
                {
                    putRequest.InputStream = _inputStream;
                }
                else
                {
                    putRequest.FilePath = _filePath;
                    putRequest.ContentType = _contentType;
                }

                var response = await client.PutObjectAsync(putRequest);

                var url = GetUrlFileS3(_bucket, _pathS3, client);

                return url;
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                string error = amazonS3Exception.ErrorCode switch
                {
                    "InvalidAccessKeyId" or "InvalidSecurity" => "Check the provided AWS Credentials.",
                    _ => "Error occurred: " + amazonS3Exception.Message
                };

                throw new Exception(error, amazonS3Exception);
            }
            catch (Exception e)
            {
                throw new Exception("Error occurred: " + e.InnerException?.Message ?? e.Message, e);
            }
        }

        private string GetUrlFileS3(string bucket, string pathS3, AmazonS3Client client)
        {
            try
            {
                var request = new GetPreSignedUrlRequest
                {
                    BucketName = bucket,
                    Key = pathS3,
                    Expires = DateTime.Now.AddDays(1)
                };

                var url = client.GetPreSignedURL(request);

                string[] parts = url.Split('?');
                return parts[0];
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}

