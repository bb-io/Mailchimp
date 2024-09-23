using System.Security.Cryptography;
using System.Text;

namespace Apps.Mailchimp.Utils;

public static class HashHelper
{
    public static string ConvertStringToHash(string input)
    {
        using var sha256 = SHA256.Create();
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            
        byte[] hashBytes = sha256.ComputeHash(inputBytes);
            
        var hashStringBuilder = new StringBuilder();
        foreach (byte b in hashBytes)
        {
            hashStringBuilder.Append(b.ToString("x2"));
        }
            
        return hashStringBuilder.ToString();
    }
}