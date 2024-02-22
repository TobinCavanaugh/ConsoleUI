using System.Security.Cryptography;
using System.Text;

namespace ConsoleUI;

public static class QuickHash
{
    private static Dictionary<string, HashAlgorithm> hashAlgorithms = new()
    {
        {"sha256", SHA256.Create()},
        {"sha1", SHA1.Create()},
    };

    public static string GetHash(this char[] data, string algo = "sha1")
    {
        var bytes = hashAlgorithms[algo].ComputeHash(Encoding.UTF8.GetBytes(data));
        return BuildHash(bytes);
    }


    public static string GetHash(this string data, string algo = "sha1")
    {
        var bytes = hashAlgorithms[algo].ComputeHash(Encoding.UTF8.GetBytes(data));

        return BuildHash(bytes);
    }

    private static string BuildHash(byte[] bytes)
    {
        StringBuilder sb = new();

        for (int i = 0; i < bytes.Length; i++)
        {
            sb.Append(bytes[i].ToString("x"));
        }

        return sb.ToString();
    }
}