using SecretManager.Authenticator.Cryptography;
using SecretManager.Authenticator.EncryptionDecryption;
using SecretManager.Interfaces;

namespace SecretManager.Factory
{
    public static class CryptographyHelperFactory
    {
        public static ICryptographyHelper GetCryptographyHelper(string type)
        {
            return type switch
            {
                "Base64" => new Base64CryptographyHelper(),
                _ => new PlainTextCryptoGraphyHelper(),
            };
        }
    }
}
