using SecretManager.Authenticator.Cryptography;
using SecretManager.Authenticator.EncryptionDecryption;
using SecretManager.Authenticator.Models;

namespace SecretManager.Factory
{
    public sealed class CryptographyHelperFactory
    {
        public static ICryptographyHelper GetCryptographyHelper(string type)
        {
            return type switch
            {
                "PlainText" => new PlainTextCryptoGraphyHelper(),
                "Base64" => new Base64CryptographyHelper(),
                _ => new PlainTextCryptoGraphyHelper(),
            };
        }
    }
}
