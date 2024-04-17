using System.Security.Cryptography;
using Motorent.Domain.Users.Services;

namespace Motorent.Infrastructure.Users;

internal sealed class EncryptionService : IEncryptionService
{
    private const int SaltSize = 16; // 128 bits
    private const int KeySize = 32; // 256 bits
    private const int Iterations = 50000;
    private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA256;

    private const char SegmentDelimiter = ':';

    public string Encrypt(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            Iterations,
            Algorithm,
            KeySize);

        return string.Join(
            SegmentDelimiter,
            Convert.ToBase64String(hash),
            Convert.ToBase64String(salt),
            Iterations,
            Algorithm);
    }

    public bool Verify(string password, string passwordHash)
    {
        var segments = passwordHash.Split(SegmentDelimiter);
        var hash = Convert.FromBase64String(segments[0]);
        var salt = Convert.FromBase64String(segments[1]);
        var iterations = int.Parse(segments[2]);
        var algorithm = new HashAlgorithmName(segments[3]);

        var testHash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            iterations,
            algorithm,
            hash.Length);

        return CryptographicOperations.FixedTimeEquals(hash, testHash);
    }
}