using SecretManager.Authenticator.Models;
using System.Text;

namespace SecretManager.Authenticator.Cryptography
{
    public class Base64CryptographyHelper : ICryptographyHelper
    {
        public string Decipher(string data)
        {
            var base64EncodedBytes = Convert.FromBase64String(data);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public string Encipher(string data)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(data);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}
