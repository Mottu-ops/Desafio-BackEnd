using System.Security.Cryptography;
using System.Text;

namespace Job.Domain.Commons;

public static class Cryptography
{
    public static string Encrypt(string textToEncrypt)
    {
        const string key = "48b7ab03b4cb46de89034ea8fc189ab7";
        const string initializationVector = "HR$2pIjHR$2pIj12";

        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(key);
        aes.IV = Encoding.UTF8.GetBytes(initializationVector);

        using var ms = new MemoryStream();
        using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
        {
            var inputBytes = Encoding.UTF8.GetBytes(textToEncrypt);
            cs.Write(inputBytes, 0, inputBytes.Length);
        }
        return Convert.ToBase64String(ms.ToArray());
    }
}